using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using UserCenter.NETSDK;

namespace UserCenter.NetAPITest
{
    class Program
    {
        static void Main(string[] args)
        {
            UserAPI user = new UserAPI("123456", "admin", "http://127.0.0.1:8888/api/v1/");
            long id = user.addNewAsync("123", "123", "123").Result;
            Console.WriteLine(id);
            Console.ReadKey();
        }
    }
}
