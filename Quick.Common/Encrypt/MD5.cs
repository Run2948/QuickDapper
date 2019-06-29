/* ==============================================================================
* 命名空间：Quick.Common.Encrypt 
* 类 名 称：MD5
* 创 建 者：Qing
* 创建时间：2019/06/29 11:10:40
* CLR 版本：4.0.30319.42000
* 保存的文件名：MD5
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
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Quick.Common.Encrypt
{
    /// <summary>
    /// MD5加解密
    /// </summary>
    public static class Md5
    {
        /// <summary>
        /// MD5 16位加密，不可逆
        /// </summary>
        /// <param name="password"></param> 
        public static string Encrypt16Bit(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var str = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(password)), 4, 8);
            return str.Replace("-", "");
        }

        /// <summary>
        ///  MD5 32位加密，不可逆
        /// </summary>
        /// <param name="password"></param> 
        public static string Encrypt32Bit(string password)
        {
            var md5 = MD5.Create();
            var str = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return str.Aggregate("", (current, t) => current + t.ToString("X"));
        }

        /// <summary>
        ///  MD5 64位加密，不可逆
        /// </summary>
        /// <param name="password"></param> 
        public static string Encrypt64Bit(string password)
        {
            var md5 = MD5.Create();
            var str = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(str);
        }
    }

    /* 调用方式
        public static void md5()
        {
            var str1 = MD5.Encrypt16Bit("123456789");
            var str2 = MD5.Encrypt32Bit("123456789");
            var str3 = MD5.Encrypt64Bit("123456789");
        }
     */
}
