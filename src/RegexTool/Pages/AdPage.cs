using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using RegexTool.Core;

namespace RegexTool.Pages
{
    public partial class AdPage : UserControl
    {
        public AdPage()
        {
            InitializeComponent();

#if DEBUG
#else
            this.wbBrowser.IsWebBrowserContextMenuEnabled = false;
            this.wbBrowser.ScrollBarsEnabled = false;
            this.wbBrowser.ScriptErrorsSuppressed = true;
#endif

            Action act = LoadAds;
            act.BeginInvoke(null, null);
        }

        private void LoadAds()
        {
            //fid is short for "FromId", and 1 here means "RegexTool".
            var additionalHeaders = AppHelper.GetAppInfo().ToHttpString4Headers();
            additionalHeaders += AppHelper.GetLangHeaderString();
            this.wbBrowser.Navigate(RegexToolHelper.URL_AD, "_self", Encoding.UTF8.GetBytes(""), additionalHeaders);
        }

        public void InitAds()
        {
            Action act = LoadAds;
            act.BeginInvoke(null, null);
        }

        public void NavToBlank(int delayInSecond = 0)
        {
            try
            {
                if (delayInSecond > 0)
                    Thread.Sleep(delayInSecond * 1000);

                this.wbBrowser.Navigate("about:blank");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Navigate(string url)
        {
            if (!string.IsNullOrEmpty(url))
                this.wbBrowser.Navigate(url);
        }
    }
}
