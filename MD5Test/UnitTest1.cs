using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MD5Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public string GetMD5(string str)
        {
            // 创建MD5对象
            MD5 md5 = MD5.Create();
            //开始加密，将字符串转换成直接数组
            byte[] buffer = Encoding.Default.GetBytes(str);
            //返回一个加密好的直接数组
            byte[] MD5Buffer = md5.ComputeHash(buffer);
            //将直接数组转换成字符串
            return Encoding.Default.GetString(MD5Buffer);
        }

        static void Main(string[] args)
        {
            UnitTest1 ut = new UnitTest1();
            string s = "Ryan";
            string md5 = ut.GetMD5(s);
        }
    }
}
