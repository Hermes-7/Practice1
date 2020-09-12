using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MongodbWinFrame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            //相当于数据库
            IMongoDatabase database = client.GetDatabase("TestDb1");
            IMongoCollection<Person> collection = database.GetCollection<Person>("Persons");
            IMongoCollection<BsonDocument> persons = database.GetCollection<BsonDocument>("Persons");
            var filter1 = Builders<BsonDocument>.Filter.Gt("Age", 5);
            using (var personsCursor = await persons.FindAsync(filter1))
            {
                foreach (var p in await personsCursor.ToListAsync())
                {
                    MessageBox.Show(p.GetValue("Name").AsString);
                }
            }
        }
    }

    public class Person
    {
        // 获取数据的时候，如果MongoDB表中存在“ObjectId”类型的数据，那么获取对象也应该添加对应类型
        //public MongoDB.Bson.ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
