﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketForm.Function
{
    static class ExtendFunctions
    {
        #region 类型转换
        /// <summary>
        /// 将变量转换为int32
        /// </summary>
        /// <param name="obj">任意变量</param>
        /// <returns>值</returns>
        public static int Int(this object obj)
        {
            try { return Convert.ToInt32(obj); }
            catch { return 0; }
        }
        /// <summary>
        /// 将变量转换为int32, 但无法转换会报错
        /// </summary>
        /// <param name="obj">任意变量</param>
        /// <returns>值</returns>
        public static int ToIntWidthEx(this object obj)
        {
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// 将变量转换为int32, 但无法转换会返回null
        /// </summary>
        /// <param name="obj">任意变量</param>
        /// <returns>值</returns>
        public static int? ToIntWidthNull(this object obj)
        {
            try { return Convert.ToInt32(obj); }
            catch { return null; }
        }

        /// <summary>
        /// 将变量转换为int64
        /// </summary>
        /// <param name="obj">任意变量</param>
        /// <returns>值</returns>
        public static long Long(this object obj)
        {
            try { return Convert.ToInt64(obj); }
            catch { return 0; }
        }
        /// <summary>
        /// 将变量转换为int64, 但无法转换会报错
        /// </summary>
        /// <param name="obj">任意变量</param>
        /// <returns>值</returns>
        public static long ToLongWidthEx(this object obj)
        {
            return Convert.ToInt64(obj);
        }
        /// <summary>
        /// 将变量转换为int64, 但无法转换会返回null
        /// </summary>
        /// <param name="obj">任意变量</param>
        /// <returns>值</returns>
        public static long? ToLongWidthNull(this object obj)
        {
            try { return Convert.ToInt64(obj); }
            catch { return null; }
        }

        /// <summary>
        /// 将变量转换为double
        /// </summary>
        /// <param name="obj">任意变量</param>
        /// <returns>值</returns>
        public static double Double(this object obj)
        {
            try { return Convert.ToDouble(obj); }
            catch { return 0; }
        }
        /// <summary>
        /// 将变量转换为double, 但无法转换会报错
        /// </summary>
        /// <param name="obj">任意变量</param>
        /// <returns>值</returns>
        public static double ToDoubleWidthEx(this object obj)
        {
            return Convert.ToDouble(obj);
        }
        /// <summary>
        /// 将变量转换为double, 但无法转换会返回null
        /// </summary>
        /// <param name="obj">任意变量</param>
        /// <returns>值</returns>
        public static double? ToDoubleWidthNull(this object obj)
        {
            try { return Convert.ToDouble(obj); }
            catch { return null; }
        }
        #endregion

        #region 序列化
        /// <summary>
        /// 将对象转换为byte数组
        /// </summary>
        /// <param name="obj">被转换对象</param>
        /// <returns>转换后byte数组</returns>
        public static byte[] ToBytes(this object obj)
        {
            byte[] buff;
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter iFormatter = new BinaryFormatter();
                iFormatter.Serialize(ms, obj);
                buff = ms.GetBuffer();
            }
            return buff;
        }

        /// <summary>
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <returns>转换完成后的对象</returns>
        public static T ToObject<T>(this byte[] buff) where T : new()
        {
            T obj;
            using (MemoryStream ms = new MemoryStream(buff))
            {
                IFormatter iFormatter = new BinaryFormatter();
                obj = (T)iFormatter.Deserialize(ms);
            }
            return obj;
        } 
        #endregion
    }
}
