namespace RegexTool
{
    partial class ToolBody
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.spcToolRight = new System.Windows.Forms.SplitContainer();
            this.bodyLeft = new RegexTool.ToolBodyLeft();
            this.ad = new RegexTool.Pages.AdPage();
            this.bodyRight = new RegexTool.ToolBodyRight();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcToolRight)).BeginInit();
            this.spcToolRight.Panel1.SuspendLayout();
            this.spcToolRight.Panel2.SuspendLayout();
            this.spcToolRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.bodyLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.spcToolRight);
            this.splitContainer1.Size = new System.Drawing.Size(881, 544);
            this.splitContainer1.SplitterDistance = 454;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.spcToolRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcToolRight.Location = new System.Drawing.Point(0, 0);
            this.spcToolRight.Name = "splitContainer2";
            this.spcToolRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.spcToolRight.Panel1.Controls.Add(this.ad);
            // 
            // splitContainer2.Panel2
            // 
            this.spcToolRight.Panel2.Controls.Add(this.bodyRight);
            this.spcToolRight.Size = new System.Drawing.Size(421, 544);
            this.spcToolRight.SplitterDistance = 79;
            this.spcToolRight.TabIndex = 2;
            // 
            // bodyLeft
            // 
            this.bodyLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bodyLeft.Location = new System.Drawing.Point(0, 0);
            this.bodyLeft.Name = "bodyLeft";
            this.bodyLeft.Size = new System.Drawing.Size(454, 544);
            this.bodyLeft.TabIndex = 0;
            // 
            // ad
            // 
            this.ad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ad.Location = new System.Drawing.Point(0, 0);
            this.ad.Name = "ad";
            this.ad.Size = new System.Drawing.Size(421, 79);
            this.ad.TabIndex = 1;
            // 
            // bodyRight
            // 
            this.bodyRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bodyRight.Location = new System.Drawing.Point(0, 0);
            this.bodyRight.Name = "bodyRight";
            this.bodyRight.Size = new System.Drawing.Size(421, 461);
            this.bodyRight.TabIndex = 0;
            // 
            // ToolBody
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ToolBody";
            this.Size = new System.Drawing.Size(881, 544);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.spcToolRight.Panel1.ResumeLayout(false);
            this.spcToolRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcToolRight)).EndInit();
            this.spcToolRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ToolBodyLeft bodyLeft;
        private Pages.AdPage ad;
        private System.Windows.Forms.SplitContainer spcToolRight;
        private ToolBodyRight bodyRight;


    }
}
