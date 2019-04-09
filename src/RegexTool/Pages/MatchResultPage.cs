using System;
using System.Windows.Forms;
using System.Diagnostics;
using RegexTool.Core;

namespace RegexTool.Pages
{
    public partial class MatchResultPage : UserControl, ISetUserInterfaceTexts
    {
        private const string STR_MATCH_INFO = "{0} Matches";
        private const string STR_MATCH_INFO_WITH_FILTER = "{0} Matches, and got {1} after filtered";

        public MatchResultPage()
        {
            InitializeComponent();

            SetUserInterfaceTexts();

            tableLayoutPanel1.Dock = DockStyle.Fill;
            tvResult.NodeMouseClick += tvResult_NodeMouseClick;
        }

        private void btnCollapse_Click(object sender, EventArgs e)
        {
            tvResult.CollapseAll();
        }

        private void btnExpend_Click(object sender, EventArgs e)
        {
            tvResult.ExpandAll();
        }

        private void tsmiCopyValue_Click(object sender, EventArgs e)
        {
            var node = tvResult.SelectedNode;

            if (node != null)
            {
                var locationInfo = node.Tag as LocationInfo;

                if (locationInfo != null && !string.IsNullOrEmpty(locationInfo.Text))
                    Clipboard.SetText(locationInfo.Text);
            }
        }

        private void tvResult_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var node = e.Item as TreeNode;
            if (node == null) return;
            DoDragDrop(((LocationInfo)node.Tag).Text, DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void tvResult_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
                e.Effect = DragDropEffects.Copy | DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFilter.Text = string.Empty;

            foreach (TreeNode node in tvResult.Nodes)
            {
                HighlightNode(node, false);
            }
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, null);
            }
        }

        private void HighlightNode(TreeNode node, bool highlight = true)
        {
            node.BackColor = highlight ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
        }

        public void SetUserInterfaceTexts()
        {
            lblFilter.Text = ResxManager.GetResourceString(FormStringKeys.STR_LBL_FILTER);
            btnSearch.Text = ResxManager.GetResourceString(FormStringKeys.STR_BTN_SEARCH);
            btnClear.Text = ResxManager.GetResourceString(FormStringKeys.STR_BTN_CLEAR);
            btnCollapse.Text = ResxManager.GetResourceString(FormStringKeys.STR_BTN_COLLAPSE);
            btnExpend.Text = ResxManager.GetResourceString(FormStringKeys.STR_BTN_EXPEND);
            ttMatchResultPage.SetToolTip(cbRegexMode, ResxManager.GetResourceString(FormStringKeys.TIP_CB_FILTER_AS_REGEX));
            ttMatchResultPage.SetToolTip(cbWhole, ResxManager.GetResourceString(FormStringKeys.TIP_CB_FILTER_WHOLEWORD));
        }

        private void cbRegexMode_CheckedChanged(object sender, EventArgs e)
        {
            cbWhole.Enabled = !cbRegexMode.Checked;
        }
    }
}
