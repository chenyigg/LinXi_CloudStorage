using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LinXi_Common;
using LinXi_IService;
using LinXi_Model;
using LinXi_Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack.Redis;

namespace LinXi_CloudStorageApi.Controllers
{
    /// <summary>
    /// 注册
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
        #region 注入字段

        private readonly ILogger<RegisterController> _logger;
        private readonly ILxUsersService _ILxUsersService;
        private readonly IServiceProvider _service;
        private readonly IHttpClientFactory _httpClientFactory;

        #endregion 注入字段

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ILxUsersService"></param>
        /// <param name="service"></param>
        public RegisterController(ILogger<RegisterController> logger, ILxUsersService ILxUsersService, IServiceProvider service, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _ILxUsersService = ILxUsersService;
            _service = service;
            _httpClientFactory = httpClientFactory;
        }

        #region 注册部分

        /// <summary>
        /// 注册Post
        /// </summary>
        /// <param name="User">用户</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<InfoResult<RegisterDTO>> CreateUser(RegisterDTO User)
        {
            //判断模型注解
            if (ModelState.IsValid)
            {
                //判断验证码
                HttpContext.Session.TryGetValue("RegisterCode", out byte[] Register);
                if (Register == null || Encoding.UTF8.GetString(Register) != User.Code)
                {
                    return new InfoResult<RegisterDTO>()
                    {
                        Msg = "验证码不匹配",
                        Entity = new RegisterDTO() { ValidCode = "no", ModelState = "ok", state = "no" }
                    };
                }
                else
                {
                    LxUsers users = new LxUsers()
                    {
                        LxUsersLoginName = User.LxUsersLoginName,
                        LxUsersLoginPwd = User.LxUsersLoginPwd,
                        LxUsersLoginEmail = User.LxUsersLoginEmail,
                        LxUsersPic = "DefaultPic.png"
                    };

                    try
                    {
                        if (await _ILxUsersService.Add(users) > 0)
                        {
                            var client = _httpClientFactory.CreateClient();
                            var dic = new Dictionary<string, object>
                            {
                               { "grant_type", "password" },
                               { "client_id","client2" },
                               { "client_secret","secret" },
                               {"username",User.LxUsersLoginName},
                               { "UserId",User.LxUsersId},
                               { "password",User.LxUsersLoginPwd}
                            };
                            var dic_str = dic.Select(m => m.Key + "=" + m.Value).DefaultIfEmpty().Aggregate((m, n) => m + "&" + n);

                            HttpContent httpcontent = new StringContent(dic_str);
                            httpcontent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                            var oauth_rep = await client.PostAsync("http://localhost:63834/connect/token", httpcontent);
                            var oauth_str = await oauth_rep.Content.ReadAsStringAsync();
                            var oauth_job = JsonConvert.DeserializeObject<JObject>(oauth_str);
                            if (oauth_job["access_token"] == null)
                            {
                                return new InfoResult<RegisterDTO>()
                                {
                                    Msg = "账号密码不匹配",
                                    Entity = new RegisterDTO { ValidCode = "ok", ModelState = "ok", state = "no" }
                                };
                            }
                            else
                            {
                                RedisHelper.Set($"token_{users.LxUsersId}", oauth_job["access_token"].ToString());
                                return new InfoResult<RegisterDTO>()
                                {
                                    Msg = oauth_job["access_token"].ToString(),
                                    Entity = new RegisterDTO { LxUsersLoginName = User.LxUsersLoginName, ValidCode = "ok", ModelState = "ok", state = "ok" }
                                };
                            }
                        }

                        return new InfoResult<RegisterDTO>()
                        {
                            Msg = "账号密码不匹配",
                            Entity = new RegisterDTO { ValidCode = "ok", ModelState = "ok", state = "no" }
                        };
                    }
                    catch (Exception)
                    {
                        return new InfoResult<RegisterDTO>()
                        {
                            Msg = "注册失败！",
                            Entity = new RegisterDTO() { ValidCode = "ok", ModelState = "ok", state = "no" }
                        };
                    }
                }
            }
            else
            {
                return new InfoResult<RegisterDTO>()
                {
                    Msg = "账号密码不匹配",
                    Entity = new RegisterDTO() { ValidCode = "no", ModelState = "no", state = "no" }
                };
            }
        }

        /// <summary>
        /// 判断抢注
        /// </summary>
        /// <param name="LxUsersLoginName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<RegisterDTO>> HasAccount(string LxUsersLoginName)
        {
            LxUsers user = (await _ILxUsersService.Search(use => use.LxUsersLoginName == LxUsersLoginName)).FirstOrDefault();

            //如果没找到，则证明没被抢注
            return user == null ? new InfoResult<RegisterDTO>(new RegisterDTO() { state = "ok" }) : new InfoResult<RegisterDTO>(new RegisterDTO() { state = "no" });
        }

        /// <summary>
        /// 发送邮件，此处保存注册验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public InfoResult<RegisterDTO> SendEmail(string Email)
        {
            bool b = StaticHelper.SendEmail(Email, out int RegisterCode);

            if (RegisterCode != -1)
            {
                HttpContext.Session.SetString("RegisterCode", RegisterCode.ToString());
            }

            return b ? new InfoResult<RegisterDTO>(new RegisterDTO() { state = "ok" }) : new InfoResult<RegisterDTO>(new RegisterDTO() { state = "no" });
        }

        #endregion 注册部分
    }
}