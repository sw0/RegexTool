using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RegexTool
{
    public class SmartTabControlMenuItemEventArgs : EventArgs
    {
        public TabPage TabPage { get; set; }
    }

    public class SmartTabControl : TabControl
    {
        public readonly ContextMenuStrip TabCMS = new ContextMenuStrip();

        public SmartTabControl()
        {
            //TabCMS.Items.Add("Close Project");
            //TabCMS.Items.Add("Close without save");
            //TabCMS.Items.Add("New project");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Point pt = new Point(e.X, e.Y);
            TabPage tp = GetTabPageByTab(pt);

            if (tp != null)
            {
                DoDragDrop(tp, DragDropEffects.All);
            }
            if (e.Button == MouseButtons.Right)
            {
                TabCMS.Show(this, e.Location);
            }
        }

        private TabPage GetTabPageByTab(Point pt)
        {
            TabPage tp = null;

            for (int i = 0; i < TabPages.Count; i++)
            {
                if (GetTabRect(i).Contains(pt))
                {
                    tp = TabPages[i];
                    break;
                }
            }

            return tp;
        }
    }
}
