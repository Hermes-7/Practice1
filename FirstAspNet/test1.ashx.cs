using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstAspNet
{
    /// <summary>
    /// test1 的摘要说明
    /// </summary>
    public class test1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            HttpContext current = HttpContext.Current;
            string username = current.Request["name"];
            context.Response.Write(username + "<font color='red'>你好111</font>");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}