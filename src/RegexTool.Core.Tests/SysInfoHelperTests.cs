using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Diagnostics;

namespace RegexTool.Core.Tests
{
    [TestClass]
    public class SysInfoHelperTests
    {
        private ISysInfoHelper _sysInfoHelper = new SysInfoHelper();
#if DEBUG
        //Referrence: http://www.cnblogs.com/kingboy/archive/2008/10/31/1025399.html
#endif
        [TestMethod]
        public void SIH_GetMac_Successfully()
        {
            var mac = _sysInfoHelper.GetMAC();

            Debug.WriteLine(mac);
            Assert.IsFalse(string.IsNullOrWhiteSpace(mac), "failed to get the MAC of current computer.");
        }

        [TestMethod]
        public void SIH_GetComputerName_Successfully()
        {
            var computerName = _sysInfoHelper.GetComputerName();

            Debug.WriteLine(computerName);

            Assert.IsFalse(string.IsNullOrWhiteSpace(computerName), "failed to get computer full name.");
        }


        [TestMethod]
        public void SIH_GetDiskVolumeSerialNumber_Successfully()
        {
            var discVoumeSerialNumber = _sysInfoHelper.GetDiskVolumeSerialNumber("c:");
            var discVoumeSerialNumber2 = _sysInfoHelper.GetDiskVolumeSerialNumber("D:");

            Debug.WriteLine("serial number for C: is " + discVoumeSerialNumber);
            Debug.WriteLine("serial number for D: is " + discVoumeSerialNumber2);

            Assert.IsFalse(string.IsNullOrWhiteSpace(discVoumeSerialNumber), "failed to get disk serial number for C:.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(discVoumeSerialNumber2), "failed to get disk serial number for D:.");
        }

#if DEBUG
        [ExpectedException(typeof(ManagementException))]
#endif
        [TestMethod]
        public void SIH_GetDiskVolumeSerialNumber_Drive_K_Not_Exists()
        {
            var discVoumeSerialNumber = _sysInfoHelper.GetDiskVolumeSerialNumber("K:");

            Debug.WriteLine("serial number for K: is " + discVoumeSerialNumber);

            Assert.IsFalse(string.IsNullOrWhiteSpace(discVoumeSerialNumber), "failed to get disk serial number for K:.");
        }

        [TestMethod]
        public void SIH_GetDiskId_Successfully()
        {
            var diskId = _sysInfoHelper.GetDiskId();

            Debug.WriteLine("diskId: " + diskId);

            Assert.IsFalse(string.IsNullOrWhiteSpace(diskId), "failed to get Disk ID.");
        }

        [TestMethod]
        public void SIH_GetCPU_Successfully()
        {
            var cpu = _sysInfoHelper.GetCpu();

            Debug.WriteLine("cpu: " + cpu);

            Assert.IsFalse(string.IsNullOrWhiteSpace(cpu), "failed to get CPU.");
        }

        [TestMethod]
        public void SIH_GetOS_Successfully()
        {
            var os = _sysInfoHelper.GetOS();

            Debug.WriteLine("OS: " + os);

            Assert.IsFalse(string.IsNullOrWhiteSpace(os), "failed to get OS.");
        }

        [TestMethod]
        public void SIH_GetUserAgentForIE_Successfully()
        {
            var ua = _sysInfoHelper.GetUserAgentForIE();

            Debug.WriteLine("UserAgent: " + ua);

            Assert.IsFalse(string.IsNullOrWhiteSpace(ua), "failed to get UserAgent.");
        }
    }
}
