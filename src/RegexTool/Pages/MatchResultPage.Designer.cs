namespace RegexTool.Pages
{
    partial class MatchResultPage
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbRegexMode = new System.Windows.Forms.CheckBox();
            this.cbWhole = new System.Windows.Forms.CheckBox();
            this.btnExpend = new System.Windows.Forms.Button();
            this.btnCollapse = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.tvResult = new System.Windows.Forms.TreeView();
            this.lblMatchInfo = new System.Windows.Forms.Label();
            this.cmsResult = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyValue = new System.Windows.Forms.ToolStripMenuItem();
            this.ttMatchResultPage = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.cmsResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tvResult, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblMatchInfo, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(569, 507);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbRegexMode);
            this.panel1.Controls.Add(this.cbWhole);
            this.panel1.Controls.Add(this.btnExpend);
            this.panel1.Controls.Add(this.btnCollapse);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtFilter);
            this.panel1.Controls.Add(this.lblFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(563, 24);
            this.panel1.TabIndex = 0;
            // 
            // cbRegexMode
            // 
            this.cbRegexMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRegexMode.AutoSize = true;
            this.cbRegexMode.Location = new System.Drawing.Point(256, 3);
            this.cbRegexMode.Name = "cbRegexMode";
            this.cbRegexMode.Size = new System.Drawing.Size(40, 21);
            this.cbRegexMode.TabIndex = 7;
            this.cbRegexMode.Text = "R";
            this.ttMatchResultPage.SetToolTip(this.cbRegexMode, "Use Regular Expression");
            this.cbRegexMode.UseVisualStyleBackColor = true;
            this.cbRegexMode.CheckedChanged += new System.EventHandler(this.cbRegexMode_CheckedChanged);
            // 
            // cbWhole
            // 
            this.cbWhole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWhole.AutoSize = true;
            this.cbWhole.Location = new System.Drawing.Point(235, 5);
            this.cbWhole.Name = "cbWhole";
            this.cbWhole.Size = new System.Drawing.Size(18, 17);
            this.cbWhole.TabIndex = 6;
            this.ttMatchResultPage.SetToolTip(this.cbWhole, "Match whole word");
            this.cbWhole.UseVisualStyleBackColor = true;
            // 
            // btnExpend
            // 
            this.btnExpend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpend.Location = new System.Drawing.Point(494, -1);
            this.btnExpend.Name = "btnExpend";
            this.btnExpend.Size = new System.Drawing.Size(65, 25);
            this.btnExpend.TabIndex = 5;
            this.btnExpend.Text = "Expend All";
            this.btnExpend.UseVisualStyleBackColor = true;
            this.btnExpend.Click += new System.EventHandler(this.btnExpend_Click);
            // 
            // btnCollapse
            // 
            this.btnCollapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCollapse.Location = new System.Drawing.Point(421, -1);
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.Size = new System.Drawing.Size(70, 25);
            this.btnCollapse.TabIndex = 4;
            this.btnCollapse.Text = "Collapse";
            this.btnCollapse.UseVisualStyleBackColor = true;
            this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(363, -1);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(55, 25);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(295, -1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 25);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(51, 1);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(179, 22);
            this.txtFilter.TabIndex = 1;
            this.txtFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(2, 3);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(43, 17);
            this.lblFilter.TabIndex = 0;
            this.lblFilter.Text = "Filter:";
            // 
            // tvResult
            // 
            this.tvResult.AllowDrop = true;
            this.tvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvResult.Location = new System.Drawing.Point(3, 33);
            this.tvResult.Name = "tvResult";
            this.tvResult.Size = new System.Drawing.Size(563, 451);
            this.tvResult.TabIndex = 1;
            this.tvResult.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvResult_ItemDrag);
            this.tvResult.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvResult_DragEnter);
            // 
            // lblMatchInfo
            // 
            this.lblMatchInfo.AutoSize = true;
            this.lblMatchInfo.Location = new System.Drawing.Point(3, 487);
            this.lblMatchInfo.Name = "lblMatchInfo";
            this.lblMatchInfo.Size = new System.Drawing.Size(83, 17);
            this.lblMatchInfo.TabIndex = 2;
            this.lblMatchInfo.Text = "No matches";
            // 
            // cmsResult
            // 
            this.cmsResult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyValue});
            this.cmsResult.Name = "cmsResult";
            this.cmsResult.Size = new System.Drawing.Size(154, 28);
            // 
            // tsmiCopyValue
            // 
            this.tsmiCopyValue.Name = "tsmiCopyValue";
            this.tsmiCopyValue.Size = new System.Drawing.Size(153, 24);
            this.tsmiCopyValue.Text = "Copy Value";
            this.tsmiCopyValue.Click += new System.EventHandler(this.tsmiCopyValue_Click);
            // 
            // MatchResultPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MatchResultPage";
            this.Size = new System.Drawing.Size(572, 508);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cmsResult.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.TreeView tvResult;
        private System.Windows.Forms.Button btnExpend;
        private System.Windows.Forms.Button btnCollapse;
        private System.Windows.Forms.Label lblMatchInfo;
        private System.Windows.Forms.ContextMenuStrip cmsResult;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyValue;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.CheckBox cbWhole;
        private System.Windows.Forms.ToolTip ttMatchResultPage;
        private System.Windows.Forms.CheckBox cbRegexMode;
    }
}
