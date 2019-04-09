using RegexTool.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegexTool.SimpleComparer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //menuStrip1.Hide();

            var cp = new ComparePage();
            cp.Dock = DockStyle.Fill;
            this.pnlMain.Controls.Add(cp);
        }


        private void SetUserInterfaceTexts()
        {
            tsmiHome.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_HOME);
            //tsmiMisc.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_MISC);
            tsmiSettings.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_SETTINGS);
            tsmiLanguage.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_LANGUAGE);
            tsmiNewComparer.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_NEW_WINDOW);
            tsmiHelp.Text = ResxManager.GetResourceString(FormStringKeys.STR_MENU_HELP);
            tsmiAbout.Text = ResxManager.GetResourceString(FormStringKeys.STR_ABOUT);
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            //to initialize the languages options
            tsmiLanguage_DropDownOpening(null, null);

            tsmiNewComparer.Click += (s, o) => Process.Start(System.Reflection.Assembly.GetEntryAssembly().CodeBase);
            tsmiAbout.Click += (s, o) => (new AboutBox()).Show();

            SetUserInterfaceTexts();
        }
    }
}
