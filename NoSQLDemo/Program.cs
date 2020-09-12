using Enyim.Caching;
using Enyim.Caching.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NoSQLDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person();
            MemcachedClientConfiguration mcConfig = new MemcachedClientConfiguration();
            mcConfig.AddServer("127.0.0.1:11211");//必须指定端口
            using (MemcachedClient client = new MemcachedClient(mcConfig))
            {
                var cas = client.GetWithCas("Name");
                Console.WriteLine("按任意键继续");
                Console.ReadKey();
                var res = client.Cas(Enyim.Caching.Memcached.StoreMode.Set, "Name", cas.Result + "1", cas.Cas);
                if (res.Result)
                {
                    Console.WriteLine("修改成功");
                }
                else
                {
                    Console.WriteLine("被别人改了");
                }
            }
            Console.ReadKey();
        }
    }

    // 标记序列化，即可保存至Memcached
    [Serializable]
    public class Person
    {
        public string Name { get; set; } = "Ryan";
        public int Age { get; set; } = 27;
    }
}
