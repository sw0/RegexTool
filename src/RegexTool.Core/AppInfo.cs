using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RegexTool.Core
{
    public class StartUpInfo
    {
        public string AppId { get; private set; }
        //public string MAC { get; private set; }
        //public string CPUNumber { get; private set; }
        //public string AppVersion { get; private set; }
        //public string RuntimeVersion { get; private set; }
        //public string ComputerName { get; private set; }
        //public string CDriveSerialNumber { get; private set; }
        //public string PublicKeyHash { get; private set; }
        //public string SN { get; private set; }
        private string MAC;
        private string CPUNumber;
        private string AppVersion;
        private string RuntimeVersion;
        private string ComputerName;
        private string CDriveSerialNumber;
        private string PublicKeyHash;
        //private string OS;
        private string SN;

        private StartUpInfo()
        {
        }

        public StartUpInfo(AppInfo appInfo)
        {
            this.AppId = appInfo.AppId;
            this.MAC = appInfo.MAC;
            this.ComputerName = appInfo.ComputerName;
            this.AppVersion = appInfo.AppVersion;
            this.RuntimeVersion = appInfo.RuntimeVersion;

            SysInfoHelper sih = new SysInfoHelper();
            this.CDriveSerialNumber = sih.GetDiskVolumeSerialNumber(null);
            this.CPUNumber = sih.GetCpu();

            var ini = new IniFileOperator(IniFileOperator.IniFileName);
            //Serial Number
            string sn = ini.ReadValue("Application", "SN", "");

            var encdec = new EncDec();
            this.PublicKeyHash = encdec.GetMd5Hash(AppHelper.STR_PUBLIC_KEY);
            this.SN = sn;
        }

        public string ToQueryString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("AppId:{0}", this.AppId).AppendLine()
                .AppendFormat("MAC:{0}", this.MAC).AppendLine()
                .AppendFormat("CN:{0}", this.ComputerName).AppendLine()
                .AppendFormat("AppVersion:{0}", this.AppVersion).AppendLine()
                .AppendFormat("RuntimeVersion:{0}", this.RuntimeVersion).AppendLine()
                .AppendFormat("CDSN:{0}", this.CDriveSerialNumber).AppendLine()
                .AppendFormat("CPU:{0}", this.CPUNumber).AppendLine()
                .AppendFormat("SN:{0}", SN);

            string s = sb.ToString();

            RSAPublicKey rsaPublic = RSAPublicKey.FromXmlString(AppHelper.STR_PUBLIC_KEY);
            Byte[] bs = RSAHelper.Encrypt(Encoding.UTF8.GetBytes(s), rsaPublic);
            var result = Convert.ToBase64String(bs);
#if DEBUG
            Debug.WriteLine("Encrypted and encoded with base64:" + result);
            var bs2 = Convert.FromBase64String(result);
            var decryptedBytes = RSAHelper.Decrypt(bs2, RSAPrivateKey.FromXmlString(AppHelper.STR_PRIVATE_KEY));
            var original = Encoding.UTF8.GetString(decryptedBytes);

            if (original != s)
            {
                throw new Exception("failed to decrypt");
            }
#endif
            return result;
        }


        internal bool SNExists()
        {
            return false == string.IsNullOrWhiteSpace(SN);
        }
    }

    public class AppInfo
    {
        public string MAC { get; set; }
        public string ComputerName { get; set; }
        public string AppVersion { get; set; }
        public string AppId { get; set; }
        public string RuntimeVersion { get; set; }
        public string SN { get; set; }
        //public string OS { get; set; }

        private string ToBase64(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return Convert.ToBase64String(bytes);
            }
            return string.Empty;
        }

        /// <summary>
        /// this method is used when client cannot connect to the internet or service is broken.
        /// </summary>
        /// <returns></returns>
        public bool SNVerifyLocally()
        {
            if (string.IsNullOrEmpty(this.SN)) return false;

            try
            {
                RSAPublicKey rsaPublic = RSAPublicKey.FromXmlString(AppHelper.STR_PUBLIC_KEY);
                byte[] decryptBytes = RSAHelper.Decrypt(Convert.FromBase64String(this.SN), rsaPublic);
                string resultDecrypted = Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);

                string[] sns = resultDecrypted.Split(new char[] { '|' }, 3);

                if (sns.Length == 2 || sns.Length == 3)
                {
                    var localHash = sns[1];

                    var tmp = string.Format("{0}-{1}-{2}", this.AppId.Substring(0, 8), this.MAC, sns[0]).ToLower();
                    var data = (new EncDec()).GetMd5Hash(tmp);

                    if (data.ToLower() == localHash.ToLower())
                    {
                        if (sns.Length == 3)
                        {
                            DateTime expireDate = DateTime.Parse(sns[2]);

                            if (DateTime.Now > expireDate)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        //the SN value might be modified by client user manually.
                        //Just keep the value that user modified.
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public string ToHttpString4Headers()
        {
            if (string.IsNullOrWhiteSpace(AppId))
            {
                //probably failed to get the app info
                return string.Empty;
            }

            return string.Format("AppId: {0}\r\nAppMAC: {1}\r\nCName: {2}\r\nAppVersion: {3}\r\nRuntimeVersion: {4}\r\n",
                this.AppId ?? string.Empty,
                ToBase64(MAC),
                ToBase64(this.ComputerName),
                this.AppVersion ?? string.Empty,
                this.RuntimeVersion);
        }
    }
}
