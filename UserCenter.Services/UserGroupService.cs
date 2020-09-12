using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.IServices;
using UserCenter.Services.Models;

namespace UserCenter.Services
{
    public class UserGroupService : IUserGroupService
    {
        private static UserGroupDTO ToUserGroupDTO(UserGroup group)
        {
            UserGroupDTO dto = new UserGroupDTO();
            dto.Id = group.Id;
            dto.Name = group.Name;
            return dto;
        }

        public async Task<UserGroupDTO[]> GetGroupsAsync(long userId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var user = await ctx.Users.SingleOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    return null;
                }

                var groups = user.Groups;
                List<UserGroupDTO> dtos = new List<UserGroupDTO>();
                foreach (var group in groups)
                {
                    dtos.Add(ToUserGroupDTO(group));
                }
                return dtos.ToArray();
            }
        }


        public async Task<UserGroupDTO> GetByIdAsync(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var group = await ctx.UserGroups.SingleOrDefaultAsync(e => e.Id == id);
                if (group == null)
                {
                    return null;
                }
                return ToUserGroupDTO(group);
            }
        }

        public async Task<UserDTO[]> GetGroupUsersAsync(long userGroupId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var group = await ctx.UserGroups.SingleOrDefaultAsync(e => e.Id == userGroupId);
                if (group == null)
                {
                    return null;
                }
                List<UserDTO> dtos = new List<UserDTO>();
                foreach (var user in group.Users)
                {
                    UserDTO userDTO = new UserDTO();
                    userDTO.Id = user.Id;
                    userDTO.NickName = user.NickName;
                    userDTO.PhoneNum = user.PhoneNum;
                    dtos.Add(userDTO);
                }
                return dtos.ToArray();
            }
        }

        public async Task RemoveUserFromGroupAsync(long userGroupId, long userId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var group = await ctx.UserGroups.SingleOrDefaultAsync(e => e.Id == userGroupId);
                if (group == null)
                {
                    throw new ArgumentException("userGroupId=" + userGroupId + "不存在");
                }
                var user = ctx.Users.SingleOrDefault(e => e.Id == userId);
                if (user == null)
                {
                    throw new ArgumentException("userId=" + userId + "不存在");
                }
                group.Users.Remove(user);
                user.Groups.Remove(group);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task AddUserToGroupAsync(long userGroupId, long userId)
        {

            using (MyDbContext ctx = new MyDbContext())
            {
                var group = await ctx.UserGroups.SingleOrDefaultAsync(e => e.Id == userGroupId);
                if (group == null)
                {
                    throw new ArgumentException("userGroupId=" + userGroupId + "不存在");
                }
                var user = ctx.Users.SingleOrDefault(e => e.Id == userId);
                if (user == null)
                {
                    throw new ArgumentException("userId=" + userId + "不存在");
                }
                group.Users.Add(user);
                user.Groups.Add(group);
                await ctx.SaveChangesAsync();
            }
        }
    }
}

