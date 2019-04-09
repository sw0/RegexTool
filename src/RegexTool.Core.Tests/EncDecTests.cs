using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace RegexTool.Core.Tests
{
#if DEBUG
    [TestClass]
    public class EncDecTests
    {
        private EncDec _instance = new EncDec();

        //[AssemblyInitialize()]
        //public static void AssemblyInitializeMethod(TestContext context) {
        //    Debug.WriteLine("AssemblyInitializeMethod called");
        //}
        static RSACryptoServiceProvider _rsaProvider = null;
        static private string _privateKey = string.Empty;
        static private string _publicKey = string.Empty;

        [ClassInitialize]
        public static void TestClassInit(TestContext context)
        {
            _rsaProvider = new RSACryptoServiceProvider();
            _privateKey = _rsaProvider.ToXmlString(true);
            _publicKey = _rsaProvider.ToXmlString(false);
            Debug.WriteLine("PrivateKey:" + _privateKey);
            Debug.WriteLine("PublicKey:" + _publicKey);
        }

        [ClassCleanup]
        public static void TestClassCleanup()
        {
            _rsaProvider = null;
            _privateKey = null;
            _publicKey = null;
        }

        [TestMethod]
        public void ED_RSA_CreateKeys_Success()
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            var privateKey = provider.ToXmlString(true);
            var publicKey = provider.ToXmlString(false);

            Assert.IsNotNull(privateKey);
            Assert.IsTrue(privateKey.Length > 0);
            Debug.WriteLine("PrivateKey:" + privateKey);
            Assert.IsNotNull(publicKey);
            Assert.IsTrue(publicKey.Length > 0);
            Debug.WriteLine("PublicKey:" + publicKey);
        }

        [TestMethod]
        public void ED_RSA_Encrypt_Descript_Success()
        {
            var ed = new EncDec();
            string input = "Hello, my name is Shaowei Lin, I am from China and now working in linsw.com.";
            string strEncrypt = ed.RSAEncrypt(_publicKey, input);
            Debug.WriteLine("Encrypt: " + strEncrypt);

            string strDecrypted = ed.RSADecrypt(_privateKey, strEncrypt);
            Debug.WriteLine("Decrypt: " + strDecrypted);
            Assert.AreEqual(input, strDecrypted);
        }

        [TestMethod]
        public void ED_GetMd5Hash_Test()
        {
            string str = "123456";

            var result = _instance.GetMd5Hash(str);
            Assert.IsNotNull(result);
            Debug.WriteLine(result);

            for (int i = 0; i < 80; i++)
            {
                str = str + "123456";
            }
            result = _instance.GetMd5Hash(str);
            Assert.IsNotNull(result);
            Debug.WriteLine(result);
        }
    }
#endif
}
