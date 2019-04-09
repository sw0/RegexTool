using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexTool.Core
{
    public class AppStartResponse
    {
        public string ClientIP { get; set; }
        public UpdateInfo UpdateInfo { get; set; }
    }

    public class AppStartRequest
    {
        //public StartUpInfo StartUpData { get; set; }
        public UpdateInfo UpdateInfo { get; set; }
    }

    public class UpdateInfo
    {
        /// <summary>
        /// the version from request client
        /// </summary>
        public string ClientVersion { get; set; }
        /// <summary>
        /// the version that on server
        /// </summary>
        public string LatestVersion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AppType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool ForceUpdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool PopUpdateMessage { get; set; }

        /// <summary>
        /// the url for downloading of new version of app
        /// </summary>
        public string UpdateUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        public string Note { get; set; }

        public string ToUpdateInfoText()
        {
            if (string.IsNullOrEmpty(this.Message))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Good News! ")
                .Append("You got a new version of RegexTool. ")
                .AppendFormat("Please download the latest tool from {0}, the latest version number is {1}", this.UpdateUrl, this.LatestVersion);
                return sb.ToString();
            }
            else
            {
                return this.Message;
            }
        }
    }
}
