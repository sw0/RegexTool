using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RegexTool.Core;
using System.IO;
using System.Xml.Serialization;
using RegexTool.Properties;
using System.Diagnostics;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace RegexTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            splitContainer1.Panel1Collapsed = true;

            //SysInfoHelper sih = new SysInfoHelper();
            ////initial the value
            //sih.GetUserAgentForIE();

            AppHelper ah = new AppHelper();
            AsyncCallback callBack = new AsyncCallback(ProcessUpdateResponse);

            Action<AppStartRequest> startUp = AppHelper.StartUp;

            var ui = new AppStartRequest();
            startUp.BeginInvoke(ui, callBack, ui);
            //AppHelper.StartUp();

            UIManager.Current = new UIManager(this, tcMain);

            {
                var tabNewProject = new ToolStripMenuItem();
                tabNewProject.Tag = tabNewProject.Name = tabNewProject.Text = ResxManager.GetResourceString(FormStringKeys.STR_NEW_PROJECT.ToString());
                tabNewProject.Click += mNew_Click;
                tcMain.TabCMS.Items.Add(tabNewProject);

                var tabClose = new ToolStripMenuItem();
                tabClose.Name = tabClose.Text = ResxManager.GetResourceString(FormStringKeys.STR_CLOSE_PROJECT.ToString());
                tabClose.Click += tsmiClose_Click;
                tcMain.TabCMS.Items.Add(tabClose);

                var tabClose2 = new ToolStripMenuItem();
                tabClose2.Name = tabClose2.Text = ToolHelper.STR_MENUITEM_CLOSE_WITHOUT_SAVE;
                tabClose2.Click += tsmiClose_Click;
                tabClose2.Tag = ToolHelper.STR_MENUITEM_CLOSE_WITHOUT_SAVE;
                tcMain.TabCMS.Items.Add(tabClose2);
            }

            UIManager.Current.AddNew();

            SetUserInterfaceTexts();
        }

        private void ProcessUpdateResponse(IAsyncResult result)
        {
            string a = string.Empty;
            var asr = result.AsyncState as AppStartRequest;

            if (asr == null) return;

            var ui = asr.UpdateInfo as UpdateInfo;

            if (ui == null) return;

            Version v = new Version(ui.LatestVersion);

            Version vClient = new Version(ui.ClientVersion);

            if (v.CompareTo(vClient) == 1)
            {
                if (this.InvokeRequired)
                {
                    Action<IAsyncResult> act = this.ProcessUpdateResponse;
                    this.Invoke(act, result);
                }
                else
                {
                    UpdateForm uf = new UpdateForm();
                    uf.Tag = ui;
                    var r = uf.ShowDialog();
                    if (ui.ForceUpdate)
                    {
                        this.Close();
                    }
                }
            }
        }

        private void SetUserInterfaceTexts()
        {
            tsmiHome.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_HOME);
            tsmiMisc.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_MISC);
            var sc = tsmiMisc.DropDownItems.Find("tsmiSimpleComparer", false);
            if (sc != null && sc.Length > 0) sc[0].Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_SIMPLE_COMPARER);

            tsmiSettings.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_SETTINGS);
            tsmiLanguage.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_LANGUAGE);
            tsmiDonate.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_DONATE);
            tsmiDonate.ToolTipText = "We really appreciate your support. Thank you!";
            tsmiHelp.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_HELP);

            tsbAbout.Text = ResxManager.GetResourceString(FormStringKeys.STR_ABOUT);
            tsbNew.Text = this.tsbNew2.Text = ResxManager.GetResourceString(FormStringKeys.STR_NEW_PROJECT);
            this.tsbOpen.Text = this.tsbOpen2.Text = ResxManager.GetResourceString(FormStringKeys.STR_OPEN_PROJECT);
            this.tsbSave.Text = tsbSave2.Text = ResxManager.GetResourceString(FormStringKeys.STR_SAVE_PROJECT);
            this.tsbSave.Tag = tsbSave2.Tag = FormStringKeys.STR_SAVE_PROJECT.ToString();
            this.tsbSaveAs.Text = ResxManager.GetResourceString(FormStringKeys.STR_TITLE_SAVE_PROJECT_AS);
            this.tsbSaveAs.Tag = FormStringKeys.STR_TITLE_SAVE_PROJECT_AS.ToString();
            tsbCloseProject.Text = this.tsmiClose.Text = ResxManager.GetResourceString(FormStringKeys.STR_CLOSE_PROJECT);
            this.tsmiExit.Text = ResxManager.GetResourceString(FormStringKeys.STR_EXIT);
            this.tsmiHistory.Text = ResxManager.GetResourceString(FormStringKeys.STR_HISTORY);
            this.tsmiHomepage.Text = ResxManager.GetResourceString(FormStringKeys.STR_HOMEPAGE);
            this.tsmiMatch.Text = tsmiMatch2.Text = ResxManager.GetResourceString(FormStringKeys.STR_MATCH);
            tsmiOnlineDoc.Text = ResxManager.GetResourceString(FormStringKeys.STR_ONLINE_DOC);
            tsmiOpenInternetExplorer.Text = ResxManager.GetResourceString(FormStringKeys.STR_OPEN_BROWSER);
            tsmiOpenNotepad.Text = ResxManager.GetResourceString(FormStringKeys.STR_OPEN_NOTEPAD);
            tsmiRecentFiles.Text = ResxManager.GetResourceString(FormStringKeys.STR_RECENT_FILES);
            tsmiRecentProjects.Text = ResxManager.GetResourceString(FormStringKeys.STR_RECENT_PROJECTS);
            tsmiStop.Text = ResxManager.GetResourceString(FormStringKeys.STR_STOP);

            foreach (TabPage item in this.tcMain.TabPages)
            {
                var t = item.Controls[0] as ToolBody;

                if (t != null)
                {
                    t.SetUserInterfaceTexts();
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Width = 1200;
            this.Height = 796;

            //TODO set to configuration file
            this.WindowState = FormWindowState.Maximized;

            tsmiDonate.Click += tsmiDonate_Click;
            tsmiLanguage_DropDownOpening(null, null);

            if (File.Exists("RegexTool.SimpleComparer.exe"))
            {
                var tsmi = new ToolStripMenuItem();
                tsmi.Name = "tsmiSimpleComparer";
                tsmi.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_SIMPLE_COMPARER);
                tsmi.Click += (s, x) => Process.Start("RegexTool.SimpleComparer.exe");
                tsmiMisc.DropDownItems.Add(tsmi);
            }
#if DEBUG
            //This way, I can know the release app is not built in DEBUG mode.
            this.Text = this.Text + " [DBG]";
#endif
        }

        void tsmiDonate_Click(object sender, EventArgs e)
        {
            var cul = ResxManager.GetCultureInfo() ?? Thread.CurrentThread.CurrentUICulture;
            var info = AppHelper.GetAppInfo();

            string url = string.Format(RegexToolHelper.ULR_DONATE, info.AppId, cul.Name);
            Process.Start(url);
        }

        private void mNew_Click(object sender, EventArgs e)
        {
            var tb = UIManager.Current.AddNew();
        }

        private void Run()
        {
            var toolbody = UIManager.Current.CurrentTool;

            if (toolbody != null)
            {
                toolbody.Run();
            }
        }

        public void SetMatchStopButtons()
        {
            if (this.InvokeRequired)
            {
                Action act = SetMatchStopButtons;
                this.Invoke(act);
            }
            else
            {
                var toolbody = UIManager.Current.CurrentTool;

                if (toolbody != null)
                {
                    tsmiMatch.Enabled = tsmiMatch2.Enabled = !toolbody.CanStop;
                    tsmiStop.Enabled = toolbody.CanStop;
                }
            }
        }

        private void tsbMatch_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            var am = new AboutMe();
            am.Show();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            var toolbody = UIManager.Current.CurrentTool;

            if (toolbody != null)
            {
                string cmd = string.Empty;

                if (sender is ToolStripItem)
                {
                    cmd = ((ToolStripItem)sender).Tag as string;
                }
                if (FormStringKeys.STR_TITLE_SAVE_PROJECT_AS.ToString() == cmd
                    || string.IsNullOrWhiteSpace(toolbody.FileLocation))
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Title = FormStringKeys.STR_TITLE_SAVE_PROJECT_AS.ToString() == cmd
                                    ? ResxManager.GetResourceString(FormStringKeys.STR_TITLE_SAVE_PROJECT_AS)
                                    : sfd.Title = ResxManager.GetResourceString(FormStringKeys.STR_TITLE_SAVE_PROJECT);
                    sfd.Filter = ToolHelper.STR_FILE_FILTER;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        toolbody.SaveProject(sfd.FileName);
                        string filename = Path.GetFileNameWithoutExtension(sfd.FileName);
                        tcMain.SelectedTab.Text = filename;
                        tcMain_SelectedIndexChanged(null, null);
                    }
                }
                else
                {
                    toolbody.SaveProject(toolbody.FileLocation);
                }
            }
            else
            {
                throw new Exception("1232dsdsdsdsdd");
            }
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            bool withoutSave = false;

            if (string.IsNullOrEmpty(UIManager.Current.CurrentTool.FileLocation))
            {

                if (sender != null)
                {
                    var tsmi = sender as ToolStripMenuItem;
                    if (tsmi != null && ToolHelper.STR_MENUITEM_CLOSE_WITHOUT_SAVE.Equals(tsmi.Tag))
                    {
                        withoutSave = true;
                    }
                }
                if (false == withoutSave)
                {
                    var pf = UIManager.Current.CurrentTool.GetPageFile();
                    if ((string.IsNullOrEmpty(pf.FileOrUrl)
                         && string.IsNullOrEmpty(pf.SourceText))
                        && string.IsNullOrEmpty(pf.RegexPattern))
                    {
                        //empty project, no need to save
                    }
                    else
                    {
                        tsbSave_Click(null, null);
                    }
                }
            }
            UIManager.Current.Remove();
        }

        private void tsbOpenPage_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.FileName = ToolHelper.LastOpenedFile;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName;

                OpenPage(path);
            }
        }

        private void OpenPage(string path)
        {
            var existedTabPage = UIManager.Current.FindByFileName(path);

            if (existedTabPage == null)
            {
                try
                {
                    if (false == File.Exists(path))
                    {
                        var dr = MessageBox.Show(ResxManager.GetResourceString(FormStringKeys.STR_RECENT_FILE_NOT_FOUND),
                            ResxManager.GetResourceString(FormStringKeys.STR_TITLE_WARNNING),
                            MessageBoxButtons.OKCancel);

                        if (dr == DialogResult.OK)
                        {
                            UIManager.Current.RemoveRecentProject(path);
                        }
                        return;
                    }

                    var tp = UIManager.Current.AddNew();

                    var tb = tp.Controls[0] as ToolBody;

                    tb.OpenProject(path);

                    tcMain.SelectedTab.Text = Path.GetFileNameWithoutExtension(path);

                    tcMain_SelectedIndexChanged(null, null);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("MainForm.OpenPage error: " + ex.Message);
                }
            }
            else
            {
                UIManager.Current.Select(existedTabPage);

                var tb = existedTabPage.Controls[0] as ToolBody;
                tb.OpenProject(path);
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            tsmiClose_Click(null, null);

            this.Close();
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var toolbody = UIManager.Current.CurrentTool;

            if (toolbody == null) return;

            tsbSaveAs.Enabled = !string.IsNullOrEmpty(toolbody.FileLocation);
            tsmiClose.Enabled = tsbCloseProject.Enabled = tcMain.TabPages.Count > 1;

            SetMatchStopButtons();
        }

        private void tsmiClearRecents_Click(object sender, EventArgs e)
        {
            var tsmi = sender as ToolStripMenuItem;

            if (tsmi == null) return;

            string tag = tsmi.Tag as string;

            if (string.IsNullOrEmpty(tag)) return;

            UIManager.Current.ClearRecentFiles(tag);
        }

        private void tsmiRecentFiles_DropDownOpening(object sender, EventArgs e)
        {
            var tsmi = sender as ToolStripMenuItem;

            if (tsmi == null) return;

            string tag = tsmi.Tag as string;

            if (string.IsNullOrEmpty(tag)) return;

            switch (tag)
            {
                case ToolHelper.STR_MENUITEM_RECENT_PROJECTS:
                    tsmi.DropDownItems.Clear();
                    foreach (string f in UIManager.Current.RecentProjects)
                    {
                        var mi = new ToolStripMenuItem(f);
                        mi.Tag = ToolHelper.STR_MENUITEM_RECENT_PROJECTS;
                        mi.Click += new EventHandler(RecentFileClick);
                        tsmi.DropDownItems.Insert(0, mi);
                    }

                    if (tsmi.DropDownItems.Count > 0)
                    {

                        tsmi.DropDownItems.Add(new ToolStripSeparator());
                        var tsmiClr1 = new ToolStripMenuItem();
                        tsmiClr1.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_CLEAR);
                        tsmiClr1.Tag = ToolHelper.STR_MENUITEM_RECENT_PROJECTS;
                        tsmiClr1.Click += this.tsmiClearRecents_Click;
                        tsmi.DropDownItems.Add(tsmiClr1);
                    }
                    else
                    {
                        var ep = new ToolStripMenuItem("Empty");
                        ep.Enabled = false;
                        tsmi.DropDownItems.Add(ep);
                    }
                    break;
                case ToolHelper.STR_MENUITEM_RECENT_FILES:
                    tsmi.DropDownItems.Clear();
                    foreach (string f in UIManager.Current.RecentFiles)
                    {
                        var mi = new ToolStripMenuItem(f);
                        mi.Tag = ToolHelper.STR_MENUITEM_RECENT_FILES;
                        mi.Click += new EventHandler(RecentFileClick);
                        tsmi.DropDownItems.Insert(0, mi);
                    }

                    if (tsmi.DropDownItems.Count > 0)
                    {
                        tsmi.DropDownItems.Add(new ToolStripSeparator());
                        var tsmiClr = new ToolStripMenuItem();
                        tsmiClr.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_ITEM_CLEAR);
                        tsmiClr.Tag = ToolHelper.STR_MENUITEM_RECENT_FILES;
                        tsmiClr.Click += this.tsmiClearRecents_Click;
                        tsmi.DropDownItems.Add(tsmiClr);
                    }
                    else
                    {
                        var ep = new ToolStripMenuItem("Empty");
                        ep.Enabled = false;
                        tsmi.DropDownItems.Add(ep);
                    }
                    break;
            }
        }

        private void RecentFileClick(object sender, EventArgs e)
        {
            var mi = sender as ToolStripMenuItem;
            if (mi == null) return;

            string file = mi.Text;
            string tag = mi.Tag as string;

            switch (tag)
            {
                case ToolHelper.STR_MENUITEM_RECENT_FILES:
                    UIManager.Current.CurrentTool.OpenFileOrUrl(file);
                    break;
                case ToolHelper.STR_MENUITEM_RECENT_PROJECTS:
                    OpenPage(file);
                    break;
            }
        }

        private void tsmiOnlineDoc_Click(object sender, EventArgs e)
        {
            RegexToolHelper.OpenOnlineDocument();
        }

        private void tsmiHomepage_Click(object sender, EventArgs e)
        {
            RegexToolHelper.OpenHomepage();
        }

        private void tsmiHistory_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
        }

        public void SetStatusInfo(string info, bool inRed)
        {
            tsmiStatus.Text = info ?? string.Empty;
            tsmiStatus.ForeColor = inRed ? Color.Red : Color.Black;
        }

        private void tsmiStop_Click(object sender, EventArgs e)
        {
            UIManager.Current.CurrentTool.Stop();
        }

        private void tsmiOpenNotepad_Click(object sender, EventArgs e)
        {
            RegexToolHelper.OpenNotepad();
        }

        private void tsmiOpenInternetExplorer_Click(object sender, EventArgs e)
        {
            RegexToolHelper.OpenHomepage();
        }

        private void tsmiLanguage_DropDownOpening(object sender, EventArgs e)
        {
            var ci = ResxManager.GetCultureInfo();

            if (tsmiLanguage.DropDownItems == null || tsmiLanguage.DropDownItems.Count == 0)
            {
                ToolStripMenuItem tsmi = null;
                {
                    var culture = CultureInfo.CreateSpecificCulture("en-us");
                    tsmi = new ToolStripMenuItem();
                    tsmi.Name = "tsmiLanEnglish";
                    tsmi.Text = culture.DisplayName;
                    tsmi.Tag = culture.Name;
                    if (ci == null || ci.DisplayName == culture.DisplayName) tsmi.Checked = true;
                    tsmiLanguage.DropDownItems.Add(tsmi);
                    tsmi.Click += SwichLanguge_Click;
                }
                {
                    var culture = CultureInfo.CreateSpecificCulture("zh-CN");
                    tsmi = new ToolStripMenuItem();
                    tsmi.Name = "tsmiLanChinese";
                    tsmi.Text = culture.DisplayName;
                    tsmi.Tag = culture.Name;
                    if (ci != null && ci.DisplayName == culture.DisplayName) tsmi.Checked = true;
                    tsmiLanguage.DropDownItems.Add(tsmi);
                    tsmi.Click += SwichLanguge_Click;
                }
                {
                    var culture = CultureInfo.CreateSpecificCulture("zh-tw");
                    tsmi = new ToolStripMenuItem();
                    tsmi.Name = "tsmiLanTaiwan";
                    tsmi.Text = "繁体中文";
                    tsmi.Tag = culture.Name;
                    if (ci != null && ci.DisplayName == culture.DisplayName) tsmi.Checked = true;
                    tsmiLanguage.DropDownItems.Add(tsmi);
                    tsmi.Click += SwichLanguge_Click;
                }
            }
            else
            {
                foreach (ToolStripMenuItem item in tsmiLanguage.DropDownItems)
                {
                    item.Checked = ci != null && ci.DisplayName == item.Text;
                }

                if (ci == null)
                {
                    ((ToolStripMenuItem)tsmiLanguage.DropDownItems[0]).Checked = true;
                }
            }
        }

        void SwichLanguge_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;

            if (menuItem == null)
            {
#if DEBUG
                throw new Exception("SwichLanguge_Click: sender is not a ToolStripMenuItem.");
#else
                return;
#endif
            }
            var ci = ResxManager.GetCultureInfo();

            if (ci != null && ci.Name == ((ToolStripMenuItem)sender).Tag.ToString())
            {
                //Makes no change
                return;
            }
            else
            {
                try
                {
                    var newCulture = CultureInfo.CreateSpecificCulture(menuItem.Tag.ToString());
                    ResxManager.SetCultureInfo(newCulture);
                    this.SetUserInterfaceTexts();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
#if DEBUG
                    throw;
#endif
                }
            }
        }
    }
}
