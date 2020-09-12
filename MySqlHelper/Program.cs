using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = @"StudentInfo.txt";
            StreamReader fileReader = new StreamReader(filename);
            string temp;
            //string sql = "insert into PersonList values(@name,@phone,@birthday,@email)";


            StringBuilder sb = new StringBuilder("insert into PersonList values");
            int i = 0;
            while ((temp = fileReader.ReadLine()) != null)
            {
                string[] temp1 = temp.Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries);

                sb.Append("('" + temp1[0] + "','" + temp1[1] + "','" + temp1[2] + "','" + temp1[3] + "'),");

                i++;
                if (i == 1000)
                {
                    i = 0;
                    using (MySqlConnection conn = new MySqlConnection("server=localhost;database=t_users;uid=sa;pwd=123"))
                    {
                        MySqlCommand cmd = new MySqlCommand(sb.ToString().Trim(','), conn);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    sb = new StringBuilder("insert into PersonList values");
                }
            }

            // 将最后不足1000条的记录，存入数据库
            using (MySqlConnection conn = new MySqlConnection("server=localhost;database=t_users;uid=sa;pwd=123"))
            {
                MySqlCommand cmd = new MySqlCommand(sb.ToString().Trim(','), conn);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("OK");
            Console.ReadKey();

        }
    }
}
