using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.IServices
{
    public interface IAppInfoService : IServiceTag
    {
        Task<AppInfoDTO> GetByAppKeyAsync(string appKey);
    }
}
