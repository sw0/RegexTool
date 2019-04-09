using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RegexTool.Core;

namespace RegexTool.Pages
{
    public partial class ReplacePage : UserControl, ITextProvider, ISetUserInterfaceTexts
    {
        public Action<string> SetSourceText = null;

        public ReplacePage()
        {
            InitializeComponent();

            SetUserInterfaceTexts();

            splitContainer1.Dock = DockStyle.Fill;

            ControlHelper.SetDragDropAbility(txtReplacement, true);
            InitializeContextMenuForReplacement();
        }

        private void InitializeContextMenuForReplacement()
        {
            cmsResult.Items.Clear();
            cmsReplacement.Items.Clear();

            this.cmsReplacement.InitTextContextMenuItems(txtReplacement, TextContextMenuType.SimpleText | TextContextMenuType.SaveContent);

            cmsResult.InitTextContextMenuItems(txtReplaceResult,
                 TextContextMenuType.SimpleText | TextContextMenuType.SaveContent | TextContextMenuType.SendToSource);

            var mi = cmsResult.FindByName<ToolStripMenuItem>(FormStringKeys.STR_MENU_ITEM_SEND_TO_SOURCE);
            mi.Click += this.tsmiSendToSource_Click;
        }

        public void SetResult(string result)
        {
            if (txtReplaceResult.InvokeRequired)
            {
                Action<string> act = SetResult;
                txtReplaceResult.Invoke(act, result);
            }
            else
            {
                this.txtReplaceResult.Text = result;
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            ToolHelper.ProcessTextContextMenu(txtReplaceResult, sender);
        }

        private void tsmiSendToSource_Click(object sender, EventArgs e)
        {
            if (SetSourceText != null)
                SetSourceText(txtReplaceResult.Text);
        }

        public void SetUserInterfaceTexts()
        {
            InitializeContextMenuForReplacement();

            label1.Text = ResxManager.GetResourceString(FormStringKeys.STR_LBL_REPLACE_PATTERN);
            label2.Text = ResxManager.GetResourceString(FormStringKeys.STR_LBL_REPLACE_RESULT);
            cbAllowEmpty.Text = ResxManager.GetResourceString(FormStringKeys.STR_LBL_ALLOW_EMPTY_REPLACE);
        }
    }
}
