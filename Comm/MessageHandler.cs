using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Qdcdc.LessThanContainer.Comm
{
    public static class MessageHandler
    {
        /// <summary>
        /// 生成秘钥
        /// </summary>
        /// <param name="xmlKey"></param>
        /// <param name="xmlPublicKey"></param>
        public static bool RSAKey(out string xmlKey, out string xmlPublicKey)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                xmlKey = rsa.ToXmlString(true);
                xmlPublicKey = rsa.ToXmlString(false);
                return true;
            }
            catch (Exception ex)
            {
                xmlKey = "";
                xmlPublicKey = "";
                return false;
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="xmlPublicKey">公匙</param> 
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string RSAEncrypt(string xmlPublicKey, string str)
        {
            string result = "";
            try
            {
                byte[] tStr;
                byte[] Cstr;

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                tStr = (new UTF8Encoding()).GetBytes(str);
                Cstr = rsa.Encrypt(tStr, false);
                result = Convert.ToBase64String(Cstr);
                return result;

            }
            catch (Exception ex)
            {
                return result;
            }

        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="xmlPrivateKey">秘钥</param>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string RSADecrypt(string xmlPrivateKey, string str)
        {
            string result = "";
            try
            {
                byte[] tStr;
                byte[] cStr;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                tStr = Convert.FromBase64String(str);
                cStr = rsa.Decrypt(tStr, false);
                result = (new UTF8Encoding()).GetString(cStr);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }


        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="xmlPrivateKey">秘钥</param>
        /// <param name="str">签名字符串</param>
        /// <returns></returns>
        public static string RSASign(string xmlPrivateKey, string str)
        {
            byte[] hashByte;
            byte[] signByte;
            string ret = "";
            try
            {
                hashByte = Convert.FromBase64String(GetHash(str));
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm("MD5");
                signByte = rsaFormatter.CreateSignature(hashByte);
                ret = Convert.ToBase64String(signByte);
                return ret;
            }
            catch (Exception ex)
            {
                return ret;

            }

        }

        public static string GetHash(string str)
        {
            HashAlgorithm al = HashAlgorithm.Create("MD5");
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(str);
            byte[] arr = al.ComputeHash(bytes);
            return Convert.ToBase64String(arr);
        }


        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="xmlPublic">公匙</param>
        /// <param name="str">原报文</param>
        /// <param name="defStr">签名报文</param>
        /// <returns></returns>
        public static bool RSADefSign(string xmlPublicKey, string str, string defStr)
        {
            byte[] defData;
            byte[] hashByte;

            try
            {

                hashByte = Convert.FromBase64String(GetHash(str));
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);

                RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("MD5");
                defData = Convert.FromBase64String(defStr);

                if (rsaDeformatter.VerifySignature(hashByte, defData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="source"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Sign(string source, string privateKey)
        {
            if (source == null)
            {
                throw new ArgumentNullException("空的待签名字符串");
            }

            if (source.Length == 0)
            {
                throw new ArgumentNullException("零长度的待签名字符串");
            }

            if (privateKey == null)
            {
                throw new ArgumentNullException("空的私钥字符串");
            }

            if (privateKey.Length == 0)
            {
                throw new ArgumentNullException("零长度的私钥字符串");
            }

            byte[] arrayBytesData;
            byte[] arrayHashData;

            SHA1CryptoServiceProvider provider;

            try
            {
                arrayBytesData = Encoding.Default.GetBytes(source);
                provider = new SHA1CryptoServiceProvider();
                arrayHashData = provider.ComputeHash(arrayBytesData);

            }
            catch (Exception e)
            {
                throw new Exception(String.Format("计算哈西值错：{0}", e.Message));
            }

            return SignHash(arrayHashData, privateKey);
        }

        public static string SignHash(byte[] hashData, string privateKey)
        {
            string signature = "";

            if (hashData == null)
            {
                throw new ArgumentNullException("空的待签名哈西值");
            }

            if (hashData.Length == 0)
            {
                throw new ArgumentNullException("零长度的待签名哈西值");
            }

            if (privateKey == null)
            {
                throw new ArgumentNullException("空的私钥字符串");
            }

            if (privateKey.Length == 0)
            {
                throw new ArgumentNullException("零长度的私钥字符串");
            }

            RSACryptoServiceProvider provider;
            RSAPKCS1SignatureFormatter formatter;

            lock (syncRoot)
            {
                try
                {
                    provider = new RSACryptoServiceProvider();
                    provider.FromXmlString(privateKey);

                }
                catch (Exception e)
                {
                    throw new Exception(String.Format("读取私钥时出错：{0}", e.Message));
                }

                try
                {
                    formatter = new RSAPKCS1SignatureFormatter();
                    formatter.SetKey(provider);
                    formatter.SetHashAlgorithm("SHA1");
                    signature = Convert.ToBase64String(formatter.CreateSignature(hashData));
                }
                catch (Exception e)
                {
                    throw new Exception(String.Format("签名时出错：{0}", e.Message));
                }
            }

            return signature;
        }


        private static object syncRoot = new Object();

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="source"></param>
        /// <param name="signature"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static bool Verify(string source, string signature, string publicKey)
        {
            if (source == null)
            {
                throw new ArgumentNullException("空的源数据");
            }

            if (source.Length == 0)
            {
                throw new ArgumentNullException("零长度的源数据");
            }

            if (signature == null)
            {
                throw new ArgumentNullException("空的数字签名");
            }

            if (signature.Length == 0)
            {
                throw new ArgumentNullException("零长度的数字签名");
            }

            if (publicKey == null)
            {
                throw new ArgumentNullException("空的公钥字符串");
            }

            if (publicKey.Length == 0)
            {
                throw new ArgumentNullException("零长度的公钥字符串");
            }

            byte[] arrayBytesData;
            byte[] arrayHashData;

            SHA1CryptoServiceProvider provider;

            try
            {
                arrayBytesData = Encoding.Default.GetBytes(source);
                provider = new SHA1CryptoServiceProvider();
                arrayHashData = provider.ComputeHash(arrayBytesData);

            }
            catch (Exception e)
            {
                throw new Exception(String.Format("计算哈西值错：{0}", e.Message));
            }

            return VerifyHash(arrayHashData, signature, publicKey);
        }
        public static bool VerifyHash(byte[] hashData, string signature, string publicKey)
        {
            if (hashData == null)
            {
                throw new ArgumentNullException("空的哈西值");
            }

            if (hashData.Length == 0)
            {
                throw new ArgumentNullException("零长度的哈西值");
            }

            if (signature == null)
            {
                throw new ArgumentNullException("空的数字签名");
            }

            if (signature.Length == 0)
            {
                throw new ArgumentNullException("零长度的数字签名");
            }

            if (publicKey == null)
            {
                throw new ArgumentNullException("空的公钥字符串");
            }

            if (publicKey.Length == 0)
            {
                throw new ArgumentNullException("零长度的公钥字符串");
            }

            byte[] arraySignedData;
            try
            {
                arraySignedData = Convert.FromBase64String(signature);
            }
            catch
            {
                throw new ArgumentException("无效的签名数据");
            }

            RSACryptoServiceProvider provider;
            RSAPKCS1SignatureDeformatter deformatter;

            lock (syncRoot)
            {
                provider = new RSACryptoServiceProvider();

                try
                {
                    provider.FromXmlString(publicKey);

                }
                catch (Exception e)
                {
                    throw new ArgumentException(String.Format("无效的公钥参数：{0}", e.Message));
                }

                try
                {
                    deformatter = new RSAPKCS1SignatureDeformatter();
                    deformatter.SetKey(provider);
                    deformatter.SetHashAlgorithm("SHA1");
                    if (deformatter.VerifySignature(hashData, arraySignedData))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(String.Format("验证数字签名失败：{0}", e.Message));
                }
            }

        }


    }
}
