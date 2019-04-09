using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace RegexTool.Core
{
//#if DEBUG
    public class EncDec
//#else
//    internal class EncDec
//#endif
    {
        /// <summary>
        /// 生成公私钥
        /// </summary>
        /// <param name="privateKeyPath"></param>
        /// <param name="publicKeyPath"></param>
        public void RSAKey(string privateKeyPath, string publicKeyPath)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                using (StreamWriter sw = new StreamWriter(privateKeyPath))
                {
                    sw.Write(provider.ToXmlString(true));
                    sw.Close();
                }
                using (StreamWriter sw = new StreamWriter(publicKeyPath))
                {
                    sw.Write(provider.ToXmlString(false));
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                throw;
            }
        }


        //Reference: http://msdn.microsoft.com/en-us/library/system.security.cryptography.md5(v=vs.80).aspx
        //http://www.cnblogs.com/kevinShan/archive/2010/01/03/1638211.html
        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        public string GetMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="input">MD5加密后的数据</param>
        /// <returns>RSA公钥加密后的数据</returns>
        public string RSAEncrypt(string xmlPublicKey, string input)
        {
            string result;

            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPublicKey);
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] encrypted = provider.Encrypt(bytes, false);
                result = Convert.ToBase64String(encrypted);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="input">待解密的数据</param>
        /// <returns>解密后的结果</returns>
        public string RSADecrypt(string xmlPrivateKey, string input)
        {
            string oringinal;

            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPrivateKey);
                byte[] rgb = Convert.FromBase64String(input);
                byte[] buffer2 = provider.Decrypt(rgb, false);
                oringinal = Encoding.UTF8.GetString(buffer2);
            }
            catch (Exception ex)
            {
                throw;
            }

            return oringinal;
        }

        /// <summary>
        /// 对MD5加密后的密文进行签名
        /// </summary>
        /// <param name="p_strKeyPrivate">私钥</param>
        /// <param name="m_strHashbyteSignature">MD5加密后的密文</param>
        /// <returns></returns>
        public string SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature)
        {
            byte[] rgbHash = Convert.FromBase64String(m_strHashbyteSignature);
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
            formatter.SetHashAlgorithm("MD5");
            byte[] inArray = formatter.CreateSignature(rgbHash);

            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="p_strKeyPublic">公钥</param>
        /// <param name="p_strHashbyteDeformatter">待验证的用户名</param>
        /// <param name="p_strDeformatterData">注册码</param>
        /// <returns></returns>
        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData)
        {
            try
            {
                byte[] rgbHash = Convert.FromBase64String(p_strHashbyteDeformatter);
                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.FromXmlString(p_strKeyPublic);
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
                deformatter.SetHashAlgorithm("MD5");
                byte[] rgbSignature = Convert.FromBase64String(p_strDeformatterData);
                if (deformatter.VerifySignature(rgbHash, rgbSignature))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 读注册表中指定键的值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>返回键值</returns>
        private string ReadReg(string key)
        {

            string temp = "";

            try
            {

                RegistryKey myKey = Registry.LocalMachine;

                RegistryKey subKey = myKey.OpenSubKey(@"SOFTWARE/JX/Register");

                temp = subKey.GetValue(key).ToString();
                subKey.Close();
                myKey.Close();
                return temp;
            }
            catch (Exception)
            {
                throw;//可能没有此注册项;
            }
        }

        /// <summary>
        /// 创建注册表中指定的键和值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        private void WriteReg(string key, string value)
        {
            try
            {
                RegistryKey rootKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE/JX/Register");

                rootKey.SetValue(key, value);

                rootKey.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        ///// <summary>
        ///// 初始化注册表，程序运行时调用，在调用之前更新公钥xml
        ///// </summary>
        ///// <param name="path">公钥路径</param>
        //public void InitialReg(string path)        {

        //    Registry.LocalMachine.CreateSubKey(@"SOFTWARE/JX/Register");
        //    Random ra = new Random();

        //    string publickey = this.ReadPublicKey(path);
        //    if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE/JX/Register").ValueCount <= 0)
        //    {
        //        this.WriteReg("RegisterRandom", ra.Next(1, 100000).ToString());
        //        this.WriteReg("RegisterPublicKey", publickey);
        //    }
        //    else
        //    {
        //        this.WriteReg("RegisterPublicKey", publickey);
        //    }
        //}
    }


    public class RSAPublicKey
    {
        public byte[] Modulus;
        public byte[] Exponent;

        public static RSAPublicKey FromXmlString(string xmlString)
        {
            if (string.IsNullOrEmpty(xmlString))
            {
                return null;
            }

            try
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
                {
                    if (!reader.ReadToFollowing("RSAKeyValue"))
                    {
                        return null;
                    }

                    if (reader.LocalName != "Modulus" && !reader.ReadToFollowing("Modulus"))
                    {
                        return null;
                    }
                    string modulus = reader.ReadElementContentAsString();

                    if (reader.LocalName != "Exponent" && !reader.ReadToFollowing("Exponent"))
                    {
                        return null;
                    }
                    string exponent = reader.ReadElementContentAsString();

                    RSAPublicKey publicKey = new RSAPublicKey();
                    publicKey.Modulus = Convert.FromBase64String(modulus);
                    publicKey.Exponent = Convert.FromBase64String(exponent);

                    return publicKey;
                }
            }
            catch
            {
                return null;
            }
        }
    }

    public class RSAPrivateKey
    {
        public byte[] Modulus;
        public byte[] D;

        public static RSAPrivateKey FromXmlString(string xmlString)
        {
            if (string.IsNullOrEmpty(xmlString))
            {
                return null;
            }

            try
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
                {
                    if (!reader.ReadToFollowing("RSAKeyValue"))
                    {
                        return null;
                    }

                    if (reader.LocalName != "Modulus" && !reader.ReadToFollowing("Modulus"))
                    {
                        return null;
                    }
                    string modulus = reader.ReadElementContentAsString();

                    if (reader.LocalName != "D" && !reader.ReadToFollowing("D"))
                    {
                        return null;
                    }
                    string d = reader.ReadElementContentAsString();

                    RSAPrivateKey privateKey = new RSAPrivateKey();
                    privateKey.Modulus = Convert.FromBase64String(modulus);
                    privateKey.D = Convert.FromBase64String(d);

                    return privateKey;
                }
            }
            catch
            {
                return null;
            }
        }
    }

    public class RSAHelper
    {
        private static byte[] Compute(byte[] data, RSAPublicKey publicKey, int blockSize)
        {
            //
            // 公钥加密/解密公式为：ci = mi^e ( mod n )            
            // 
            // 先将 m（二进制表示）分成数据块 m1, m2, ..., mi ，然后进行运算。
            //
            BigInteger e = new BigInteger(publicKey.Exponent);
            BigInteger n = new BigInteger(publicKey.Modulus);

            int blockOffset = 0;
            using (MemoryStream stream = new MemoryStream())
            {
                while (blockOffset < data.Length)
                {
                    int blockLen = Math.Min(blockSize, data.Length - blockOffset);
                    byte[] blockData = new byte[blockLen];
                    Buffer.BlockCopy(data, blockOffset, blockData, 0, blockLen);

                    BigInteger mi = new BigInteger(blockData);
                    BigInteger ci = mi.modPow(e, n);//ci = mi^e ( mod n )

                    byte[] block = ci.getBytes();
                    stream.Write(block, 0, block.Length);
                    blockOffset += blockLen;
                }

                return stream.ToArray();
            }
        }

        private static byte[] Compute(byte[] data, RSAPrivateKey privateKey, int blockSize)
        {
            //
            // 私钥加密/解密公式为：mi = ci^d ( mod n )
            // 
            // 先将 c（二进制表示）分成数据块 c1, c2, ..., ci ，然后进行运算。            
            //
            BigInteger d = new BigInteger(privateKey.D);
            BigInteger n = new BigInteger(privateKey.Modulus);

            int blockOffset = 0;

            using (MemoryStream stream = new MemoryStream())
            {
                while (blockOffset < data.Length)
                {
                    int blockLen = Math.Min(blockSize, data.Length - blockOffset);
                    byte[] blockData = new byte[blockLen];
                    Buffer.BlockCopy(data, blockOffset, blockData, 0, blockLen);

                    BigInteger ci = new BigInteger(blockData);
                    BigInteger mi = ci.modPow(d, n);//mi = ci^d ( mod n )

                    byte[] block = mi.getBytes();
                    stream.Write(block, 0, block.Length);
                    blockOffset += blockLen;
                }

                return stream.ToArray();
            }
        }

        public static byte[] Encrypt(byte[] data, RSAPublicKey publicKey)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (publicKey == null)
            {
                throw new ArgumentNullException("publicKey");
            }

            int blockSize = publicKey.Modulus.Length - 1;
            return Compute(data, publicKey, blockSize);
        }

        public static byte[] Decrypt(byte[] data, RSAPrivateKey privateKey)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (privateKey == null)
            {
                throw new ArgumentNullException("privateKey");
            }

            int blockSize = privateKey.Modulus.Length;
            return Compute(data, privateKey, blockSize);
        }

        public static byte[] Encrypt(byte[] data, RSAPrivateKey privateKey)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (privateKey == null)
            {
                throw new ArgumentNullException("privateKey");
            }

            int blockSize = privateKey.Modulus.Length - 1;
            return Compute(data, privateKey, blockSize);
        }

        public static byte[] Decrypt(byte[] data, RSAPublicKey publicKey)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (publicKey == null)
            {
                throw new ArgumentNullException("publicKey");
            }

            int blockSize = publicKey.Modulus.Length;
            return Compute(data, publicKey, blockSize);
        }



        public static byte[] Sign(byte[] data, RSAPublicKey publicKey, HashAlgorithm hash)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (publicKey == null)
            {
                throw new ArgumentNullException("publicKey");
            }

            if (hash == null)
            {
                throw new ArgumentNullException("hash");
            }

            byte[] hashData = hash.ComputeHash(data);
            byte[] signature = Encrypt(hashData, publicKey);
            return signature;
        }

        public static bool Verify(byte[] data, RSAPrivateKey privateKey, HashAlgorithm hash, byte[] signature)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (privateKey == null)
            {
                throw new ArgumentNullException("privateKey");
            }

            if (hash == null)
            {
                throw new ArgumentNullException("hash");
            }

            if (signature == null)
            {
                throw new ArgumentNullException("signature");
            }

            byte[] hashData = hash.ComputeHash(data);
            byte[] signatureHashData = Decrypt(signature, privateKey);

            if (signatureHashData != null && signatureHashData.Length == hashData.Length)
            {
                for (int i = 0; i < signatureHashData.Length; i++)
                {
                    if (signatureHashData[i] != hashData[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }

        public static byte[] Sign(byte[] data, RSAPrivateKey privateKey, HashAlgorithm hash)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (privateKey == null)
            {
                throw new ArgumentNullException("privateKey");
            }

            if (hash == null)
            {
                throw new ArgumentNullException("hash");
            }

            byte[] hashData = hash.ComputeHash(data);
            byte[] signature = Encrypt(hashData, privateKey);
            return signature;
        }

        public static bool Verify(byte[] data, RSAPublicKey publicKey, HashAlgorithm hash, byte[] signature)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (publicKey == null)
            {
                throw new ArgumentNullException("publicKey");
            }

            if (hash == null)
            {
                throw new ArgumentNullException("hash");
            }

            if (signature == null)
            {
                throw new ArgumentNullException("signature");
            }

            byte[] hashData = hash.ComputeHash(data);

            byte[] signatureHashData = Decrypt(signature, publicKey);

            if (signatureHashData != null && signatureHashData.Length == hashData.Length)
            {
                for (int i = 0; i < signatureHashData.Length; i++)
                {
                    if (signatureHashData[i] != hashData[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }
    }
}
