using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RedisDemo3.Controllers
{
    public class RedisController : Controller
    {
        // GET: Redis
        public ActionResult Index()
        {
            return View();
        }

        private static string XinWen_Prefix = "WWW_XinWen_";
        public async Task<ActionResult> Index(int id)
        {
            using (ConnectionMultiplexer redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379"))
            {
                IDatabase db = redis.GetDatabase();//默认是访问db0数据库，可以通过方法参数指定数字访问不同的数据库
                                                   //以ip地址和文章id为key
                string hasClickKey = XinWen_Prefix + Request.UserHostAddress + "_" + id;
                //如果之前这个ip给这个文章贡献过点击量，则不重复计算点击量
                if (await db.KeyExistsAsync(hasClickKey) == false)
                {
                    await db.StringIncrementAsync(XinWen_Prefix + "XWClickCount" + id, 1);
                    //记录一下这个ip给这个文章贡献过点击量，有效期一天
                    db.StringSet(hasClickKey, "a", TimeSpan.FromDays(1));
                }
                RedisValue clickCount = await db.StringGetAsync(XinWen_Prefix + "XWClickCount" + id);
                NewsModel model = new NewsModel();
                model.ClickCount = Convert.ToInt32(clickCount);
                return View(model);
            }
            return View();
        }
    }

    public class NewsModel
    { 
        public int ClickCount { get; set; }
    }
}