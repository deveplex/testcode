using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    /// <summary>
    /// 扩展System.Web.Mvc XmlRequestBehavior
    /// 指定是否允许来自客户端的HTTP GET请求
    /// </summary>
    public enum XmlRequestBehavior
    {
        /// <summary>
        /// HTTP GET requests from the client are allowed.
        /// 允许来自客户端的HTTP GET请求
        /// </summary>      
        AllowGet = 0,
        /// <summary>
        /// HTTP GET requests from the client are not allowed.
        /// 不允许来自客户端的HTTP GET请求
        /// </summary>
        DenyGet = 1,
    }
}