using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace WebApplication3.Models
{
    /// <summary>
    /// Xml序列化与反序列化
    /// </summary>
    public class XmlUtil
    {
        #region 反序列化
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string xmlText)
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(type);

                using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(xmlText)))
                {
                    using (XmlTextReader reader = new XmlTextReader(stream))
                    {
                        return xml.Deserialize(reader);
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xml = new XmlSerializer(type);
            return xml.Deserialize(stream);
        }
        #endregion

        #region 序列化

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serializer(object obj)
        {
            return Serializer(obj, Encoding.Default);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="Encoding">类型</param>
        /// <returns></returns>
        public static string Serializer(object obj, Encoding encoding)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            string result = null;

            XmlSerializer xml = new XmlSerializer(obj.GetType());

            using (MemoryStream stream = new MemoryStream())
            {
                using (XmlTextWriter writer = new XmlTextWriter(stream, encoding))
                {
                    try
                    {
                        writer.Formatting = Formatting.Indented;
                        //序列化对象
                        xml.Serialize(writer, obj);
                    }
                    catch (InvalidOperationException)
                    {
                        throw;
                    }

                    result = encoding.GetString(stream.ToArray());
                }
            }

            return result;
        }

        #endregion
    }
}