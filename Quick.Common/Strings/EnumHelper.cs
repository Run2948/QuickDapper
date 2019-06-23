using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Quick.Common.Strings
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 返回枚举值的描述信息。
        /// </summary>
        /// <param name="value">要获取描述信息的枚举值。</param>
        /// <returns>枚举值的描述信息。</returns>
        public static string GetEnumDesc<T>(this int value)
        {
            Type enumType = typeof(T);
            DescriptionAttribute attr = null;

            // 获取枚举常数名称。
            string name = Enum.GetName(enumType, value);
            if (name != null)
            {
                // 获取枚举字段。
                FieldInfo fieldInfo = enumType.GetField(name);
                if (fieldInfo != null)
                {
                    // 获取描述的属性。
                    attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                }
            }

            // 返回结果
            if (attr != null && !string.IsNullOrEmpty(attr.Description))
                return attr.Description;
            else
                return string.Empty;
        }

        /// <summary>
        /// 返回枚举项的描述信息。
        /// </summary>
        /// <param name="e">要获取描述信息的枚举项。</param>
        /// <returns>枚举项的描述信息。</returns>
        public static string GetEnumDesc(this Enum e)
        {
            if (e == null)
            {
                return string.Empty;
            }
            Type enumType = e.GetType();
            DescriptionAttribute attr = null;

            // 获取枚举字段。
            FieldInfo fieldInfo = enumType.GetField(e.ToString());
            if (fieldInfo != null)
            {
                // 获取描述的属性。
                attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
            }

            // 返回结果
            if (attr != null && !string.IsNullOrEmpty(attr.Description))
                return attr.Description;
            else
                return string.Empty;
        }

        /// <summary>
        /// 获取枚举描述列表，并转化为键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isHasAll">是否包含“全部”</param>
        /// <param name="filterItem">过滤项</param>
        /// <returns></returns>
        public static List<EnumKeyValue> EnumDescToList<T>(bool isHasAll, params string[] filterItem)
        {
            List<EnumKeyValue> list = new List<EnumKeyValue>();

            // 如果包含全部则添加
            if (isHasAll)
            {
                list.Add(new EnumKeyValue() { Key = 0, Name = "全部" });
            }

            #region 方式一
            foreach (var item in typeof(T).GetFields())
            {
                // 获取描述
                if (item.GetCustomAttribute(typeof(DescriptionAttribute), true) is DescriptionAttribute attr && !string.IsNullOrEmpty(attr.Description))
                {
                    // 跳过过滤项
                    if (Array.IndexOf<string>(filterItem, attr.Description) != -1)
                    {
                        continue;
                    }
                    // 添加
                    EnumKeyValue model = new EnumKeyValue
                    {
                        Key = (int) Enum.Parse(typeof(T), item.Name), Name = attr.Description
                    };
                    list.Add(model);
                }
            }
            #endregion

            #region 方式二
            //foreach (int item in Enum.GetValues(typeof(T)))
            //{
            //    // 获取描述
            //    FieldInfo fi = typeof(T).GetField(Enum.GetName(typeof(T), item));
            //    var attr = fi.GetCustomAttribute(typeof(DescriptionAttribute), false) as DescriptionAttribute;
            //    if (attr != null && !string.IsNullOrEmpty(attr.Description))
            //    {
            //        // 跳过过滤项
            //        if (Array.IndexOf<string>(filterItem, attr.Description) != -1)
            //        {
            //            continue;
            //        }
            //        // 添加
            //        EnumKeyValue model = new EnumKeyValue();
            //        model.Key = item;
            //        model.Name = attr.Description;
            //        list.Add(model);
            //    }
            //} 
            #endregion

            return list;
        }

        /// <summary>
        /// 获取枚举值列表，并转化为键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isHasAll">是否包含“全部”</param>
        /// <param name="filterItem">过滤项</param>
        /// <returns></returns>
        public static List<EnumKeyValue> EnumToList<T>(bool isHasAll, params string[] filterItem)
        {
            List<EnumKeyValue> list = new List<EnumKeyValue>();

            // 如果包含全部则添加
            if (isHasAll)
            {
                list.Add(new EnumKeyValue() { Key = 0, Name = "全部" });
            }

            foreach (int item in Enum.GetValues(typeof(T)))
            {
                string name = Enum.GetName(typeof(T), item);
                // 跳过过滤项
                if (Array.IndexOf<string>(filterItem, name) != -1)
                {
                    continue;
                }
                // 添加
                EnumKeyValue model = new EnumKeyValue();
                model.Key = item;
                model.Name = name;
                list.Add(model);
            }

            return list;
        }
    }

    /// <summary>
    /// 枚举键值对
    /// </summary>
    public class EnumKeyValue
    {
        public int Key { get; set; }
        public string Name { get; set; }
    }
}
