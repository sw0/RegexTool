using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Diagnostics;

namespace RegexTool.Core.Tests
{
    [TestClass]
    public class RSAHelperTests
    {
        private static RSACryptoServiceProvider _rsaProvider = null;
        static RSAPublicKey _rsaPublic = null;
        static RSAPrivateKey _rsaPrivate = null;
        static string _rsaPrivateXml = null;
        static string _rsaPublicXml = null;

        [ClassInitialize]
        public static void TestClassInit(TestContext context)
        {
            _rsaProvider = new RSACryptoServiceProvider();

            string publicKeyXml = _rsaProvider.ToXmlString(false);
            string privateKeyXml = _rsaProvider.ToXmlString(true);
            _rsaPublicXml = publicKeyXml;
            _rsaPrivateXml = privateKeyXml;

            _rsaPublic = RSAPublicKey.FromXmlString(publicKeyXml);
            _rsaPrivate = RSAPrivateKey.FromXmlString(privateKeyXml);
        }

        [ClassCleanup]
        public static void TestClassCleanup()
        {
            _rsaProvider = null;
        }

        [TestInitialize]
        public void TestInit()
        {
            Debug.WriteLine("PrivateKeyXml:" + _rsaPrivateXml);
            Debug.WriteLine("PublicKeyXml:" + _rsaPublicXml);
        }

        [TestMethod]
        public void RSAHelper_Encrypt_By_Private_Decrypt_Success()
        {
            string input = "Hello, this is Shawn, I am learning about RSA by using BigInteger class";

            var bytes = Encoding.UTF8.GetBytes(input);
            var resultBytes = RSAHelper.Encrypt(bytes, _rsaPrivate);

            string result = Convert.ToBase64String(resultBytes);

            Debug.WriteLine("Encrypt by private key: ");
            Debug.WriteLine(result);

            byte[] decryptBytes = RSAHelper.Decrypt(Convert.FromBase64String(result), _rsaPublic);
            string resultDecrypted = Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);

            Debug.WriteLine("Decrypt by public key: ");
            Debug.WriteLine(resultDecrypted);

            Assert.AreEqual(input, resultDecrypted);
        }

        [TestMethod]
        public void RSAHelper_Encrypt_By_Public_Decrypt_Success()
        {
            string input = "Hello, this is Shawn, I am learning about RSA by using BigInteger class";

            var bytes = Encoding.UTF8.GetBytes(input);
            var resultBytes = RSAHelper.Encrypt(bytes, _rsaPublic);

            string result = Convert.ToBase64String(resultBytes);

            Debug.WriteLine("Encrypt by public key: ");
            Debug.WriteLine(result);

            EncDec ed = new EncDec();
            string encryptedByED = ed.RSAEncrypt(_rsaPublicXml, input);
            Debug.WriteLine("WARNNING: Encrypt result are {0} equal between RSAHelper and EncDec class.", encryptedByED == result ? "" : " NOT ", "");

            byte[] decryptBytes = RSAHelper.Decrypt(Convert.FromBase64String(result), _rsaPrivate);
            string resultDecrypted = Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);

            Debug.WriteLine("Decrypt by public key: ");
            Debug.WriteLine(resultDecrypted);

            string resultED = ed.RSADecrypt(_rsaPrivateXml, encryptedByED);
            Assert.AreEqual(input, resultDecrypted);
            Assert.AreEqual(input, resultED);
        }
    }
}
