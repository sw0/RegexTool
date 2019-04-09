using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RegexTool.Core;

namespace RegexTool.SimpleComparer
{
    public partial class ComparePage : UserControl
    {
        public ComparePage()
        {
            InitializeComponent();
            //spHoriz.Dock = DockStyle.Fill;
            lblResult.Text = "Please click Compare button to compare the list.";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cmsLNR.InitTextContextMenuItems(txtResultLNR,
                 TextContextMenuType.SimpleText | TextContextMenuType.SaveContent);

            cmsRNL.InitTextContextMenuItems(txtResultRNL,
                 TextContextMenuType.SimpleText | TextContextMenuType.SaveContent);
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (false == compareItem1.HasContent() || false == compareItem2.HasContent())
            {
                lblResult.Text = "No data to compare.";
                return;
            }
            txtResultLNR.Clear();
            txtResultRNL.Clear();

            lblResult.Text = "Please click Compare button to compare the list.";
            var a1 = this.compareItem1.GetItems();
            var a2 = this.compareItem2.GetItems();

            var r1 = a1.Except(a2, StringComparer.CurrentCultureIgnoreCase);
            var r2 = a2.Except(a1, StringComparer.CurrentCultureIgnoreCase);

            foreach (var item in r1)
            {
                txtResultLNR.AppendText(item);
                txtResultLNR.AppendText(Environment.NewLine);
            }
            foreach (var item in r2)
            {
                txtResultRNL.AppendText(item);
                txtResultRNL.AppendText(Environment.NewLine);
            }

            var r1c = r1.Count();
            var r2c = r2.Count();

            if (r1c == 0 && r2c == 0)
            {
                string s = string.Format("Got {0} items in left, and {1} items in right. " +
                    "And match!", a1.Count, a2.Count);
                lblResult.Text = s;
            }
            else
            {
                string s = string.Format("Got {0} items in left, and {1} items in right. " +
                    "There are {2} items not in right and {3} items not in left.",
                    a1.Count, a2.Count, r1c, r2c);
                lblResult.Text = s;
            }

            if (r1c > 0) tbCompareResult.SelectedTab = tabPage1;
            else if (r2c > 0) tbCompareResult.SelectedTab = tabPage2;
        }
    }
}
