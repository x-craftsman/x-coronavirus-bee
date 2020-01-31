using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Craftsman.Core.Common
{
    public static class UtilitiesTool
    {
        public static string MD5EncryptString(string str)
        {
            var md5 = MD5.Create();
            var byteOld = Encoding.UTF8.GetBytes(str);   //将字符串转换成字节数组
            var byteNew = md5.ComputeHash(byteOld);      //调用加密方法

            var sb = new StringBuilder(); // 将加密结果转换为字符串
            foreach (byte b in byteNew)
            {

                sb.Append(b.ToString("x2"));    //将字节转换成16进制表示的字符串
            }
            return sb.ToString();// 返回加密的字符串
        }

        public static string GetEnumDescription(Enum enumValue)
        {
            var value = enumValue.ToString();
            var field = enumValue.GetType().GetField(value);
            var objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);  //获取描述属性
            if (objs == null || objs.Length == 0)
            {
                //当描述属性没有时，直接返回名称
                return value;
            }
            var descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }
    }
}
