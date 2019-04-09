using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using RegexTool.Core;
using RegexTool.Pages;

namespace RegexTool
{
    public partial class ToolBodyRight : UserControl, ISetUserInterfaceTexts
    {
        public Action<string> SetSourceText = null;
        public Action<string[]> GotGroupNamesChanged = null;

        public TemplatePage TplPage
        {
            get { return this.tabControl1.TabPages[2].Controls[0] as TemplatePage; }
        }

        public ToolBodyRight()
        {
            InitializeComponent();

            this.matchResult1.OnNodeSelected = this.TreeNodeSelected;
            this.replacePage1.SetSourceText = SetSourceTextInternal;
            this.templatePage1.SetSourceText = SetSourceTextInternal;
            GotGroupNamesChanged = this.templatePage1.GotGroupNamesChanged;
        }

        public Action<LocationInfo> OnNodeSelected = null;

        public string Replacement
        {
            get { return this.replacePage1.Replacement; }
        }

        public bool AllowEmptyReplacement
        {
            get { return this.replacePage1.AllowEmptyReplacement; }
        }

        public string ReplacementResult
        {
            get { return this.replacePage1.ReplaceResult; }
        }

        public string Template
        {
            get { return this.templatePage1.Template; }
        }

        public TemplateParameters TplParameters
        {
            get { return this.templatePage1.TplParameters; }
        }

        public string TemplateResult
        {
            get { return this.templatePage1.TemplateResult; }
        }

        public void SetMatchResult(Regex reg, MatchCollection mc)
        {
            this.matchResult1.SetResult(reg, mc);
        }

        public void SetReplaceResult(string result)
        {
            this.replacePage1.SetResult(result);
        }

        public void SetTemplateResult(TemplateResult result)
        {
            this.templatePage1.SetResult(result);
        }

        protected void TreeNodeSelected(LocationInfo li)
        {
            if (OnNodeSelected != null)
                OnNodeSelected(li);
        }

        public void BindModel(PageFile pf)
        {
            this.matchResult1.BindModel(pf);
            replacePage1.BindModel(pf);
            templatePage1.BindModel(pf);
        }

        private void SetSourceTextInternal(string input)
        {
            if (SetSourceText != null) SetSourceText(input);
        }

        public void SetUserInterfaceTexts()
        {
            tabControl1.TabPages[0].Text = ResxManager.GetResourceString(FormStringKeys.STR_PAGE_MATCHES);
            tabControl1.TabPages[1].Text = ResxManager.GetResourceString(FormStringKeys.STR_PAGE_REPLACE);
            tabControl1.TabPages[2].Text = ResxManager.GetResourceString(FormStringKeys.STR_PAGE_TEMPLATE);

            this.matchResult1.SetUserInterfaceTexts();
            this.replacePage1.SetUserInterfaceTexts();
            this.templatePage1.SetUserInterfaceTexts();
        }
    }
}
