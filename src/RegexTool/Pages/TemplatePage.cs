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
    public partial class TemplatePage : UserControl, ITextProvider, ISetUserInterfaceTexts
    {
        public Action<string> SetSourceText = null;

        public TemplateParameters TplParameters
        {
            get
            {
                var result = new TemplateParameters()
                {
                    OrderInfo = OrderInfo,
                    IgnoreDuplicated = cbIngoreDuplicated.Checked,
                    ShowDuplicatedOnly = cbShowDuplicatedOnly.Checked,
                    BatchInfo = new BatchInfo()
                    {
                        BatchSeparator = txtBatchSeparator.Text,
                    },
                };

                try
                {
                    result.BatchInfo.ItemsPerBatch = Convert.ToInt32(txtItemsPerBatch.Text);
                }
                catch
                {
                    try { txtItemsPerBatch.Text = "0"; }
                    catch { }
                }

                return result;
            }
        }


        private OrderInfo OrderInfo
        {
            get
            {
                return new OrderInfo
                {
                    GroupForSort = txtOrderBy.Text.Trim(),
                    OrderType = (OrderOption)Enum.Parse(typeof(OrderOption), tsbOrderBy.Text),
                    AutoTurnDigitsToNumber = cbAutoTrunDigitsToNumberInSort.Checked,
                };
            }
        }

        public TemplatePage()
        {
            InitializeComponent();

            splitContainer1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);

            ControlHelper.SetDragDropAbility(txtTemplate, true);
            lblTemplateResult.Text = "";

            tsbOrderBy.Items.Clear();
            Enum.GetNames(typeof(OrderOption)).ToList().ForEach(ot => tsbOrderBy.Items.Add(ot));
            tsbOrderBy.Text = OrderOption.None.ToString();
            tsbOrderBy.SelectedIndexChanged += tsbOrderBy_SelectedIndexChanged;

            txtOrderBy.Enabled = false;

            txtBatchSeparator.Text = "\r\n-- BATCH SEPARATOR --\r\n\r\n";

            scTemplateAndBatchSprt.Panel2Collapsed = txtItemsPerBatch.Text.Trim() == "0";

            txtItemsPerBatch.TextChanged += (sender, e) =>
            {
                scTemplateAndBatchSprt.Panel2Collapsed = txtItemsPerBatch.Text.Trim() == "0";
            };

            InitializeContextMenuForTemplate();
        }

        private void InitializeContextMenuForTemplate()
        {
            cmsResult.Items.Clear();
            cmsTemplate.Items.Clear();

            cmsTemplate.InitTextContextMenuItems(txtTemplate, TextContextMenuType.SimpleText | TextContextMenuType.SaveContent);

            cmsResult.InitTextContextMenuItems(txtTemplateResult,
                 TextContextMenuType.SimpleText | TextContextMenuType.SaveContent | TextContextMenuType.SendToSource);

            var mi = cmsResult.FindByName<ToolStripMenuItem>(FormStringKeys.STR_MENU_ITEM_SEND_TO_SOURCE);
            mi.Click += this.tsmiSendToSource_Click;
        }

        void tsbOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOrderBy.Enabled = tsbOrderBy.Text != OrderOption.None.ToString();
        }

        internal void SetResult(TemplateResult result)
        {
            if (txtTemplateResult.InvokeRequired)
            {
                Action<TemplateResult> act = SetResult;
                txtTemplateResult.Invoke(act, result);
            }
            else
            {
                if (result != null)
                {
                    txtTemplateResult.Text = result.Result;
                    lblTemplateResult.Text = string.Format("Oringinal Count:{0}, Result Count: {1}, Duplicated: {2}", result.ItemsOringinalCount, result.ItemsCount, result.Duplicated);
                }
                else
                {
                    lblTemplateResult.Text = "Failed or template was not set.";
                }
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            ToolHelper.ProcessTextContextMenu(txtTemplateResult, sender);
        }

        private void tsmiSendToSource_Click(object sender, EventArgs e)
        {
            if (SetSourceText != null)
                SetSourceText(txtTemplateResult.Text);
        }

        private void tsbTemplateOptions_ButtonClick(object sender, EventArgs e)
        {
            tsbTemplateOptions.ShowDropDown();
        }

        public void SetUserInterfaceTexts()
        {
            InitializeContextMenuForTemplate();

            label1.Text = ResxManager.GetResourceString(FormStringKeys.STR_PAGE_TEMPLATE);
            label2.Text = ResxManager.GetResourceString(FormStringKeys.STR_LBL_TEMPLATE_RESULT);
            lblBatchSeparator.Text = ResxManager.GetResourceString(FormStringKeys.STR_LBL_BATCH_SEPARATOR);

            tsbTemplateOptions.Text = ResxManager.GetResourceString(FormStringKeys.STR_OPTIONS);
            cbAutoTrunDigitsToNumberInSort.Text = ResxManager.GetResourceString(FormStringKeys.STR_BTN_ALLOW_DIGIT_2_NUM);
            cbIngoreDuplicated.Text = ResxManager.GetResourceString(FormStringKeys.STR_BTN_INGORE_DUPLICATED);
            cbShowDuplicatedOnly.Text = ResxManager.GetResourceString(FormStringKeys.STR_BTN_DUPLICATED_ONLY);

            txtOrderBy.ToolTipText = ResxManager.GetResourceString(FormStringKeys.STR_TIP_ORDER_BY);
            tsbOrderBy.ToolTipText = ResxManager.GetResourceString(FormStringKeys.STR_TIP_ORDER_BY);
            txtItemsPerBatch.ToolTipText = ResxManager.GetResourceString(FormStringKeys.STR_TIP_ITEMS_PER_BATCH);
            _tsmiRowNumber.ToolTipText = ResxManager.GetResourceString(FormStringKeys.TIP_ROWNUMBER);
        }

        private void TemplatePage_Load(object sender, EventArgs e)
        {
            _tsmiRowNumber.Click += this.GroupItem_Click;
        }
    }

}
