namespace RegexTool.Pages
{
    partial class ResultBar
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
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCopyToSource = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtTransformedSavePath = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.btnCopyToSource, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSave, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtTransformedSavePath, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(515, 33);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // btnCopyToSource
            // 
            this.btnCopyToSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyToSource.Location = new System.Drawing.Point(3, 3);
            this.btnCopyToSource.Name = "btnCopyToSource";
            this.btnCopyToSource.Size = new System.Drawing.Size(89, 24);
            this.btnCopyToSource.TabIndex = 3;
            this.btnCopyToSource.Text = "To Source";
            this.btnCopyToSource.UseVisualStyleBackColor = true;
            this.btnCopyToSource.Click += new System.EventHandler(this.btnCopyToSource_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(449, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 24);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "SaveProject";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // txtTransformedSavePath
            // 
            this.txtTransformedSavePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTransformedSavePath.Location = new System.Drawing.Point(98, 3);
            this.txtTransformedSavePath.Name = "txtTransformedSavePath";
            this.txtTransformedSavePath.Size = new System.Drawing.Size(345, 22);
            this.txtTransformedSavePath.TabIndex = 4;
            // 
            // ResultBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Name = "ResultBar";
            this.Size = new System.Drawing.Size(515, 33);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnCopyToSource;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtTransformedSavePath;
    }
}
