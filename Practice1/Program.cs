using System;

namespace Practice1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    interface Iintegrface
    {
        void method(); // 无返回值
        string method1(); //返回string类型值
        string property { get; set; } // 可读可写属性
        string property1 { get; } //只读属性
        string property2 { set; } //只写属性
    }
}
