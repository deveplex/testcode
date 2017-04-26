using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    /// <summary>
    /// 扩展System.Mvc.Controller
    /// </summary>
    public static class ControllerExtension
    {
        public static XmlResult Xml(this ApiController request, object obj) { return Xml(obj, null, XmlRequestBehavior.DenyGet); }
        public static XmlResult Xml(this ApiController request, object obj, XmlRequestBehavior behavior) { return Xml(obj, null, behavior); }
        public static XmlResult Xml(this ApiController request, object obj, Encoding encoding, XmlRequestBehavior behavior) { return Xml(obj, encoding, behavior); }

        internal static XmlResult Xml(object data, Encoding encoding, XmlRequestBehavior behavior) { return new XmlResult() {Data = data, Encoding = encoding, XmlRequestBehavior = behavior }; }
    }
}