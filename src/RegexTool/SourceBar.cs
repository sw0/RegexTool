using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using RegexTool.Core;

namespace RegexTool
{
    public partial class SourceBar : UserControl, ISetUserInterfaceTexts
    {
        private const string STR_AUTO_DETECT = "AutoDetect";

        #region -- properties --

        public string PathOrUrl
        {
            get
            {
                return txtPathOrUrl.Text;
            }
        }

        public string EncodingName
        {
            get { return cbEncoding.Text; }
        }

        public Action<LoadResult> OnFileLoaded = null;

        #endregion

        public SourceBar()
        {
            InitializeComponent();

            OnFileLoaded += (lr) =>
            {
                if (lr != null && lr.IsSuccess) UIManager.Current.AddRecentFile(lr.FileOrUrl);
            };

            btnClear.Enabled = false;

#if INIT_TEST_DATA
            //txtPathOrUrl.Text = "http://tools.tainisoft.com/regextool/sample/sample01.html";
            //txtPathOrUrl.Text = "http://www.tainisoft.com";
            txtPathOrUrl.Text = @"D:\SkyDrive\Workplaces\RoviWork\LEARN_SCRIPTS\bestbuy_usa_rights_owner_creation.sql";
            //OpenFileOrUrl(txtPathOrUrl.Text);
#endif
        }

        public void OpenFileOrUrl(string fileOrUrl)
        {
            if (string.IsNullOrWhiteSpace(fileOrUrl)) return;
            btnLoad.Enabled = false;
            txtPathOrUrl.Text = fileOrUrl;
            OpenFileOrUrlInternal(fileOrUrl);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //var btnLoadStr = btnLoad.Text;
            btnLoad.Enabled = false;
            LoadResult result = null;

            string text = string.Empty;
            string fileOrURL = string.Empty;

            if (string.IsNullOrWhiteSpace(txtPathOrUrl.Text))
            {
                txtPathOrUrl.Text = string.Empty;

                var ofd = new OpenFileDialog();
                ofd.FileName = ToolHelper.LastOpenedFile;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    fileOrURL = txtPathOrUrl.Text = ofd.FileName;
                }
                else
                {
                    btnLoad.Enabled = true;
                    return;
                }

                try
                {
                    text = File.ReadAllText(fileOrURL);

                    ToolHelper.LastOpenedFile = fileOrURL;

                    result = new LoadResult(fileOrURL, text);
                }
                catch (Exception ex)
                {
                    result = new LoadResult(fileOrURL, "", false, ex.Message);
                }

                DataLoaded(result);
            }
            else
            {
                fileOrURL = txtPathOrUrl.Text;

                result = OpenFileOrUrlInternal(fileOrURL);
            }
        }

        private LoadResult OpenFileOrUrlInternal(string fileOrURL)
        {
            LoadResult result = null;
            string text;
            bool loadedCalled = false;

            try
            {
                if (Uri.IsWellFormedUriString(txtPathOrUrl.Text, UriKind.Absolute))
                {
                    loadedCalled = true;

                    Func<string, Encoding, LoadResult> act = LoadPage;

                    var enc = GetEncoding();
                    var res = act.BeginInvoke(fileOrURL, enc, (o) =>
                                                                  {
                                                                      Func<string, Encoding, LoadResult> func =
                                                                          (Func<string, Encoding, LoadResult>)o.AsyncState;
                                                                      var val = func.EndInvoke(o);

                                                                      DataLoaded(val);
                                                                  },
                                              act);

                    //result = LoadPage(fileOrURL);
                }
                else
                {
                    if (File.Exists(fileOrURL))
                    {
                        text = File.ReadAllText(fileOrURL);

                        ToolHelper.LastOpenedFile = fileOrURL;

                        result = new LoadResult(fileOrURL, text);
                    }
                    else
                    {
                        result = new LoadResult(fileOrURL, "", false, string.Format("File '{0}' does not exist.", fileOrURL));
                    }
                }
            }
            catch (Exception ex)
            {
                result = new LoadResult(fileOrURL, "", false, ex.Message);
            }
            finally
            {
                if (false == loadedCalled)
                    DataLoaded(result);
            }

            return result;
        }

        private void DataLoaded(LoadResult result)
        {
            if (btnLoad.InvokeRequired)
            {
                Action<LoadResult> act = DataLoaded;
                btnLoad.BeginInvoke(act, result);
            }
            else
            {
                if (OnFileLoaded != null)
                    OnFileLoaded(result);

                btnLoad.Enabled = true;
            }
        }

        private LoadResult LoadPage(string url, Encoding enc)
        {
            string text;
            LoadResult result;
            try
            {
                var wr = (HttpWebRequest)WebRequest.Create(url);

                var response = (HttpWebResponse)wr.GetResponse();

                if (enc == null)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), true))
                    {
                        text = sr.ReadToEnd();
                        result = new LoadResult(url, text);
                    }
                }
                else
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), enc))
                    {
                        text = sr.ReadToEnd();
                        result = new LoadResult(url, text);
                    }
                }

                response.Close();
            }
            catch (Exception ex)
            {
                result = new LoadResult(url, string.Empty, false, ex.Message);
            }
            return result;
        }

        private Encoding GetEncoding()
        {
            string encStr = cbEncoding.Text;
            Encoding enc = null;
            if (encStr != STR_AUTO_DETECT)
            {
                try
                {
                    enc = Encoding.GetEncoding(encStr);
                }
                catch
                {
                    cbEncoding.Text = STR_AUTO_DETECT;
                }
            }

            return enc;
        }

        private void txtSourceURI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLoad_Click(null, null);
            }
        }

        public void BindModel(PageFile pf)
        {
            txtPathOrUrl.Text = pf.FileOrUrl;
            cbEncoding.Text = pf.EncodingName;
        }

        private void txtPathOrUrl_DragDrop(object sender, DragEventArgs e)
        {
            string[] filePath = (string[])e.Data.GetData(DataFormats.FileDrop);

            txtPathOrUrl.Text = filePath[0];
        }

        private void txtPathOrUrl_DragEnter(object sender, DragEventArgs e)
        {
            // If the data is a file or a bitmap, display the copy cursor.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        public void SetUserInterfaceTexts()
        {
            btnLoad.Text = ResxManager.GetResourceString(FormStringKeys.STR_LBL_LOAD);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPathOrUrl.Clear();
        }

        private void txtPathOrUrl_TextChanged(object sender, EventArgs e)
        {
            btnClear.Enabled = txtPathOrUrl.Text.Length > 0;
        }
    }
}
