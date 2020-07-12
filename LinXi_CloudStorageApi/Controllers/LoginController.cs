using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using LinXi_Common;
using LinXi_IService;
using LinXi_Model;
using LinXi_Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    /// 登录校验
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowCors")]
    [Authorize]
    public class LoginController : ControllerBase
    {
        #region 注入字段

        private readonly ILogger<LoginController> _logger;
        private readonly ILxUsersService _ILxUsersService;
        private readonly IServiceProvider _service;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion 注入字段

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ILxUsersService"></param>
        /// <param name="httpClientFactory"></param>
        /// <param name=" httpContextAccessor"></param>
        /// <param name="service"></param>
        public LoginController(ILogger<LoginController> logger, ILxUsersService ILxUsersService, IServiceProvider service,
            IHttpClientFactory httpClientFactory,
              IHttpContextAccessor httpContextAccessor
         )
        {
            _logger = logger;
            _ILxUsersService = ILxUsersService;
            _service = service;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        #region 登录部分

        ///[ApiExplorerSettings(IgnoreApi = true)] 该特性隐藏Action 在SwaggerUI的显示
        /// <summary>
        /// 登录校验
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<InfoResult<UserDTO>> CloudLogin(UserDTO User)
        {
            //string json = new JWTTool((IConfiguration)_service.GetService(typeof(IConfiguration)), _logger).DecodingToken(token);
            //判断模型注解
            if (ModelState.IsValid)
            {
                //判断验证码
                HttpContext.Session.TryGetValue("LoginCode", out byte[] LoginCode);
                if (LoginCode == null || Encoding.UTF8.GetString(LoginCode) != User.Code)
                {
                    return new InfoResult<UserDTO>()
                    {
                        Msg = "验证码错误",
                        Entity = new UserDTO { ValidCode = "no", ModelState = "ok", state = "no" }
                    };
                }
                else
                {
                    var client = _httpClientFactory.CreateClient();
                    //var searchU = _ILxUsersService.Search(u => u.LxUsersLoginName == User.LxUsersLoginName && u.LxUsersLoginPwd == User.LxUsersLoginPwd).FirstOrDefault();

                    #region 后端请求注解

                    //var dic = new Dictionary<string, object>
                    //{
                    //   { "grant_type", "client_credentials" },
                    //   { "client_id","LinXi_Client Clound" },
                    //   { "client_secret","ChenyiggSecret" },
                    //   { "scope","LinXi_scope"}
                    //};

                    //client.SetBearerToken(oauth_job["access_token"].ToString());
                    //var data_rep = await client.GetStringAsync("http://localhost:63833/api/login/mysend");
                    //var data_job = JsonConvert.DeserializeObject<JObject>(data_rep);

                    #endregion 后端请求注解

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
                        return new InfoResult<UserDTO>()
                        {
                            Msg = "账号密码不匹配",
                            Entity = new UserDTO { ValidCode = "ok", ModelState = "ok", state = "no" }
                        };
                    }
                    else
                    {
                        var searchU = (await _ILxUsersService.Search(u => u.LxUsersLoginName == User.LxUsersLoginName && u.LxUsersLoginPwd == User.LxUsersLoginPwd)).FirstOrDefault();

                        //Redis储存用户token，作单点登录
                        RedisHelper.Set($"token_{searchU.LxUsersId}", oauth_job["access_token"].ToString());

                        return new InfoResult<UserDTO>()
                        {
                            Msg = oauth_job["access_token"].ToString(),
                            Entity = new UserDTO { LxUsersLoginName = User.LxUsersLoginName, ValidCode = "ok", ModelState = "ok", state = "ok" }
                        };
                    }
                }
            }
            else
            {
                return new InfoResult<UserDTO>()
                {
                    Msg = "模型验证未通过",
                    Entity = new UserDTO { ValidCode = "no", ModelState = "no", state = "no" }
                };
            }
        }

        /// <summary>
        /// 返回验证码,此处保存登录验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidCode()
        {
            string code = new ValidCode().CreateValidateCode(4);
            HttpContext.Session.SetString("LoginCode", code);
            HttpContext.Session.TryGetValue("LoginCode", out byte[] codes);
            return File(new ValidCode().CreateValidateGraphic(Encoding.UTF8.GetString(codes)), @"image/jpeg");
        }

        ///<summary>
        /// 测试异步方法
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<LxUsers>> mysend(int id)
        {
            return await _ILxUsersService.FindAsyncById(id);
            //return new JsonResult(
            //  from c in User.Claims select new { c.Type, c.Value }
            //  );
            List<int> ls = new List<int>();
            ls.AddRange(Enumerable.Range(1, 10));
            ls.ForEach(r =>
            {
                CreatedAtAction("Gets", null);
            });
        }

        /// <summary>
        /// 获取token信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetInfo()
        {
            var Claims = HttpContext.User.Claims;
            var id = Claims.Where(u => u.Type == "UserId").FirstOrDefault().Value;
            return new JsonResult(from c in HttpContext.User.Claims select new { c.Type, c.Value });
        }

        [HttpGet]
        public IActionResult Gets()
        {
            var Claims = HttpContext.User.Claims;
            var id = Claims.Where(u => u.Type == "UserId").FirstOrDefault().Value;
            return new JsonResult(from c in HttpContext.User.Claims select new { c.Type, c.Value });
        }

        #endregion 登录部分

        #region 异常界面

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        #endregion 异常界面
    }
}