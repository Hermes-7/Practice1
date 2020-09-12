using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.NETSDK
{
    //第二层封装，简化API接口的调用步骤
    public class UserAPI
    {
        private string appKey;
        private string appSecret;
        private string serverRoot;
        public UserAPI(string appKey, string appSecret, string serverRoot)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverRoot = serverRoot;
        }

        public async Task<long> addNewAsync(string phoneNum, string nickName, string password)
        {
            SDKClient client = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["phoneNum"] = phoneNum;
            data["nickName"] = nickName;
            data["password"] = password;
            var result = await client.GetAsync("User/AddNew", data);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                long id = JsonConvert.DeserializeObject<long>(result.Result);
                return id;
            }
            else
            {
                throw new ApplicationException($"新增失败，状态码{nameof(result.StatusCode)},响应报文{nameof(result.Result)}");
            }
        }
    }
}
