using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace WebApplication3.Models
{
    /// <summary>
    /// 实现XmlResult继承ActionResult
    /// 扩展MVC的ActionResult支持返回XML格式结果
    /// </summary>
    public class XmlResult : ActionResult
    {
        /// <summary>
        /// 初始化
        /// </summary>         
        public XmlResult()
        {
        }

        /// <summary>
        /// 编码格式
        /// </summary>
        public Encoding Encoding
        {
            get; set;
        }

        private string _ContentType = "application/xml";
        /// <summary>
        /// 获取返回内容的类型
        /// </summary>
        public string ContentType
        {
            get
            {
                return _ContentType;
            }
        }

        private XmlRequestBehavior _XmlRequestBehavior = XmlRequestBehavior.AllowGet;

        /// <summary>
        /// Gets or sets a value that indicates whether HTTP GET requests from the client
        /// 获取或设置一个值,指示是否HTTP GET请求从客户端
        /// </summary>
        public XmlRequestBehavior XmlRequestBehavior
        {
            get; set;
        }

        /// <summary>
        /// 获取或设置内容
        /// </summary>
        public object Data
        {
            get; set;
        }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (Data == null)
            {
                context.HttpContext.Response.Output.Write(""); // 输出流对象 
            }

            HttpRequestBase request = context.HttpContext.Request;

            if (XmlRequestBehavior == XmlRequestBehavior.DenyGet && string.Equals(request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("XmlRequest_GetNotAllowed");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = this.ContentType;

            if (this.Encoding != null)
            {
                response.ContentEncoding = this.Encoding;
            }

            XmlSerializer xml = new XmlSerializer(Data.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                using (XmlTextWriter writer = new XmlTextWriter(stream, this.Encoding))
                {
                    try
                    {
                        writer.Formatting = Formatting.Indented;
                        //序列化对象
                        xml.Serialize(writer, Data);
                    }
                    catch (InvalidOperationException)
                    {
                        throw;
                    }

                    context.HttpContext.Response.Output.Write(this.Encoding.GetString(stream.ToArray())); // 输出流对象 
                }
            }
        }
    }
}