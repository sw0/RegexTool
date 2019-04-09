using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RegexTool.Pages
{
    public partial class ResultBar : UserControl
    {
        private Action<string> CopyToSource = null;
        internal ITextProvider TextProvider { get; set; } 

        public ResultBar()
        {
            InitializeComponent();
        }

        private void btnCopyToSource_Click(object sender, EventArgs e)
        {
            if (CopyToSource != null && TextProvider != null)
            {
                CopyToSource(TextProvider.GetText());
            }
        }
    }
}
