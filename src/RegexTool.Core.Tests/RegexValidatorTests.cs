using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace RegexTool.Core.Tests
{
    [TestClass]
    public class RegexValidatorTests
    {
        [TestMethod]
        public void IsValid_Test_Invalid()
        {
            var pattern = "(";

            var result = RegexValidator.IsValid(pattern);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_EmptyPattern_Valid()
        {
            var pattern = "";

            var result = RegexValidator.IsValid(pattern);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_Simple_Valid()
        {
            var pattern = "\\d+";

            var result = RegexValidator.IsValid(pattern);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MyTestMethod()
        {
        }
    }
}
