using RegexTool.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RegexTool
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        UpdateInfo UpdateInfo
        {
            get
            {
                return this.Tag as UpdateInfo;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.UpdateInfo != null)
                {
                    string url = this.UpdateInfo.UpdateUrl;

#if DEBUG
                    if (string.IsNullOrEmpty(url))
                    {
                        url = "http://dev.tools.tainisoft.com";
                    }
#endif
                    Process.Start(url);
                }
            }
            catch
            {
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            if (this.UpdateInfo == null)
            {
                throw new Exception("Error 20130826");
            }
            else
            {
                txtUpdateInfo.Text = this.UpdateInfo.ToUpdateInfoText();

                if (this.UpdateInfo.ForceUpdate)
                {
                    btnCancel.Visible = false;
                }
                btnOK.Select();
            }
        }
    }
}
