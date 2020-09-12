using DTO;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserCenter.Services.Models;

namespace UserCenter.Services
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class MyDbContext:DbContext
    {
        public MyDbContext() : base("name=connstr")
        { }
        public DbSet<AppInfo> Appinfos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //从所在程序集,加载所有继承了EntityTypeConfiguration的类为模型配置类
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
