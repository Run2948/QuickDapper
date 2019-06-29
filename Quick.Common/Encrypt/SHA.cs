/* ==============================================================================
* 命名空间：Quick.Common.Encrypt 
* 类 名 称：SHA
* 创 建 者：Qing
* 创建时间：2019/06/29 11:10:46
* CLR 版本：4.0.30319.42000
* 保存的文件名：SHA
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common.Encrypt
{
    /// <summary>
    /// SHA加解密
    /// </summary>
    public static class Sha
    {
        /// <summary>
        /// SHA-1
        /// </summary>
        /// <param name="str"></param> 
        public static string SHA_1(string str)
        {
            var sha1Csp = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] bytHash = sha1Csp.ComputeHash(bytValue);
            sha1Csp.Clear();
            string hashStr = "";
            for (int counter = 0; counter < bytHash.Count(); counter++)
            {
                long i = bytHash[counter] / 16;
                var tempStr = "";
                if (i > 9)
                    tempStr = ((char)(i - 10 + 0x41)).ToString();
                else
                    tempStr = ((char)(i + 0x30)).ToString();
                i = bytHash[counter] % 16;
                if (i > 9)
                    tempStr += ((char)(i - 10 + 0x41)).ToString();
                else
                    tempStr += ((char)(i + 0x30)).ToString();
                hashStr += tempStr;
            }
            return hashStr;
        }

        /// <summary>
        /// SHA-256
        /// </summary>
        /// <param name="str"></param> 
        public static string SHA_256(string str)
        {
            System.Security.Cryptography.SHA256CryptoServiceProvider sha256Csp = new System.Security.Cryptography.SHA256CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] bytHash = sha256Csp.ComputeHash(bytValue);
            sha256Csp.Clear();
            string hashStr = "";
            for (int counter = 0; counter < bytHash.Count(); counter++)
            {
                long i = bytHash[counter] / 16;
                var tempStr = "";
                if (i > 9)
                    tempStr = ((char)(i - 10 + 0x41)).ToString();
                else
                    tempStr = ((char)(i + 0x30)).ToString();
                i = bytHash[counter] % 16;
                if (i > 9)
                    tempStr += ((char)(i - 10 + 0x41)).ToString();
                else
                    tempStr += ((char)(i + 0x30)).ToString();
                hashStr += tempStr;
            }
            return hashStr;
        }

        /// <summary>
        /// SHA-384
        /// </summary>
        /// <param name="str"></param> 
        public static string SHA_384(string str)
        {
            var sha384Csp = new System.Security.Cryptography.SHA384CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] bytHash = sha384Csp.ComputeHash(bytValue);
            sha384Csp.Clear();
            string hashStr = "";
            for (int counter = 0; counter < bytHash.Count(); counter++)
            {
                long i = bytHash[counter] / 16;
                var temptr = "";
                if (i > 9)
                    temptr = ((char)(i - 10 + 0x41)).ToString();
                else
                    temptr = ((char)(i + 0x30)).ToString();
                i = bytHash[counter] % 16;
                if (i > 9)
                    temptr += ((char)(i - 10 + 0x41)).ToString();
                else
                    temptr += ((char)(i + 0x30)).ToString();
                hashStr += temptr;
            }
            return hashStr;
        }

        /// <summary>
        /// SHA-512
        /// </summary>
        /// <param name="str"></param> 
        public static string SHA_512(string str)
        {
            var sha512Csp = new System.Security.Cryptography.SHA512CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] bytHash = sha512Csp.ComputeHash(bytValue);
            sha512Csp.Clear();
            string hashStr = "";
            for (int counter = 0; counter < bytHash.Count(); counter++)
            {
                long i = bytHash[counter] / 16;
                var tempStr = "";
                if (i > 9)
                    tempStr = ((char)(i - 10 + 0x41)).ToString();
                else
                    tempStr = ((char)(i + 0x30)).ToString();
                i = bytHash[counter] % 16;
                if (i > 9)
                    tempStr += ((char)(i - 10 + 0x41)).ToString();
                else
                    tempStr += ((char)(i + 0x30)).ToString();
                hashStr += tempStr;
            }
            return hashStr;
        }

    }

    /*  调用方式：
         public static void sha()
         {
             var aaa1 = SHA.SHA_1("123456789");
             var aaa2 = SHA.SHA_256("123456789");
             var aaa3 = SHA.SHA_384("123456789");
             var aaa4 = SHA.SHA_512("123456789");
         }
     */
}
