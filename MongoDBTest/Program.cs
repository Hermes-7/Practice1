using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            //相当于数据库
            IMongoDatabase database = client.GetDatabase("TestDb1");
            //相当于表
            IMongoCollection<Person> teachers = database.GetCollection<Person>("Persons");
            var filter = Builders<Person>.Filter.Where(p => p.Age <= 28);
            teachers.DeleteMany(filter);
        }

        public void add() 
        {
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int Age { get; set; }
        public string gender { get; set; }
    }
}
