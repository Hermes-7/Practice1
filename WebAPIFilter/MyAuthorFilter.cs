using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPIFilter
{
    public class MyAuthorFilter : IAuthorizationFilter //如果项目也添加了对MVC程序集的引用，一定要用System.Web.Http.Filters下的IAuthorizationFilter
    {
        public bool AllowMultiple => true;
        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext,
        CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            IEnumerable<string> values;
            if (actionContext.Request.Headers.TryGetValues("UserName", out values))
            {
                string userName = values.FirstOrDefault();
                if (userName != "admin")
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            return await continuation();
        }
    }
}