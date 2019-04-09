using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RegexTool.Core;
using System.Windows.Forms;
using System.Threading;

namespace RegexTool.Pages
{
    public partial class TemplatePage
    {
        ToolStripMenuItem _tsmiRowNumber = new ToolStripMenuItem() { Text = "$" + TemplateParameters.STR_ROWNUM_TEXT + "$" };

        public string Template
        {
            get
            {
                return this.txtTemplate.Text;
            }
        }
        public string TemplateResult
        {
            get
            {
                return this.txtTemplateResult.Text;
            }
        }

        public void BindModel(PageFile pf)
        {
            this.txtTemplate.Text = pf.Template;
            this.txtTemplateResult.Text = pf.TemplateResult;
        }

        public string GetText()
        {
            return txtTemplateResult.Text;
        }

        /// <summary>
        /// populate the group name buttons that user can be easier to input the template by clicking. 
        /// Notice: it will take in 15 group names at maximum.
        /// </summary>
        /// <param name="groupNames"></param>
        public void GotGroupNamesChanged(string[] groupNames)
        {
            if (groupNames == null || groupNames.Length == 0)
            {
                //the regular expression is invalid, do nothing
                return;
            }
            string oldValue = txtOrderBy.Text;

            if (tsGroups.Items.Count > 0)
            {
                tsGroups.Items.Clear();
            }

            txtOrderBy.Items.Clear();
            txtOrderBy.Text = "$0";

            for (int i = 0; i < groupNames.Length; i++)
            {
                if (i > 15) break;

                string gName = groupNames[i];
                string item = string.Empty;

                if (gName.ToCharArray().All(c => char.IsNumber(c)))
                {
                    item = "$" + gName;
                    tsGroups.Items.Add(new ToolStripMenuItem() { Text = item });
                    txtOrderBy.Items.Add(item);
                }
                else
                {
                    item = "${" + gName + "}";
                    tsGroups.Items.Add(new ToolStripMenuItem() { Text = item });
                    txtOrderBy.Items.Add(item);
                }
            }

            var cul = ResxManager.GetCultureInfo() ?? Thread.CurrentThread.CurrentUICulture;

            _tsmiRowNumber.ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_ROWNUMBER);
            tsGroups.Items.Add(_tsmiRowNumber);

            foreach (var item in txtOrderBy.Items)
            {
                if (item.ToString() == oldValue)
                {
                    txtOrderBy.Text = oldValue;
                    oldValue = "";
                    break;
                }
            }

            if (oldValue != "")
            {
                //if we cannot find the old select item, set it to the first item.
                if (txtOrderBy.Items.Count > 0)
                    txtOrderBy.Text = txtOrderBy.Items[0].ToString();
                else
                {
                    txtOrderBy.Items.Add("$0");
                    txtOrderBy.Text = "$0";
                }
            }

            if (groupNames.Length > 0)
            {
                tsGroups.Items.Add(new ToolStripSeparator());
                tsGroups.Items.Add(new ToolStripMenuItem() { Text = "Tab" });
                tsGroups.Items.Add(new ToolStripMenuItem() { Text = "Space" });
                tsGroups.Items.Add(new ToolStripMenuItem() { Text = "Return" });
                tsGroups.Items.Add(new ToolStripMenuItem() { Text = "," });
                tsGroups.Items.Add(new ToolStripMenuItem() { Text = ";" });
                tsGroups.Items.Add(new ToolStripMenuItem() { Text = "-" });
                tsGroups.Items.Add(new ToolStripMenuItem() { Text = "|" });
            }

            foreach (ToolStripItem item in tsGroups.Items)
            {
                if (item is ToolStripMenuItem && item != _tsmiRowNumber)
                    item.Click += GroupItem_Click;
            }
        }

        void GroupItem_Click(object sender, EventArgs e)
        {
            var text = ((ToolStripItem)sender).Text;
            if (text.IndexOf("Tab") != -1) text = "\t";
            else if (text.IndexOf("Space") != -1) text = " ";
            else if (text.IndexOf("Return") != -1) text = "\r\n";
            txtTemplate.Insert(text, true, false);
        }
    }
}
