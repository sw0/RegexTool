namespace RegexTool
{
    partial class ToolBodyLeft
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tlpSource = new System.Windows.Forms.TableLayoutPanel();
            this.sourceBar1 = new RegexTool.SourceBar();
            this.txtInput = new System.Windows.Forms.RichTextBox();
            this.regexBar1 = new RegexTool.RegexBar();
            this.cmsInput = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tlpSource.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tlpSource);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.regexBar1);
            this.splitContainer1.Size = new System.Drawing.Size(738, 448);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 1;
            // 
            // tlpSource
            // 
            this.tlpSource.ColumnCount = 1;
            this.tlpSource.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSource.Controls.Add(this.sourceBar1, 0, 0);
            this.tlpSource.Controls.Add(this.txtInput, 0, 1);
            this.tlpSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSource.Location = new System.Drawing.Point(0, 0);
            this.tlpSource.Name = "tlpSource";
            this.tlpSource.RowCount = 2;
            this.tlpSource.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSource.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSource.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSource.Size = new System.Drawing.Size(738, 250);
            this.tlpSource.TabIndex = 1;
            // 
            // sourceBar1
            // 
            this.sourceBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceBar1.Location = new System.Drawing.Point(3, 3);
            this.sourceBar1.Name = "sourceBar1";
            this.sourceBar1.Size = new System.Drawing.Size(732, 28);
            this.sourceBar1.TabIndex = 1;
            // 
            // txtInput
            // 
            this.txtInput.AcceptsTab = true;
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.EnableAutoDragDrop = true;
            this.txtInput.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.Location = new System.Drawing.Point(3, 37);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(732, 210);
            this.txtInput.TabIndex = 3;
            this.txtInput.Text = "";
            this.txtInput.SelectionChanged += new System.EventHandler(this.txtInput_SelectionChanged);
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // regexBar1
            // 
            this.regexBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regexBar1.Location = new System.Drawing.Point(0, 0);
            this.regexBar1.Name = "regexBar1";
            this.regexBar1.Size = new System.Drawing.Size(738, 192);
            this.regexBar1.TabIndex = 0;
            // 
            // cmsInput
            // 
            this.cmsInput.Name = "cmsInput";
            this.cmsInput.Size = new System.Drawing.Size(61, 4);
            // 
            // ToolBodyLeft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ToolBodyLeft";
            this.Size = new System.Drawing.Size(738, 448);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tlpSource.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tlpSource;
        private SourceBar sourceBar1;
        private RegexBar regexBar1;
        private System.Windows.Forms.ContextMenuStrip cmsInput;
        private System.Windows.Forms.RichTextBox txtInput;
    }
}
