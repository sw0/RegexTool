using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RegexTool
{
    public class RegexToolHelper
    {
        public static void OpenOnlineDocument()
        {
            Process.Start("http://tools.linsw.com/regextool/help");
        }

        public static void OpenHomepage()
        {
            Process.Start("http://tools.linsw.com/regextool");
        }

        public static void OpenNotepad()
        {
            Process.Start("notepad.exe");
        }
    }
}
