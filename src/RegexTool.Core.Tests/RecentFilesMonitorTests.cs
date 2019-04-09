using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexTool.Core.Tests
{
    [TestClass]
    public class RecentFilesMonitorTests
    {
        private IRecentFilesMonitor GetRecentFileMonitor()
        {
            return new RecentFilesMonitor("recent.txt", 15);
        }

        [TestMethod]
        public void RecentFilesMoniter_Clear_Test_Success()
        {
            var recent = GetRecentFileMonitor();
            recent.Clear();
            Assert.AreEqual(0, recent.RecentItems.Count);
        }


        [TestMethod]
        public void RecentFilesMoniter_Add_Test_Success()
        {
            var recent = GetRecentFileMonitor();
            recent.Clear();

            recent.Add("d:\\abc.txt");
            recent.Add("d:\\aBc.txt");
            recent.Add("d:\\a12314c.txt");

            Assert.AreEqual(2, recent.RecentItems.Count);
        }


        [TestMethod]
        public void RecentFilesMoniter_Add_ExceedMax_Test_Success()
        {
            var recent = GetRecentFileMonitor();
            recent.Clear();

            recent.Add("d:\\a1.txt");
            recent.Add("d:\\a2.txt", false);
            recent.Add("d:\\a3.txt");
            recent.Add("d:\\a4.txt");
            recent.Add("d:\\a4.txt");
            recent.Add("d:\\a4.txt");
            recent.Add("d:\\a5.txt");
            recent.Add("d:\\a6.txt");
            recent.Add("d:\\a7.txt");
            recent.Add("d:\\a8.txt");
            recent.Add("d:\\a9.txt");
            recent.Add("d:\\a10.txt");
            recent.Add("d:\\a11txt");
            recent.Add("d:\\a12.txt");
            recent.Add("d:\\a13.txt");
            recent.Add("d:\\a14.txt");
            recent.Add("d:\\a15.txt");
            recent.Add("d:\\a16.txt");
            recent.Add("d:\\a17.txt");

            Assert.AreEqual(recent.MaxItemsCount, recent.RecentItems.Count);
        }
    }
}
