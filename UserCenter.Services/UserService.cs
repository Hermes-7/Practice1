using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using UserCenter.IServices;
using UserCenter.Services.Models;

namespace UserCenter.Services
{
    public class UserService : IUserService
    {
        public async Task<long> AddNewAsync(string phoneNum, string nickName, string password)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                if (await ctx.Users.AnyAsync(u => u.PhoneNum == phoneNum))
                {
                    throw new ApplicationException($"手机号{nameof(phoneNum)}已经存在");
                }

                User user = new User();
                user.NickName = nickName;
                user.PhoneNum = phoneNum;
                string salt = new Random().Next(10000, 99999).ToString();
                string hash = MD5Helper(password, salt);
                user.PasswordHash = hash;
                user.PasswordSalt = salt;

                ctx.Users.Add(user);
                await ctx.SaveChangesAsync();
                return user.Id;
            }
        }

        public async Task<bool> CheckLoginAsync(string phoneNum, string password)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var user = await ctx.Users.SingleOrDefaultAsync(e => e.PhoneNum == phoneNum);
                if (user == null)
                {
                    return false;
                }
                string inputHash = MD5Helper(password,  user.PasswordSalt);
                return user.PasswordHash == inputHash;
            }
        }

        private static UserDTO ToDTO(User user)
        {
            UserDTO dto = new UserDTO();
            dto.Id = user.Id;
            dto.NickName = user.NickName;
            dto.PhoneNum = user.PhoneNum;
            return dto;
        }

        public async Task<UserDTO> GetByIdAsync(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var user = await ctx.Users.SingleOrDefaultAsync(e => e.Id == id);
                if (user == null)
                {
                    return null;
                }
                else
                {
                    return ToDTO(user);
                }
            }
        }

        public async Task<UserDTO> GetByPhoneNumAsync(string phoneNum)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var user = await ctx.Users.SingleOrDefaultAsync(e => e.PhoneNum == phoneNum);
                if (user == null)
                {
                    return null;
                }
                else
                {
                    return ToDTO(user);
                }
            }
        }

        public async Task<bool> UserExistsAsync(string phoneNum)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                return await ctx.Users.AnyAsync(e => e.PhoneNum == phoneNum);
            }
        }

        /// <summary>
		/// 获取MD5得值，没有转换成Base64的
		/// </summary>
		/// <param name="sDataIn">需要加密的字符串</param>
		/// <param name="move">偏移量</param>
		/// <returns>sDataIn加密后的字符串</returns>
		public string MD5Helper(string sDataIn, string move)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] byt, bytHash;
            byt = System.Text.Encoding.UTF8.GetBytes(move + sDataIn);
            bytHash = md5.ComputeHash(byt);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("x").PadLeft(2, '0');
            }
            return sTemp;
        }
    }
}
