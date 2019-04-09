namespace RegexTool
{
    partial class RegexBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegexBar));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtRegPattern = new System.Windows.Forms.TextBox();
            this.cmsRegex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbIgnoreCase = new System.Windows.Forms.ToolStripButton();
            this.cbMultiline = new System.Windows.Forms.ToolStripButton();
            this.cbExplicitCapture = new System.Windows.Forms.ToolStripButton();
            this.cbSingleline = new System.Windows.Forms.ToolStripButton();
            this.cbIgnorePatternWhitespace = new System.Windows.Forms.ToolStripButton();
            this.tssbMore = new System.Windows.Forms.ToolStripSplitButton();
            this.cbECMAScript = new System.Windows.Forms.ToolStripMenuItem();
            this.cbCultureInvariant = new System.Windows.Forms.ToolStripMenuItem();
            this.cbRightToLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRegexButtons = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRegOptionsHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.txtRegPattern, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tsRegexButtons, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(654, 245);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtRegPattern
            // 
            this.txtRegPattern.AllowDrop = true;
            this.txtRegPattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegPattern.ContextMenuStrip = this.cmsRegex;
            this.txtRegPattern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRegPattern.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegPattern.HideSelection = false;
            this.txtRegPattern.Location = new System.Drawing.Point(3, 35);
            this.txtRegPattern.Multiline = true;
            this.txtRegPattern.Name = "txtRegPattern";
            this.txtRegPattern.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRegPattern.Size = new System.Drawing.Size(680, 177);
            this.txtRegPattern.TabIndex = 2;
            this.txtRegPattern.TextChanged += new System.EventHandler(this.txtRegPattern_TextChanged);
            this.txtRegPattern.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtRegPattern_MouseUp);
            // 
            // cmsRegex
            // 
            this.cmsRegex.Name = "cmsRegex";
            this.cmsRegex.Size = new System.Drawing.Size(61, 4);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.toolStrip1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(680, 26);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cbIgnoreCase,
            this.cbMultiline,
            this.cbExplicitCapture,
            this.cbSingleline,
            this.cbIgnorePatternWhitespace,
            this.tssbMore});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(680, 26);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "tsRegexOptions";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(64, 23);
            this.toolStripLabel1.Text = "Options:";
            // 
            // cbIgnoreCase
            // 
            this.cbIgnoreCase.Image = ((System.Drawing.Image)(resources.GetObject("cbIgnoreCase.Image")));
            this.cbIgnoreCase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cbIgnoreCase.Name = "cbIgnoreCase";
            this.cbIgnoreCase.Size = new System.Drawing.Size(103, 23);
            this.cbIgnoreCase.Text = "IgnoreCase";
            // 
            // cbMultiline
            // 
            this.cbMultiline.Image = ((System.Drawing.Image)(resources.GetObject("cbMultiline.Image")));
            this.cbMultiline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cbMultiline.Name = "cbMultiline";
            this.cbMultiline.Size = new System.Drawing.Size(87, 23);
            this.cbMultiline.Text = "Multiline";
            // 
            // cbExplicitCapture
            // 
            this.cbExplicitCapture.Image = ((System.Drawing.Image)(resources.GetObject("cbExplicitCapture.Image")));
            this.cbExplicitCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cbExplicitCapture.Name = "cbExplicitCapture";
            this.cbExplicitCapture.Size = new System.Drawing.Size(129, 23);
            this.cbExplicitCapture.Text = "ExplicitCapture";
            // 
            // cbSingleline
            // 
            this.cbSingleline.Image = ((System.Drawing.Image)(resources.GetObject("cbSingleline.Image")));
            this.cbSingleline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cbSingleline.Name = "cbSingleline";
            this.cbSingleline.Size = new System.Drawing.Size(94, 23);
            this.cbSingleline.Text = "Singleline";
            // 
            // cbIgnorePatternWhitespace
            // 
            this.cbIgnorePatternWhitespace.Image = ((System.Drawing.Image)(resources.GetObject("cbIgnorePatternWhitespace.Image")));
            this.cbIgnorePatternWhitespace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cbIgnorePatternWhitespace.Name = "cbIgnorePatternWhitespace";
            this.cbIgnorePatternWhitespace.Size = new System.Drawing.Size(196, 24);
            this.cbIgnorePatternWhitespace.Text = "IgnorePatternWhitespace";
            // 
            // tssbMore
            // 
            this.tssbMore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tssbMore.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbECMAScript,
            this.cbCultureInvariant,
            this.cbRightToLeft,
            this.toolStripSeparator1,
            this.tsmiRegOptionsHelp});
            this.tssbMore.Image = ((System.Drawing.Image)(resources.GetObject("tssbMore.Image")));
            this.tssbMore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssbMore.Name = "tssbMore";
            this.tssbMore.Size = new System.Drawing.Size(60, 24);
            this.tssbMore.Text = "More";
            // 
            // cbECMAScript
            // 
            this.cbECMAScript.Name = "cbECMAScript";
            this.cbECMAScript.Size = new System.Drawing.Size(194, 24);
            this.cbECMAScript.Text = "ECMAScript";
            // 
            // cbCultureInvariant
            // 
            this.cbCultureInvariant.Name = "cbCultureInvariant";
            this.cbCultureInvariant.Size = new System.Drawing.Size(194, 24);
            this.cbCultureInvariant.Text = "CultureInvariant";
            // 
            // cbRightToLeft
            // 
            this.cbRightToLeft.Name = "cbRightToLeft";
            this.cbRightToLeft.Size = new System.Drawing.Size(194, 24);
            this.cbRightToLeft.Text = "RightToLeft";
            // 
            // tsRegexButtons
            // 
            this.tsRegexButtons.Location = new System.Drawing.Point(0, 215);
            this.tsRegexButtons.Name = "tsRegexButtons";
            this.tsRegexButtons.Size = new System.Drawing.Size(686, 25);
            this.tsRegexButtons.TabIndex = 3;
            this.tsRegexButtons.Text = "toolStrip2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
            // 
            // tsmiRegOptionsHelp
            // 
            this.tsmiRegOptionsHelp.Name = "tsmiRegOptionsHelp";
            this.tsmiRegOptionsHelp.Size = new System.Drawing.Size(194, 24);
            this.tsmiRegOptionsHelp.Text = "Online Document";
            this.tsmiRegOptionsHelp.ToolTipText = "Online Document";
            // 
            // RegexBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RegexBar";
            this.Size = new System.Drawing.Size(654, 245);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip cmsRegex;
        private System.Windows.Forms.TextBox txtRegPattern;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cbIgnoreCase;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton cbMultiline;
        private System.Windows.Forms.ToolStripButton cbExplicitCapture;
        private System.Windows.Forms.ToolStripButton cbSingleline;
        private System.Windows.Forms.ToolStripButton cbIgnorePatternWhitespace;
        private System.Windows.Forms.ToolStripSplitButton tssbMore;
        private System.Windows.Forms.ToolStripMenuItem cbECMAScript;
        private System.Windows.Forms.ToolStripMenuItem cbCultureInvariant;
        private System.Windows.Forms.ToolStripMenuItem cbRightToLeft;
        private System.Windows.Forms.ToolStrip tsRegexButtons;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiRegOptionsHelp;

    }
}
