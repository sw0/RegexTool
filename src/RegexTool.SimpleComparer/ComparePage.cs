using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            List<string> a1, a2;
            if (((Button)sender).Text.Contains("without loading"))
            {
                try
                {
                    a1 = File.ReadAllLines(compareItem1.SourceFileOrUrl).ToList();
                    a2 = File.ReadAllLines(compareItem2.SourceFileOrUrl).ToList();
                }
                catch (Exception ex)
                {
                    lblResult.Text = "failed when opening files: " + ex.Message;
                    return;
                }
            }
            else
            {
                if (false == compareItem1.HasContent() || false == compareItem2.HasContent())
                {
                    lblResult.Text = "No data to compare.";
                    return;
                }

                txtResultLNR.Clear();
                txtResultRNL.Clear();

                lblResult.Text = "Please click Compare button to compare the list.";
                a1 = this.compareItem1.GetItems();
                a2 = this.compareItem2.GetItems();
            }

            var r1 = a1.Except(a2, StringComparer.CurrentCultureIgnoreCase).Where(str => str != string.Empty).ToList();
            var r2 = a2.Except(a1, StringComparer.CurrentCultureIgnoreCase).Where(str => str != string.Empty).ToList();
            var rIntersected = a1.Intersect(a2).ToList();

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
            foreach (var item in rIntersected)
            {
                txtIntersected.AppendText(item);
                txtIntersected.AppendText(Environment.NewLine);
            }

            var r1c = r1.Count();
            var r2c = r2.Count();

            string s = string.Format("{0} : {1} >> {2} (Matched: {3}) {4}", a1.Count, a2.Count,
                r1.Count(), rIntersected.Count(), r2.Count());
            lblResult.Text = s;

            if (r1c > 0) tbCompareResult.SelectedTab = tabPage1;
            else if (r2c > 0) tbCompareResult.SelectedTab = tabPage2;
        }
    }
}
