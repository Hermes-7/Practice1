using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIrstADONet
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn =
        new MySqlConnection("Server=localhost;SslMode=none;Database=demo;uid=root;pwd=123456;Charset=utf8"))
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();//Reader执行时，连接不能关闭
                cmd.CommandText = "Select * from t_users";
                using (MySqlDataReader reader = cmd.ExecuteReader()) {
                    dt.Load(reader); // 加载到table中（存入本地内存），此时数据库连接已断开
                }
            }


            //从DataTable中获取已离线至本地的数据，完全脱离数据库连接
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                // int? 可null int，详见笔记“.Net --> base --> 整数类型”
                int? id = row.IsNull("Id") ? (int?)null : (int)row["Id"];//NULL处理
                string username = row.IsNull("username") ? null : (string)row["username"];
                Console.WriteLine("主键：" + id + ", 用户名：" + username);
            }

            Console.ReadKey();
        }
    }
}
