using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace UserCenter.OpenAPI
{
    public class VersionControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;

        public VersionControllerSelector(HttpConfiguration config) : base(config)
        {
            _config = config;
        }

        public override IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            Dictionary<string, HttpControllerDescriptor> dict = new Dictionary<string, HttpControllerDescriptor>();
            foreach (var asm in _config.Services.GetAssembliesResolver().GetAssemblies())
            {
                //获取所有继承自ApiController的非抽象类
                var controllerTypes = asm.GetTypes()
.Where(t => !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t)).ToArray();
                foreach (var ctrlType in controllerTypes)
                {
                    //从namespace中提取出版本号
                    var match = Regex.Match(ctrlType.Namespace,
                    @"UserCenter.OpenAPI.Controllers.v(\d+)");
                    if (match.Success)
                    {
                        string verNum = match.Groups[1].Value;//获取版本号
                        string ctrlName = Regex.Match(ctrlType.Name, "(.+)Controller").Groups[1].Value;//从PersonController中拿到Person
                        string key = ctrlName + "v" + verNum;//Personv2为key
                        dict[key] = new HttpControllerDescriptor(_config, ctrlName, ctrlType);
                    }
                }
            }
            return dict;
        }

        //设计就是返回HttpControllerDesriptor的过程
        public override System.Web.Http.Controllers.HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            //获取所有的controller键值集合
            var controllers = GetControllerMapping();
            //获取路由数据
            var routeData = request.GetRouteData();
            //从路由中获取当前controller的名称
            var controllerName = (string)routeData.Values["controller"];
            //从url中获取到版本号
            //api/v1/Person
            string verNum = Regex.Match(request.RequestUri.PathAndQuery, @"api/v(\d+)").Groups[1].Value;
            string key = controllerName + "v" + verNum;//获取Personv2
            if (controllers.ContainsKey(key))//获取HttpControllerDescriptor
            {
                return controllers[key];
            }
            else
            {
                return null;
            }
        }

    }
}