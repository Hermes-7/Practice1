using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SocketDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);//TCP、UDP。
            //socket.Connect(new DnsEndPoint("127.0.0.1", 8080));//连接服务器。http协议默认的端口号是80。每个服务器软件监听一个端口（别的软件就不能监听这个端口了），发送给这个端口的数据只会被这个服务器软件接收到。
            socket.Connect("127.0.0.1", 8080);//连接服务器。http协议
            using (NetworkStream netStream = new NetworkStream(socket))//读写socket通讯数据的流
            using (StreamWriter writer = new StreamWriter(netStream))
            {
                writer.WriteLine("GET /index.html HTTP/1.1");//每一行指令都回车一下。相对于网站根目录的路径，不要写在服务器上的全路径。
                writer.WriteLine("Host: 127.0.0.1:8080");
                writer.WriteLine();//空行回车，表示指令结束
            }
            using (NetworkStream netStream = new NetworkStream(socket))
            using (StreamReader reader = new StreamReader(netStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            Console.ReadKey();
        }
    }
}
