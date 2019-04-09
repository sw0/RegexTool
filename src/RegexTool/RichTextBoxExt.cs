using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RegexTool
{
    static class RichTextBoxExt
    {
        public static void SetRefreshable(this RichTextBox rich, bool allowOrNot)
        {
            WMHelper.SendMessage(rich.Handle,
                WMHelper.WM_SETREDRAW,
                allowOrNot ? 1 : 0, 0);
        }

        public static int GetScrollPos(this RichTextBox rich)
        {
            return WMHelper.GetScrollPos(rich.Handle, WMHelper.SB_VERT);
        }

        public static int SetScrollPos(this RichTextBox rich, int pos)
        {
            return WMHelper.SetScrollPos(rich.Handle, WMHelper.SB_VERT, pos, false);
        }
    }
}
