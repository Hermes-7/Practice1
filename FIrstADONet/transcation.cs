using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIrstADONet
{
    class transcation
    {
        static void Main(string[] args)
        {
            MySqlHelper.CreateConnection();
            MySqlTransaction tx = conn.BeginTransaction();
            try
            {
                MySqlHelper.ExecuteNonQuery(conn, "Update t_accounts Set Amount=Amount-1000 where Number='0001'");
                string s = null; s.ToLower();
                MySqlHelper.ExecuteNonQuery(conn, "Update t_accounts Set Amount=Amount+1000 where Number='0002'");
                tx.Commit();
            }
            catch (Exception ex)
            {
                tx.Rollback();
            }
        }
    }
}
