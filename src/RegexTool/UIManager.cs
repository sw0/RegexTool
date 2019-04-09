using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RegexTool.Core;
using RegexTool.Properties;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace RegexTool
{
    public class UIManager
    {
        private static UIManager _current = null;

        public static UIManager Current
        {
            get { return _current; }
            set { _current = value; }
        }

        public ToolBody CurrentTool
        {
            get
            {
                var currentTab = _tabControl.SelectedTab;

                if (currentTab == null)
                {
                    return null;
                }

                var toolbody = currentTab.Controls[0] as ToolBody;
                if (toolbody == null) throw new Exception("fatal error: toolbody is null");
                return toolbody;
            }
        }

        #region -- properties --
        private readonly Form _form = null;
        private readonly TabControl _tabControl = null;
        #endregion

        #region -- feature: recent files and recent projects --

        private readonly IRecentFilesMonitor _recentFilesMonitor = new RecentFilesMonitor("recentfiles.txt", 15);

        private readonly IRecentFilesMonitor _recentProjectsMonitor = new RecentFilesMonitor("recentprojects.txt", 15);

        public List<string> RecentProjects
        {
            get { return _recentProjectsMonitor.RecentItems; }
        }

        public List<string> RecentFiles
        {
            get { return _recentFilesMonitor.RecentItems; }
        }

        #endregion

        public UIManager(Form form, TabControl tc)
        {
            _form = form;
            this._tabControl = tc;
            if (tc == null) throw new Exception("error 01");
        }

        /// <summary>
        /// we only want the first tab got ads enabled.
        /// </summary>
        /// <param name="showAds"></param>
        /// <returns></returns>
        public TabPage AddNew(bool showAds)
        {
            var tpage = new TabPage(ResxManager.GetResourceString(FormStringKeys.STR_UNTITLED_PROJECT));

            var body = new ToolBody(showAds);

            body.Dock = DockStyle.Fill;
            tpage.Controls.Add(body);
            _tabControl.TabPages.Add(tpage);
            _tabControl.SelectedTab = tpage;

            return tpage;
        }

        public TabPage AddNew()
        {
            var ai = AppHelper.GetAppInfo();
            int ix = _tabControl.TabPages.Count - 1;
            for (; ix >= 0; ix--)
            {
                var t = _tabControl.TabPages[ix];

                var tb = t.Controls[0] as ToolBody;

                if (tb.ShowAds)
                {
                    break;
                }
            }

            return AddNew(false == ai.SNVerifyLocally() && ix < 0);
        }

        public TabPage FindByFileName(string fileName)
        {
            foreach (TabPage tabPage in _tabControl.TabPages)
            {
                var tb = tabPage.Controls[0] as ToolBody;
                if (tb != null && fileName.Equals(tb.FileLocation, StringComparison.OrdinalIgnoreCase))
                {
                    return tabPage;
                }
            }
            return null;
        }

        public void Remove()
        {
            if (_tabControl.TabPages.Count <= 1) return;

            var st = _tabControl.SelectedTab;

            if (st == null)
                st = _tabControl.TabPages[_tabControl.TabPages.Count - 1];

            _tabControl.TabPages.Remove(st);

            _tabControl.SelectedIndex = 0;
        }

        internal void Select(TabPage existedTabPage)
        {
            _tabControl.SelectedTab = existedTabPage;
        }

        #region -- recent files and projects --
        internal void ClearRecentFiles(string type)
        {
            switch (type)
            {
                case ToolHelper.STR_MENUITEM_RECENT_PROJECTS:
                    _recentProjectsMonitor.Clear();
                    break;
                case ToolHelper.STR_MENUITEM_RECENT_FILES:
                    _recentFilesMonitor.Clear();
                    break;
            }
        }

        internal void RemoveRecentFile(string file)
        {
            _recentFilesMonitor.Remove(file);
        }

        internal void AddRecentFile(string fileName)
        {
            _recentFilesMonitor.Add(fileName);
        }

        internal void AddRecentProject(string fileName)
        {
            _recentProjectsMonitor.Add(fileName);
        }

        internal void RemoveRecentProject(string path)
        {
            _recentProjectsMonitor.Remove(path);
        }

        public void SetRowColumnIndex(RowColumnIndex rci)
        {
            SetStatusInfo(rci.ToString());
        }

        public void SetStatusInfo(string text, bool inRed = false)
        {
            var mf = _form as MainForm;

            if (mf != null)
            {
                mf.SetStatusInfo(text, inRed);
            }
        }

        public void SetMatchStopButtonStatus()
        {
            var mf = _form as MainForm;

            if (mf != null)
            {
                mf.SetMatchStopButtons();
            }
        }

        #endregion
    }
}
