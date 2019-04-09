using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using RegexTool.Core;
using RegexTool.Properties;
using System.Net;
using System.Configuration;

namespace RegexTool
{
    public partial class ToolBody : UserControl, ISetUserInterfaceTexts
    {
        private RegexFullInfo _regexInfo = null;

        private Thread _threadMatch = null;

        public string FileLocation = string.Empty;

        /// <summary>
        /// if the match count number is greater than the value, it will call Application.DoEvents per the count
        /// </summary>
        public static int MatchMinCountNeedsDoEvents = 1500;

        public Action<string> OnProjectSaved = null;
        public Action<string> OnProjectOpened = null;

        private readonly bool _showAds = true;
        public bool ShowAds { get { return _showAds; } }

        private readonly object _lockFlag = new object();

        bool _isRunning = false;

        //public ToolBody() : this(false) { }
        public ToolBody(bool showAds)
        {
            InitializeComponent();
            _showAds = showAds;
            ad.Visible = _showAds;
            spcToolRight.Panel1Collapsed = !ad.Visible;
            //ad.Hide();
            spcToolRight.IsSplitterFixed = true;
            if (ConfigurationManager.AppSettings["ResultSpliterFixed"] == "false")
            {
                spcToolRight.IsSplitterFixed = false;
            }

            bodyRight.OnNodeSelected = this.OnTreeNodeSelected;
            bodyRight.SetSourceText = this.SetSourceText;
            bodyLeft.OnRegexGroupNamesChanged = bodyRight.GotGroupNamesChanged;

            #region -- UIManager related --

            OnProjectOpened += (fileName) => UIManager.Current.AddRecentProject(fileName);
            OnProjectSaved += (fileName) => UIManager.Current.AddRecentProject(fileName);

            #endregion
        }

        public void Run()
        {
            lock (_lockFlag)
            {
                _isRunning = true;
            }

            UIManager.Current.SetMatchStopButtonStatus();

            var pattern = bodyLeft.RegexPattern;
            var options = bodyLeft.Options;
            var input = bodyLeft.SourceText;
            //todo do we allow selection match?
            var replacement = bodyRight.Replacement;
            var allowEmptyReplacement = bodyRight.AllowEmptyReplacement;
            var template = bodyRight.Template;

            if (pattern == string.Empty)
            {
                lock (_lockFlag)
                {
                    _isRunning = false;
                }
                UIManager.Current.SetMatchStopButtonStatus();
                MessageBox.Show(ResxManager.GetResourceString(FormStringKeys.STR_MSG_EMPTY_REGEXEXPRESSION));
                return;
            }

            var info = new RegexFullInfo(pattern, options, input,
                replacement, allowEmptyReplacement,
                template, bodyRight.TplPage.TplParameters);
            _regexInfo = info;

            if (_threadMatch == null || _threadMatch.IsAlive == false)
            {
                _threadMatch = new Thread(RunInternal);
                _threadMatch.IsBackground = true;
                _threadMatch.Start(info);
            }
            else
            {
                UIManager.Current.SetStatusInfo("It's still running...");
            }
        }

        private void RunInternal(object itm)
        {
            //Thread.Sleep(5000);
            RegexFullInfo info = itm as RegexFullInfo;
            if (info == null) throw new Exception();

            var worker = new RegexWorker(info);
            var result1 = worker.ExcuteMatches();

            if (result1.IsSuccess)
            {
                bodyRight.SetMatchResult(info.RegexObj, result1.Data);

                if (result1.Data != null && result1.Data.Count >= MatchMinCountNeedsDoEvents)
                    Application.DoEvents();

                var result2 = worker.ExecuteReplace();

                if (result2.IsSuccess)
                {
                    bodyRight.SetReplaceResult(result2.Data);

                    if (result1.Data != null && result1.Data.Count >= MatchMinCountNeedsDoEvents)
                        Application.DoEvents();
                }

                var result3 = worker.ExecuteTemplate();
                if (result3.IsSuccess)
                {
                    bodyRight.SetTemplateResult(result3.Data);

                    if (result1.Data != null && result1.Data.Count >= MatchMinCountNeedsDoEvents)
                        Application.DoEvents();
                }
            }
            else
            {
                if (null != result1.ErrorMessage) UIManager.Current.SetStatusInfo(result1.ErrorMessage);
            }

            bodyLeft.RegexExcuted();

            lock (_lockFlag)
            {
                _isRunning = false;
            }

            UIManager.Current.SetMatchStopButtonStatus();
        }

        public bool CanStop
        {
            get
            {
                lock (_lockFlag)
                {
                    return _isRunning;
                }
            }
        }

        public void Stop()
        {
            if (CanStop)
            {
                if (_threadMatch != null) _threadMatch.Abort();
                lock (_lockFlag)
                {
                    _isRunning = false;
                }
            }
            UIManager.Current.SetMatchStopButtonStatus();
        }

        public void SetSourceText(string input)
        {
            bodyLeft.SetInput(input);
        }

        public void OnTreeNodeSelected(LocationInfo li)
        {
            this.bodyLeft.LocateSelected(li);
        }

        internal void SaveProject(string fileName)
        {
            var data = GetPageFile();

            if (data == null) return;

            try
            {
                var fm = new FileManager();
                var result = fm.Save(data, fileName);

                if (result.Success)
                {
                    //TODO message
                    FileLocation = fileName;

                    if (OnProjectSaved != null) OnProjectSaved(fileName);
                }
                else
                {
                    //TODO message
                }
            }
            catch (Exception ex)
            {
                //TODO
                MessageBox.Show(ex.Message);
            }
        }

        internal void OpenProject(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    var fm = new FileManager();
                    var obj = fm.Open(path);

                    if (obj.Success)
                    {
                        var pf = obj.Data;
                        //todo ...
                        FileLocation = path;

                        BindModel(pf);

                        if (OnProjectOpened != null)
                            OnProjectOpened(path);
                    }
                    else
                    {
                        MessageBox.Show(obj.ErrorMessage);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal PageFile GetPageFile()
        {

            //TOdo
            return new PageFile()
            {
                FileOrUrl = bodyLeft.PathOrUrl,
                EncodingName = bodyLeft.EncodingName,
                SourceText = bodyLeft.SourceText,
                RegexPattern = bodyLeft.RegexPattern,
                Replacement = bodyRight.Replacement,
                AllowEmptyReplacement = bodyRight.AllowEmptyReplacement,
                ReplacementResult = bodyRight.ReplacementResult,
                Template = bodyRight.Template,
                TemplateResult = bodyRight.TemplateResult,
            };
        }

        public void BindModel(PageFile pf)
        {
            bodyLeft.BindModel(pf);
            bodyRight.BindModel(pf);
        }

        internal void OpenFileOrUrl(string file)
        {
            if (false == Uri.IsWellFormedUriString(file, UriKind.Absolute)
                && !File.Exists(file))
            {
                var dr = MessageBox.Show(ResxManager.GetResourceString(FormStringKeys.STR_RECENT_FILE_NOT_FOUND),
                    ResxManager.GetResourceString(FormStringKeys.STR_TITLE_WARNNING),
                    MessageBoxButtons.OKCancel);

                if (dr == DialogResult.OK)
                {
                    UIManager.Current.RemoveRecentFile(file);
                }
                return;
            }
            bodyLeft.OpenFileOrUrl(file);
        }

        public void SetUserInterfaceTexts()
        {
            this.bodyLeft.SetUserInterfaceTexts();
            this.bodyRight.SetUserInterfaceTexts();
        }

        #region -- Ad related --

        //public void OpenAds()
        //{
        //    if (this.InvokeRequired)
        //    {
        //        Action b = OpenAds;
        //        this.Invoke(b);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            ad.Visible = true;
        //            ad.InitAds();
        //        }
        //        catch (Exception ex)
        //        {
        //            System.Diagnostics.Debug.WriteLine(ex.Message);
        //        }
        //    }
        //}

        void HackHideAd()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://tools.tainisoft.com/regextool/myip.aspx");

                var response = (HttpWebResponse)request.GetResponse();
                using (var strm = new StreamReader(response.GetResponseStream()))
                {
                    var str = strm.ReadToEnd().Trim();

                    Thread.Sleep(500);//delay 1 second to make sure AdPage got initialized.
#if DEBUG
                    HideAd(true);
#else
                    if (str.IndexOf("222.126.164.66") >= 0)
                    {
                        //Hide the ads inside Rovi
                        HideAd(true);
                    }
                    //else
                    //{
                    //    //For other cases, we still only want to display the AD for a few minutes only.
                    //    //Thread.Sleep(1000);
                    //    Thread.Sleep(new Random().Next(10, 180) * 1000); //X(1 to 5) minutes
                    //    HideAd(true);
                    //}
#endif
                }
            }
            catch (WebException wex)
            {
                if (wex.Status == WebExceptionStatus.ConnectFailure)
                {
                    //TODO hide the ad or display an friendly ad html?
                    //Can it connect to bing.com, google.com or other websites, if yes. user might overwrite the hosts file.
                    HideAd(true);
                }
                else if (wex.Status == WebExceptionStatus.NameResolutionFailure)
                { //DNS issue
                    HideAd(true);
                }
                else if (wex.Status == WebExceptionStatus.ProtocolError)
                {
                    //User manually set the domain in hosts file
                    //TODO hide or show?
                    HideAd(false);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void HideAd(bool navToBlank = false)
        {
            if (this.InvokeRequired)
            {
                Action<bool> b = HideAd;
                this.Invoke(b, navToBlank);
            }
            else
            {
                ad.Visible = false;
                bodyRight.Dock = DockStyle.Fill;

                if (navToBlank)
                {
                    //Some Script in the Ads page would cause high CPU on client PC, which is not good.
                    //So navigate it to blank page after a few seconds
                    Action<int> n2b = ad.NavToBlank;
                    n2b.BeginInvoke((new Random()).Next(3, 30), null, null);
                    //ad.NavToBlank();
                }
            }
        }
        #endregion
    }
}
