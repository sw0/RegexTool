namespace RegexTool.SimpleComparer
{
    partial class ComparePage
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
            this.scVertical = new System.Windows.Forms.SplitContainer();
            this.spHoriz = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbCompareResult = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtResultLNR = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtResultRNL = new System.Windows.Forms.TextBox();
            this.btnCompare = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.cmsLNR = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsRNL = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.compareItem1 = new RegexTool.SimpleComparer.CompareItem();
            this.compareItem2 = new RegexTool.SimpleComparer.CompareItem();
            ((System.ComponentModel.ISupportInitialize)(this.scVertical)).BeginInit();
            this.scVertical.Panel1.SuspendLayout();
            this.scVertical.Panel2.SuspendLayout();
            this.scVertical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spHoriz)).BeginInit();
            this.spHoriz.Panel1.SuspendLayout();
            this.spHoriz.Panel2.SuspendLayout();
            this.spHoriz.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tbCompareResult.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // scVertical
            // 
            this.scVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scVertical.Location = new System.Drawing.Point(0, 0);
            this.scVertical.Name = "scVertical";
            this.scVertical.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scVertical.Panel1
            // 
            this.scVertical.Panel1.Controls.Add(this.spHoriz);
            // 
            // scVertical.Panel2
            // 
            this.scVertical.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.scVertical.Size = new System.Drawing.Size(682, 584);
            this.scVertical.SplitterDistance = 396;
            this.scVertical.SplitterWidth = 8;
            this.scVertical.TabIndex = 0;
            // 
            // spHoriz
            // 
            this.spHoriz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spHoriz.Location = new System.Drawing.Point(0, 0);
            this.spHoriz.Name = "spHoriz";
            // 
            // spHoriz.Panel1
            // 
            this.spHoriz.Panel1.Controls.Add(this.compareItem1);
            // 
            // spHoriz.Panel2
            // 
            this.spHoriz.Panel2.Controls.Add(this.compareItem2);
            this.spHoriz.Size = new System.Drawing.Size(682, 396);
            this.spHoriz.SplitterDistance = 338;
            this.spHoriz.SplitterWidth = 6;
            this.spHoriz.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tbCompareResult, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnCompare, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblResult, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(682, 180);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tbCompareResult
            // 
            this.tbCompareResult.Controls.Add(this.tabPage1);
            this.tbCompareResult.Controls.Add(this.tabPage2);
            this.tbCompareResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCompareResult.Location = new System.Drawing.Point(3, 33);
            this.tbCompareResult.Name = "tbCompareResult";
            this.tbCompareResult.SelectedIndex = 0;
            this.tbCompareResult.Size = new System.Drawing.Size(676, 124);
            this.tbCompareResult.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtResultLNR);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(668, 95);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "In Left but not in Right";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtResultLNR
            // 
            this.txtResultLNR.ContextMenuStrip = this.cmsLNR;
            this.txtResultLNR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResultLNR.Location = new System.Drawing.Point(3, 3);
            this.txtResultLNR.Multiline = true;
            this.txtResultLNR.Name = "txtResultLNR";
            this.txtResultLNR.Size = new System.Drawing.Size(662, 89);
            this.txtResultLNR.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtResultRNL);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(668, 95);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "In Right but not in Left";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtResultRNL
            // 
            this.txtResultRNL.ContextMenuStrip = this.cmsRNL;
            this.txtResultRNL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResultRNL.Location = new System.Drawing.Point(3, 3);
            this.txtResultRNL.Multiline = true;
            this.txtResultRNL.Name = "txtResultRNL";
            this.txtResultRNL.Size = new System.Drawing.Size(662, 89);
            this.txtResultRNL.TabIndex = 0;
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(3, 3);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 23);
            this.btnCompare.TabIndex = 1;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(3, 160);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(46, 17);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "label1";
            // 
            // cmsLNR
            // 
            this.cmsLNR.Name = "contextMenuStrip1";
            this.cmsLNR.Size = new System.Drawing.Size(61, 4);
            // 
            // cmsRNL
            // 
            this.cmsRNL.Name = "contextMenuStrip1";
            this.cmsRNL.Size = new System.Drawing.Size(153, 26);
            // 
            // compareItem1
            // 
            this.compareItem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compareItem1.Location = new System.Drawing.Point(0, 0);
            this.compareItem1.Name = "compareItem1";
            this.compareItem1.Size = new System.Drawing.Size(338, 396);
            this.compareItem1.TabIndex = 0;
            // 
            // compareItem2
            // 
            this.compareItem2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compareItem2.Location = new System.Drawing.Point(0, 0);
            this.compareItem2.Name = "compareItem2";
            this.compareItem2.Size = new System.Drawing.Size(338, 396);
            this.compareItem2.TabIndex = 0;
            // 
            // ComparePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scVertical);
            this.Name = "ComparePage";
            this.Size = new System.Drawing.Size(682, 584);
            this.scVertical.Panel1.ResumeLayout(false);
            this.scVertical.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scVertical)).EndInit();
            this.scVertical.ResumeLayout(false);
            this.spHoriz.Panel1.ResumeLayout(false);
            this.spHoriz.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spHoriz)).EndInit();
            this.spHoriz.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tbCompareResult.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scVertical;
        private System.Windows.Forms.SplitContainer spHoriz;
        private CompareItem compareItem1;
        private CompareItem compareItem2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tbCompareResult;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.TextBox txtResultRNL;
        private System.Windows.Forms.TextBox txtResultLNR;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ContextMenuStrip cmsLNR;
        private System.Windows.Forms.ContextMenuStrip cmsRNL;

    }
}
