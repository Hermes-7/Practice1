using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlHelper
{
	class transcation
	{
		static void Main(string[] args)
		{
			//接收用户输入
			string str = Console.ReadLine();
			//读取数据连接配置信息
			string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["hem09"].ConnectionString;
			//创建连接对象
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				//创建命令对象，第一个参数指定为存储过程的名称
				SqlCommand cmd = new SqlCommand("MyTrim", conn);
				//指定执行的命令类型为“存储过程”
				cmd.CommandType = CommandType.StoredProcedure;
				//提供存储过程的参数，这里是双向的参数
				SqlParameter p1 = new SqlParameter("@str", str);
				//执行前输出
				Console.WriteLine(p1.Value);
				//只有输入没有输出的时候，可以省略下面的这一句话
				p1.Direction = ParameterDirection.InputOutput;
				cmd.Parameters.Add(p1);
				//多个参数下，就多次添加
				//insert into employee values(@name,@gender,@birthday)
				//
				//SqlParameter p2 = new SqlParameter("@name", "");
				//p2.Direction = ParameterDirection.Output;
				//cmd.Parameters.Add(p2);
				//执行命令
				conn.Open();
				cmd.ExecuteNonQuery();
				//通过参数获得存储过程返回的值
				Console.WriteLine("==================");
				Console.WriteLine(p1.Value);
			}

		}
	}
}
