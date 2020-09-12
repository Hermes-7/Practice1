using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.Services.Models;

namespace UserCenter.Services
{
    class UserGroupConfig : EntityTypeConfiguration<UserGroup>
    {
        public UserGroupConfig()
        {
            this.ToTable("T_Usergroups");
        }
    }
}
