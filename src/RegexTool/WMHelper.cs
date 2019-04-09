using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RegexTool
{
    /// <summary>
    /// windows message helper
    /// </summary>
    public class WMHelper
    {
        #region -- consts --

        public const int WM_SETREDRAW = 0x0B;

        public const int WM_HSCROLL = 0x0114;

        public const int WM_VSCROLL = 0x0115;

        public const int SB_VERT = 0x0001;
        #endregion

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            IntPtr hWnd,　　　// handle to destination window 
            int Msg,　　　 // message 
            int wParam,　 // first message parameter 
            int lParam   // second message parameter 
            );

        [DllImport("user32")]
        public static extern int GetScrollPos(IntPtr hWnd, Int32 nBar);

        [DllImport("user32")]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);
    }
}
