/* ==============================================================================
* 命名空间：Quick.Common.Encrypt 
* 类 名 称：DES
* 创 建 者：Qing
* 创建时间：2019/06/29 11:03:20
* CLR 版本：4.0.30319.42000
* 保存的文件名：DES
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common.Encrypt
{
    /// <summary>
    /// DES加解密
    /// </summary>
    public static class Des
    {
        /// <summary>
        /// 生成key
        /// </summary>
        public static void Generator(out string key)
        {
            var des = (DESCryptoServiceProvider)DES.Create();
            key = Encoding.ASCII.GetString(des.Key);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="password"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DesEncrypt(string password, string key)
        {
            byte[] data = Encoding.UTF8.GetBytes(password);
            var des = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(key), IV = Encoding.ASCII.GetBytes(key)
            };
            var desEncrypt = des.CreateEncryptor();
            var result = desEncrypt.TransformFinalBlock(data, 0, data.Length);
            return BitConverter.ToString(result);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="password"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DesDecrypt(string password, string key)
        {
            var input = password.Split("-".ToCharArray());
            var data = new byte[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                data[i] = byte.Parse(input[i], NumberStyles.HexNumber);
            }
            var des = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(key), IV = Encoding.ASCII.GetBytes(key)
            };
            byte[] result;
            using (var encrypt = des.CreateDecryptor())
            {
                result = encrypt.TransformFinalBlock(data, 0, data.Length);
            }
            return Encoding.UTF8.GetString(result);
        }

        /// <summary>   
        /// 加密数据   MD5方式
        /// </summary>   
        /// <param name="str"></param>   
        /// <param name="key">加密密钥</param>   
        /// <returns></returns>   
        public static string DesEncryptMd5(string str, string key)
        {
            var des = new DESCryptoServiceProvider();
            var inputByteArray = Encoding.Default.GetBytes(str); 
            des.Key = Encoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")?.Substring(0, 8) ?? throw new InvalidOperationException());
            des.IV = Encoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")?.Substring(0, 8) ?? throw new InvalidOperationException());
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            var ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
                ret.AppendFormat("{0:X2}", b);
            return ret.ToString();
        }

        /// <summary>   
        /// 解密数据   MD5方式
        /// </summary>   
        /// <param name="str"></param>   
        /// <param name="key">加密密钥</param>   
        /// <returns></returns>   
        public static string DesDecryptMd5(string str, string key)
        {
            var des = new DESCryptoServiceProvider();
            var len = str.Length / 2;
            var inputByteArray = new byte[len];
            int x;
            for (x = 0; x < len; x++)
            {
                var i = Convert.ToInt32(str.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = Encoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")?.Substring(0, 8) ?? throw new InvalidOperationException());
            des.IV = Encoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")?.Substring(0, 8) ?? throw new InvalidOperationException());
            var ms = new  MemoryStream();
            var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
    } 

    /*  调用方式
        public static void des()
        {
            string key = "";
            DES.Generator(out key);
            var aaa = DES.DESEncryptMD5("123456789", "12312312");
            var bbb = DES.DESDecryptMD5(aaa, "12312312");
            var cc = DES.DESEncrypt("123456789", key);
            var dd = DES.DESDecrypt(cc, key);
        }
    */
}

