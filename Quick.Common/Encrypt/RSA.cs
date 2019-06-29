/* ==============================================================================
* 命名空间：Quick.Common.Encrypt 
* 类 名 称：RSA
* 创 建 者：Qing
* 创建时间：2019/06/29 10:55:51
* CLR 版本：4.0.30319.42000
* 保存的文件名：RSA
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/

using System;
using System.Security.Cryptography;
using System.Text;

namespace Quick.Common.Encrypt
{
    /// <summary>
    ///  非对称加密RSA 算法 https://www.cnblogs.com/heimalang/p/6496856.html#4209270
    /// </summary>
    public static class Rsa
    {
        /// <summary>
        /// 生成密钥
        /// <param name="privateKey">私钥</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="keySize">密钥长度：512,1024,2048，4096，8192</param>
        /// </summary>
        public static void Generator(out string privateKey, out string publicKey, int keySize = 2048)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize);
            privateKey = rsa.ToXmlString(true); //将RSA算法的私钥导出到字符串PrivateKey中 参数为true表示导出私钥 true 表示同时包含 RSA 公钥和私钥；false 表示仅包含公钥。
            publicKey = rsa.ToXmlString(false); //将RSA算法的公钥导出到字符串PublicKey中 参数为false表示不导出私钥 true 表示同时包含 RSA 公钥和私钥；false 表示仅包含公钥。
        }
       
        /// <summary>
        /// RSA加密 将公钥导入到RSA对象中，准备加密
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="encryptString">待加密的字符串</param>
        public static string RsaEncrypt(string publicKey, string encryptString)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            var plainTextBArray = (new UnicodeEncoding()).GetBytes(encryptString);
            var cypherTextBArray = rsa.Encrypt(plainTextBArray, false);
            var result = Convert.ToBase64String(cypherTextBArray);
            return result;
        }
        
        /// <summary>
        /// RSA解密 将私钥导入RSA中，准备解密
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="decryptString">待解密的字符串</param>
        public static string RsaDecrypt(string privateKey, string decryptString)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            var plainTextBArray = Convert.FromBase64String(decryptString);
            var dypherTextBArray = rsa.Decrypt(plainTextBArray, false);
            var result = (new UnicodeEncoding()).GetString(dypherTextBArray);
            return result;
        }
    }

    /*    调用方式：
        static void Main(string[] args)
        {
            string PrivateKey = "";
            string PublicKey = "";
            Rsa.Generator(out PrivateKey, out PublicKey, 1024);
            var aaa = Rsa.RsaEncrypt(PublicKey, "123456789");
            var bbb = Rsa.RsaDecrypt(PrivateKey, aaa);
        }
     */
}
