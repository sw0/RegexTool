using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RegexTool
{
    public class RegexToolHelper
    {
        internal const string URL_HELP = "http://tools.tainisoft.com/regextool/help";
        internal const string URL_HOMEPAGE = "http://tools.tainisoft.com/regextool";
#if DEBUG
        internal const string ULR_APP_STARTUP = "http://dev.tools.tainisoft.com/regextool/appstart.ashx";
#else
        internal const string ULR_APP_STARTUP = "http://tools.tainisoft.com/regextool/appstart.ashx";
#endif
        //"http://tools.tainisoft.com/RegexTool/donate?appid={0}&lan={1}";
        //string url = Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cDovL3Rvb2xzLmxpbnN3LmNvbS9SZWdleFRvb2wvZG9uYXRlP2FwcGlkPXswfSZsYW49ezF9"));
        /// <summary>
        /// http://tools.tainisoft.com/RegexTool/donate?appid={0}&lan={1}
        /// </summary>
        public const string ULR_DONATE = "http://tools.tainisoft.com/RegexTool/donate?appid={0}&lan={1}";
        public const string URL_AD = "http://tools.tainisoft.com/RegexTool/ad?fid=1";


        /**********************************************
         * string data = 
         * URL_HELP=http://tools.tainisoft.com/regextool/help
         * URL_HOMEPAGE=http://tools.tainisoft.com/regextool
         * ULR_APP_STARTUP=http://tools.tainisoft.com/regextool/appstart.ashx
         * 
         * 
         * 
         * 
         * 
         * *********************************************/
        public static void OpenOnlineDocument()
        {
            Process.Start("http://tools.tainisoft.com/regextool/help");
        }

        public static void OpenHomepage()
        {
            Process.Start("http://tools.tainisoft.com/regextool");
        }

        public static void OpenNotepad()
        {
            Process.Start("notepad.exe");
        }
    }
}
