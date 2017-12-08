using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Tools
{
    public class RsaTool
    {
        /// <summary>
        /// RSA加密的密匙结构  公钥和私匙
        /// </summary>
        public struct RSAKey
        {
            public string PublicKey { get; set; }
            public AsymmetricKeyParameter PublicKeyObj { get; set; }
            public string PrivateKey { get; set; }
            public AsymmetricKeyParameter PrivateKeyObj { get; set; }
        }

        public static RSAKey GetRASKey()
        {
            //RSA密钥对的构造器  
            RsaKeyPairGenerator keyGenerator = new RsaKeyPairGenerator();
            //RSA密钥构造器的参数  
            RsaKeyGenerationParameters param = new RsaKeyGenerationParameters(
                Org.BouncyCastle.Math.BigInteger.ValueOf(3),
                new Org.BouncyCastle.Security.SecureRandom(),
                1024,   //密钥长度  
                25);
            //用参数初始化密钥构造器  
            keyGenerator.Init(param);
            //产生密钥对  
            AsymmetricCipherKeyPair keyPair = keyGenerator.GenerateKeyPair();
            //获取公钥和私钥  
            AsymmetricKeyParameter publicKey = keyPair.Public;
            AsymmetricKeyParameter privateKey = keyPair.Private;

            if (((RsaKeyParameters)publicKey).Modulus.BitLength < 1024)
            {
                throw new Exception("failed key generation (1024) length test");
            }
            return new RSAKey()
            {
                PublicKey = SavePublicKey(publicKey),
                PublicKeyObj=publicKey,
                PrivateKey = SavePrivateKey(privateKey),
                PrivateKeyObj = privateKey
            };
        }

        /// <summary>
        /// 字符串加密(私钥加密公钥解密)
        /// </summary>
        /// <param name="source">源字符串 明文</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>加密遇到错误将会返回原字符串</returns>
        public static string EncryptString(string source, AsymmetricKeyParameter privateKey)
        {
            string encryptString = string.Empty;
            byte[] encryptData = Encoding.UTF8.GetBytes(source);

            //非对称加密算法，加解密用 
            IAsymmetricBlockCipher engine = new RsaEngine();
            //私钥加密
            engine.Init(true, privateKey);
            try
            {
                encryptData = engine.ProcessBlock(encryptData, 0, encryptData.Length);
                encryptString = Convert.ToBase64String(encryptData);
            }
            catch (Exception)
            {
                encryptString = source;
                //throw new Exception("failed - exception " + Environment.NewLine + ex.ToString());
            }
            return encryptString;
        }

        /// <summary>
        /// 字符串解密(私钥加密公钥解密)
        /// </summary>
        /// <param name="encryptString">密文</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>遇到解密失败将会返回原字符串</returns>
        public static string DecryptString(string dncryptString, string publicKey)
        {
            string source = string.Empty;
            byte[] decryptData = Convert.FromBase64String(dncryptString);
            byte[] publicKeyData = Convert.FromBase64String(publicKey);
            //非对称加密算法，加解密用 
            IAsymmetricBlockCipher engine = new RsaEngine();

            //还原  
            AsymmetricKeyParameter publicKeyObj = (RsaKeyParameters)PublicKeyFactory.CreateKey(publicKeyData);

            //公钥解密
            engine.Init(false, publicKeyObj);
            try
            {
                decryptData = engine.ProcessBlock(decryptData, 0, decryptData.Length);
                source = Encoding.UTF8.GetString(decryptData);
            }
            catch (Exception)
            {
                source = dncryptString;
                //throw new Exception("failed - exception " + e.ToString());
            }
            return source;
        }
        
        /// <summary>
        /// 字符串解密(私钥加密公钥解密)
        /// </summary>
        /// <param name="encryptString">密文</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>遇到解密失败将会返回原字符串</returns>
        public static string DecryptString(string dncryptString, AsymmetricKeyParameter publicKey)
        {
            string source = string.Empty;
            byte[] decryptData = Convert.FromBase64String(dncryptString);

            //非对称加密算法，加解密用 
            IAsymmetricBlockCipher engine = new RsaEngine();

            //公钥解密
            engine.Init(false, publicKey);
            try
            {
                decryptData = engine.ProcessBlock(decryptData, 0, decryptData.Length);
                source = Encoding.UTF8.GetString(decryptData);
            }
            catch (Exception)
            {
                source = dncryptString;
                //throw new Exception("failed - exception " + e.ToString());
            }
            return source;
        }

        public static string SavePublicKey(AsymmetricKeyParameter publicKey)
        {
            //保存公钥到文件  
            SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
            Asn1Object aobject = publicKeyInfo.ToAsn1Object();
            byte[] pubInfoByte = aobject.GetEncoded();
            return Convert.ToBase64String(pubInfoByte);
        }

        public static string SavePrivateKey(AsymmetricKeyParameter privateKey)
        {
            //保存公钥到文件  
            string alg = "1.2.840.113549.1.12.1.3"; // 3 key triple DES with SHA-1  
            byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int count = 1000;
            char[] password = "123456".ToCharArray();
            EncryptedPrivateKeyInfo enPrivateKeyInfo = EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(
                alg,
                password,
                salt,
                count,
                privateKey);
            byte[] priInfoByte = enPrivateKeyInfo.ToAsn1Object().GetEncoded();
            return Convert.ToBase64String(priInfoByte);

            ////还原  
            //PrivateKeyInfo priInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(password, enPrivateKeyInfo);
            //AsymmetricKeyParameter privateKey = PrivateKeyFactory.CreateKey(priInfoByte);  
        }
    }
}
