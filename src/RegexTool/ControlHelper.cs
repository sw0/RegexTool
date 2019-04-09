using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RegexTool
{
    public class ControlHelper
    {
        public static void SetDragDropAbility(TextBoxBase textbox, bool allowOrNot)
        {
            if (textbox == null) throw new ArgumentNullException("textbox");

            if (allowOrNot)
            {
                textbox.AllowDrop = true;
                textbox.DragDrop += Textbox_DragDrop;
                textbox.DragEnter += Textbox_DragEnter;
                textbox.DragOver += Textbox_DragOver;
            }
            else
            {
                textbox.AllowDrop = false;
                textbox.DragDrop -= Textbox_DragDrop;
                textbox.DragEnter -= Textbox_DragEnter;
                textbox.DragOver -= Textbox_DragOver;
            }
        }

        private static void Textbox_DragDrop(object sender, DragEventArgs e)
        {
            var txt = sender as TextBoxBase;
            if (txt == null) throw new Exception("txt is null");

            var textDrag = e.Data.GetData(DataFormats.Text) as string;

            var txtPoint = txt.PointToScreen(new Point(0, 0));
            var pointInsideTextbox = new Point(e.X - txtPoint.X, e.Y - txtPoint.Y);

            var idx = txt.GetCharIndexFromPosition(pointInsideTextbox);

            if (!string.IsNullOrEmpty(textDrag))
            {
                txt.Text = txt.Text.Insert(idx, textDrag);
                txt.SelectionStart = idx;
                txt.SelectionLength = textDrag.Length;
            }
        }

        private static void Textbox_DragEnter(object sender, DragEventArgs e)
        {
            var txt = sender as TextBoxBase;
            if (txt == null) throw new Exception("txt is null");

            var txtPoint = txt.PointToScreen(new Point(0, 0));
            var pointInsideTextbox = new Point(e.X - txtPoint.X, e.Y - txtPoint.Y);
            var idx = txt.GetCharIndexFromPosition(pointInsideTextbox);
            txt.SelectionStart = idx;
            txt.SelectionLength = 0;

            txt.Focus();

            //e.Effect = e.Data.GetDataPresent(DataFormats.Text, true) ?
            //    (((e.KeyState & 8) > 0) ? DragDropEffects.Copy : DragDropEffects.Move)
            //    : DragDropEffects.None;

            e.Effect = e.Data.GetDataPresent(DataFormats.Text, true) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private static void Textbox_DragOver(object sender, DragEventArgs e)
        {
            var txt = sender as TextBoxBase;
            if (txt == null) throw new Exception("txt is null");

            var txtPoint = txt.PointToScreen(new Point(0, 0));
            var pointInsideTextbox = new Point(e.X - txtPoint.X, e.Y - txtPoint.Y);
            var idx = txt.GetCharIndexFromPosition(pointInsideTextbox);
            txt.SelectionStart = idx;
            txt.SelectionLength = 0;
        }
    }
}
