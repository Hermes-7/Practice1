﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UserCenter.IServices;

namespace UserCenter.OpenAPI.Controllers.v1
{
    public class UserGroupController : ApiController
    {
        public IUserGroupService GroupService { get; set; }

        [HttpGet]
        public async Task<UserGroupDTO> GetById(long id)
        {
            return await GroupService.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<UserGroupDTO[]> GetGroups(long userId)
        {
            return await GroupService.GetGroupsAsync(userId);
        }

        [HttpGet]

        public async Task<UserDTO[]> GetGroupUser(long userGroupId)
        {
            return await GroupService.GetGroupUsersAsync(userGroupId);
        }

        [HttpGet]
        public async Task RemoveUserFromGroup(long userGroupId, long userId)
        {
            await GroupService.RemoveUserFromGroupAsync(userGroupId, userId);

        }

        [HttpGet]
        public async Task AddUserToGroup(long userGroupId, long userId)
        {
            await GroupService.AddUserToGroupAsync(userGroupId, userId);
        }
    }
}