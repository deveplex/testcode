using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Models
{
    public class ImageResult : ActionResult
    {

        private string _ContentType = "image/jpeg";
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

        // 图片  
        public Image Data;

        // 构造器  
        public ImageResult(Image image)
        {
            Data = image;
        }

        // 主要需要重写的方法  
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            // 设置 HTTP Header  
            response.ContentType = this.ContentType;

            // 将图片数据写入Response  
            Data.Save(context.HttpContext.Response.OutputStream, ImageFormat.Jpeg);
        }
    }
}