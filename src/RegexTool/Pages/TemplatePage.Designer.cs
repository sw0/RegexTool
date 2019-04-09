namespace RegexTool.Pages
{
    partial class TemplatePage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplatePage));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbTemplateOptions = new System.Windows.Forms.ToolStripSplitButton();
            this.cbShowDuplicatedOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.cbIngoreDuplicated = new System.Windows.Forms.ToolStripMenuItem();
            this.cbAutoTrunDigitsToNumberInSort = new System.Windows.Forms.ToolStripMenuItem();
            this.txtOrderBy = new System.Windows.Forms.ToolStripComboBox();
            this.tsbOrderBy = new System.Windows.Forms.ToolStripComboBox();
            this.txtItemsPerBatch = new System.Windows.Forms.ToolStripComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.scTemplateAndBatchSprt = new System.Windows.Forms.SplitContainer();
            this.txtTemplate = new System.Windows.Forms.TextBox();
            this.cmsTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tlpBatchSeparator = new System.Windows.Forms.TableLayoutPanel();
            this.txtBatchSeparator = new System.Windows.Forms.TextBox();
            this.lblBatchSeparator = new System.Windows.Forms.Label();
            this.tsGroups = new System.Windows.Forms.ToolStrip();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTemplateResult = new System.Windows.Forms.TextBox();
            this.cmsResult = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblTemplateResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scTemplateAndBatchSprt)).BeginInit();
            this.scTemplateAndBatchSprt.Panel1.SuspendLayout();
            this.scTemplateAndBatchSprt.Panel2.SuspendLayout();
            this.scTemplateAndBatchSprt.SuspendLayout();
            this.tlpBatchSeparator.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(357, 351);
            this.splitContainer1.SplitterDistance = 130;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.scTemplateAndBatchSprt, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tsGroups, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(357, 130);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(351, 26);
            this.panel1.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbTemplateOptions,
            this.txtOrderBy,
            this.tsbOrderBy,
            this.txtItemsPerBatch});
            this.toolStrip1.Location = new System.Drawing.Point(33, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(298, 28);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "$0";
            // 
            // tsbTemplateOptions
            // 
            this.tsbTemplateOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbTemplateOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbShowDuplicatedOnly,
            this.cbIngoreDuplicated,
            this.cbAutoTrunDigitsToNumberInSort});
            this.tsbTemplateOptions.Image = ((System.Drawing.Image)(resources.GetObject("tsbTemplateOptions.Image")));
            this.tsbTemplateOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTemplateOptions.Name = "tsbTemplateOptions";
            this.tsbTemplateOptions.Size = new System.Drawing.Size(77, 25);
            this.tsbTemplateOptions.Text = "Options";
            this.tsbTemplateOptions.ButtonClick += new System.EventHandler(this.tsbTemplateOptions_ButtonClick);
            // 
            // cbShowDuplicatedOnly
            // 
            this.cbShowDuplicatedOnly.CheckOnClick = true;
            this.cbShowDuplicatedOnly.Name = "cbShowDuplicatedOnly";
            this.cbShowDuplicatedOnly.Size = new System.Drawing.Size(307, 24);
            this.cbShowDuplicatedOnly.Text = "Duplicated Only";
            // 
            // cbIngoreDuplicated
            // 
            this.cbIngoreDuplicated.CheckOnClick = true;
            this.cbIngoreDuplicated.Name = "cbIngoreDuplicated";
            this.cbIngoreDuplicated.Size = new System.Drawing.Size(307, 24);
            this.cbIngoreDuplicated.Text = "Ingore Duplicated";
            // 
            // cbAutoTrunDigitsToNumberInSort
            // 
            this.cbAutoTrunDigitsToNumberInSort.Checked = true;
            this.cbAutoTrunDigitsToNumberInSort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoTrunDigitsToNumberInSort.Name = "cbAutoTrunDigitsToNumberInSort";
            this.cbAutoTrunDigitsToNumberInSort.Size = new System.Drawing.Size(307, 24);
            this.cbAutoTrunDigitsToNumberInSort.Text = "Auto Trun Digits to Number in sort";
            // 
            // txtOrderBy
            // 
            this.txtOrderBy.AutoCompleteCustomSource.AddRange(new string[] {
            "$0",
            "$1",
            "$2",
            "$3",
            "$4",
            "$5",
            "$6",
            "$7",
            "$8",
            "$9",
            "$10",
            "$11"});
            this.txtOrderBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtOrderBy.AutoSize = false;
            this.txtOrderBy.DropDownWidth = 50;
            this.txtOrderBy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.txtOrderBy.Items.AddRange(new object[] {
            "$0",
            "$1"});
            this.txtOrderBy.Name = "txtOrderBy";
            this.txtOrderBy.Size = new System.Drawing.Size(70, 28);
            this.txtOrderBy.Text = "$0";
            this.txtOrderBy.ToolTipText = "specify the order by group, like $0";
            // 
            // tsbOrderBy
            // 
            this.tsbOrderBy.AutoSize = false;
            this.tsbOrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsbOrderBy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.tsbOrderBy.Items.AddRange(new object[] {
            "None",
            "ASC",
            "DESC"});
            this.tsbOrderBy.MaxDropDownItems = 4;
            this.tsbOrderBy.Name = "tsbOrderBy";
            this.tsbOrderBy.Size = new System.Drawing.Size(68, 28);
            this.tsbOrderBy.ToolTipText = "Order";
            // 
            // txtItemsPerBatch
            // 
            this.txtItemsPerBatch.AutoSize = false;
            this.txtItemsPerBatch.Items.AddRange(new object[] {
            "0",
            "2",
            "3",
            "4",
            "5",
            "8",
            "10",
            "20",
            "30",
            "40",
            "50",
            "100",
            "150",
            "200",
            "300",
            "400",
            "500",
            "1000",
            "2000",
            "3000",
            "4000",
            "5000"});
            this.txtItemsPerBatch.Name = "txtItemsPerBatch";
            this.txtItemsPerBatch.Size = new System.Drawing.Size(65, 28);
            this.txtItemsPerBatch.Text = "0";
            this.txtItemsPerBatch.ToolTipText = "Input the number that you want it as items per batch.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Template:";
            // 
            // scTemplateAndBatchSprt
            // 
            this.scTemplateAndBatchSprt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTemplateAndBatchSprt.Location = new System.Drawing.Point(3, 35);
            this.scTemplateAndBatchSprt.Name = "scTemplateAndBatchSprt";
            // 
            // scTemplateAndBatchSprt.Panel1
            // 
            this.scTemplateAndBatchSprt.Panel1.Controls.Add(this.txtTemplate);
            // 
            // scTemplateAndBatchSprt.Panel2
            // 
            this.scTemplateAndBatchSprt.Panel2.Controls.Add(this.tlpBatchSeparator);
            this.scTemplateAndBatchSprt.Size = new System.Drawing.Size(351, 62);
            this.scTemplateAndBatchSprt.SplitterDistance = 231;
            this.scTemplateAndBatchSprt.TabIndex = 5;
            // 
            // txtTemplate
            // 
            this.txtTemplate.AcceptsTab = true;
            this.txtTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTemplate.ContextMenuStrip = this.cmsTemplate;
            this.txtTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTemplate.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemplate.Location = new System.Drawing.Point(0, 0);
            this.txtTemplate.Multiline = true;
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.Size = new System.Drawing.Size(231, 62);
            this.txtTemplate.TabIndex = 3;
            // 
            // cmsTemplate
            // 
            this.cmsTemplate.Name = "cmsResult";
            this.cmsTemplate.Size = new System.Drawing.Size(61, 4);
            // 
            // tlpBatchSeparator
            // 
            this.tlpBatchSeparator.ColumnCount = 1;
            this.tlpBatchSeparator.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.tlpBatchSeparator.Controls.Add(this.txtBatchSeparator, 0, 1);
            this.tlpBatchSeparator.Controls.Add(this.lblBatchSeparator, 0, 0);
            this.tlpBatchSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBatchSeparator.Location = new System.Drawing.Point(0, 0);
            this.tlpBatchSeparator.MinimumSize = new System.Drawing.Size(0, 70);
            this.tlpBatchSeparator.Name = "tlpBatchSeparator";
            this.tlpBatchSeparator.RowCount = 2;
            this.tlpBatchSeparator.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpBatchSeparator.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpBatchSeparator.Size = new System.Drawing.Size(116, 70);
            this.tlpBatchSeparator.TabIndex = 5;
            // 
            // txtBatchSeparator
            // 
            this.txtBatchSeparator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBatchSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBatchSeparator.Location = new System.Drawing.Point(3, 23);
            this.txtBatchSeparator.Multiline = true;
            this.txtBatchSeparator.Name = "txtBatchSeparator";
            this.txtBatchSeparator.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBatchSeparator.Size = new System.Drawing.Size(123, 66);
            this.txtBatchSeparator.TabIndex = 3;
            this.txtBatchSeparator.Text = "\r\n-- BATCH --\r\n\r\n";
            // 
            // lblBatchSeparator
            // 
            this.lblBatchSeparator.AutoSize = true;
            this.lblBatchSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBatchSeparator.Location = new System.Drawing.Point(3, 0);
            this.lblBatchSeparator.Name = "lblBatchSeparator";
            this.lblBatchSeparator.Size = new System.Drawing.Size(123, 20);
            this.lblBatchSeparator.TabIndex = 0;
            this.lblBatchSeparator.Text = "Batch Separator:";
            // 
            // tsGroups
            // 
            this.tsGroups.Location = new System.Drawing.Point(0, 100);
            this.tsGroups.Name = "tsGroups";
            this.tsGroups.Size = new System.Drawing.Size(357, 25);
            this.tsGroups.TabIndex = 6;
            this.tsGroups.Text = "toolStrip2";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtTemplateResult, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblTemplateResult, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(357, 216);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Result:";
            // 
            // txtTemplateResult
            // 
            this.txtTemplateResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTemplateResult.ContextMenuStrip = this.cmsResult;
            this.txtTemplateResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTemplateResult.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemplateResult.Location = new System.Drawing.Point(3, 20);
            this.txtTemplateResult.Multiline = true;
            this.txtTemplateResult.Name = "txtTemplateResult";
            this.txtTemplateResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTemplateResult.Size = new System.Drawing.Size(351, 173);
            this.txtTemplateResult.TabIndex = 1;
            // 
            // cmsResult
            // 
            this.cmsResult.Name = "cmsResult";
            this.cmsResult.Size = new System.Drawing.Size(61, 4);
            // 
            // lblTemplateResult
            // 
            this.lblTemplateResult.AutoSize = true;
            this.lblTemplateResult.Location = new System.Drawing.Point(3, 196);
            this.lblTemplateResult.Name = "lblTemplateResult";
            this.lblTemplateResult.Size = new System.Drawing.Size(121, 17);
            this.lblTemplateResult.TabIndex = 2;
            this.lblTemplateResult.Text = "lblTemplateResult";
            // 
            // TemplatePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "TemplatePage";
            this.Size = new System.Drawing.Size(360, 366);
            this.Load += new System.EventHandler(this.TemplatePage_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.scTemplateAndBatchSprt.Panel1.ResumeLayout(false);
            this.scTemplateAndBatchSprt.Panel1.PerformLayout();
            this.scTemplateAndBatchSprt.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scTemplateAndBatchSprt)).EndInit();
            this.scTemplateAndBatchSprt.ResumeLayout(false);
            this.tlpBatchSeparator.ResumeLayout(false);
            this.tlpBatchSeparator.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTemplateResult;
        private System.Windows.Forms.ContextMenuStrip cmsResult;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTemplateResult;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton tsbTemplateOptions;
        private System.Windows.Forms.ToolStripMenuItem cbShowDuplicatedOnly;
        private System.Windows.Forms.ToolStripMenuItem cbIngoreDuplicated;
        private System.Windows.Forms.ToolStripComboBox tsbOrderBy;
        private System.Windows.Forms.ToolStripMenuItem cbAutoTrunDigitsToNumberInSort;
        private System.Windows.Forms.ToolStripComboBox txtOrderBy;
        private System.Windows.Forms.ToolStripComboBox txtItemsPerBatch;
        private System.Windows.Forms.SplitContainer scTemplateAndBatchSprt;
        private System.Windows.Forms.TextBox txtTemplate;
        private System.Windows.Forms.TableLayoutPanel tlpBatchSeparator;
        private System.Windows.Forms.TextBox txtBatchSeparator;
        private System.Windows.Forms.Label lblBatchSeparator;
        private System.Windows.Forms.ContextMenuStrip cmsTemplate;
        private System.Windows.Forms.ToolStrip tsGroups;
    }
}
