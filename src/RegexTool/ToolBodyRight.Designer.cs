using RegexTool.Pages;

namespace RegexTool
{
    partial class ToolBodyRight
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpMatch = new System.Windows.Forms.TabPage();
            this.matchResult1 = new RegexTool.Pages.MatchResultPage();
            this.tpReplace = new System.Windows.Forms.TabPage();
            this.replacePage1 = new RegexTool.Pages.ReplacePage();
            this.tpTemplate = new System.Windows.Forms.TabPage();
            this.templatePage1 = new RegexTool.Pages.TemplatePage();
            this.tabControl1.SuspendLayout();
            this.tpMatch.SuspendLayout();
            this.tpReplace.SuspendLayout();
            this.tpTemplate.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpMatch);
            this.tabControl1.Controls.Add(this.tpReplace);
            this.tabControl1.Controls.Add(this.tpTemplate);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(492, 396);
            this.tabControl1.TabIndex = 0;
            // 
            // tpMatch
            // 
            this.tpMatch.Controls.Add(this.matchResult1);
            this.tpMatch.Location = new System.Drawing.Point(4, 25);
            this.tpMatch.Name = "tpMatch";
            this.tpMatch.Padding = new System.Windows.Forms.Padding(3);
            this.tpMatch.Size = new System.Drawing.Size(484, 367);
            this.tpMatch.TabIndex = 0;
            this.tpMatch.Text = "Matches";
            this.tpMatch.UseVisualStyleBackColor = true;
            // 
            // matchResult1
            // 
            this.matchResult1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.matchResult1.Location = new System.Drawing.Point(3, 3);
            this.matchResult1.Name = "matchResult1";
            this.matchResult1.Size = new System.Drawing.Size(478, 361);
            this.matchResult1.TabIndex = 0;
            // 
            // tpReplace
            // 
            this.tpReplace.Controls.Add(this.replacePage1);
            this.tpReplace.Location = new System.Drawing.Point(4, 25);
            this.tpReplace.Name = "tpReplace";
            this.tpReplace.Padding = new System.Windows.Forms.Padding(3);
            this.tpReplace.Size = new System.Drawing.Size(484, 367);
            this.tpReplace.TabIndex = 1;
            this.tpReplace.Text = "Replace";
            this.tpReplace.UseVisualStyleBackColor = true;
            // 
            // replacePage1
            // 
            this.replacePage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.replacePage1.Location = new System.Drawing.Point(3, 3);
            this.replacePage1.Name = "replacePage1";
            this.replacePage1.Size = new System.Drawing.Size(478, 361);
            this.replacePage1.TabIndex = 0;
            // 
            // tpTemplate
            // 
            this.tpTemplate.Controls.Add(this.templatePage1);
            this.tpTemplate.Location = new System.Drawing.Point(4, 25);
            this.tpTemplate.Name = "tpTemplate";
            this.tpTemplate.Size = new System.Drawing.Size(484, 367);
            this.tpTemplate.TabIndex = 2;
            this.tpTemplate.Text = "Template";
            this.tpTemplate.UseVisualStyleBackColor = true;
            // 
            // templatePage1
            // 
            this.templatePage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templatePage1.Location = new System.Drawing.Point(0, 0);
            this.templatePage1.Name = "templatePage1";
            this.templatePage1.Size = new System.Drawing.Size(192, 71);
            this.templatePage1.TabIndex = 0;
            // 
            // ToolBodyRight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ToolBodyRight";
            this.Size = new System.Drawing.Size(492, 396);
            this.tabControl1.ResumeLayout(false);
            this.tpMatch.ResumeLayout(false);
            this.tpReplace.ResumeLayout(false);
            this.tpTemplate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpMatch;
        private System.Windows.Forms.TabPage tpReplace;
        private System.Windows.Forms.TabPage tpTemplate;
        private MatchResultPage matchResult1;
        private Pages.ReplacePage replacePage1;
        private Pages.TemplatePage templatePage1;
    }
}
