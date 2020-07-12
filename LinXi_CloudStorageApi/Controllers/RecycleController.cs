using System;
using System.Linq;
using AutoMapper;
using LinXi_Common;
using System.Collections;
using LinXi_IService;
using LinXi_Model;
using LinXi_Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net.Http;

namespace LinXi_CloudStorageApi.Controllers
{
    /// <summary>
    /// 回收
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RecycleController : ControllerBase
    {
        #region 字段

        private ILogger<RecycleController> _logger;
        private ILxUsersService _ILxUsersService;
        private ILxResourceAccountService _ILxResourceAccountService;
        private ILxResourceService _ILxResourceService;
        private IServiceProvider _service;
        private ILxShareService _ILxShareService;
        private IMapper _IMapper;
        private IHttpContextAccessor _httpContext;
        private readonly static object obj = new object();

        private int UserId
        {
            get
            {
                return int.Parse(_httpContext.HttpContext.User.Claims.Where(u => u.Type == "UserId").FirstOrDefault().Value);
            }
        }

        #endregion 字段

        #region 构造函数注入

        /// <summary>
        /// 回收构造
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ILxUsersService"></param>
        /// <param name="service"></param>
        /// <param name="ILxResourceAccountService"></param>
        /// <param name="ILxResourceService"></param>
        /// <param name="ILxShareService"></param>
        /// <param name="IMapper"></param>
        /// <param name="httpContextAccessor"></param>
        public RecycleController(
            ILogger<RecycleController> logger,
            ILxUsersService ILxUsersService,
            IServiceProvider service,
            ILxResourceAccountService ILxResourceAccountService,
            ILxResourceService ILxResourceService,
            ILxShareService ILxShareService,
            IMapper IMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _ILxUsersService = ILxUsersService;
            _ILxResourceAccountService = ILxResourceAccountService;
            _ILxResourceService = ILxResourceService;
            _service = service;
            _ILxShareService = ILxShareService;
            _IMapper = IMapper;
            _httpContext = httpContextAccessor;
        }

        #endregion 构造函数注入

        /// <summary>
        /// 获取回收表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<IEnumerable<RecycleDTO>>> GetRecycleList(string SearchName = "")
        {
            //先获得该用户所有的回收信息
            var t = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxRecycle == true && u.LxResourceAccountTime.AddDays(10) > DateTime.Now && EF.Functions.Like(u.LxResourceAccountName, $"%{SearchName}%"))).ToList();

            Dictionary<int, LxResourceAccount> dc = new Dictionary<int, LxResourceAccount>();
            var filter = new List<LxResourceAccount>();

            foreach (var item in t)
            {
                dc.Add(item.LxResourceAccountId, item);
            }

            foreach (KeyValuePair<int, LxResourceAccount> item in dc)
            {
                //如果集合中，某个数据的id键是另外一个数据的父id，则另外一个数据过滤
                if (!dc.ContainsKey(item.Value.LxPid))
                {
                    filter.Add(item.Value);
                }
            }

            //转化成DTO
            List<RecycleDTO> FileList = _IMapper.Map<List<RecycleDTO>>(filter);

            if (FileList.Count == 0)
            {
                return new InfoResult<IEnumerable<RecycleDTO>>(FileList) { Code = 204 };
            }

            //获取文件名、文件资源类型ID、文件大小
            FileList.ForEach(r =>
            {
                var id = _ILxResourceAccountService.FindAsyncById(r.LxResourceAccountID).Result.LxResourceId;
                r.LxResourceAccountName = _ILxResourceAccountService.FindAsyncById(r.LxResourceAccountID).Result.LxResourceAccountName;
                r.LxResourceSize = _ILxResourceService.FindAsyncById(id).Result.LxResourceSize;
                r.LxResourceCategoryId = _ILxResourceService.FindAsyncById(id).Result.LxResourceCategoryId;
            });

            return new InfoResult<IEnumerable<RecycleDTO>>(FileList);
        }

        /// <summary>
        /// 从回收站回收文件
        /// </summary>
        /// <param name="jsondata">前台传的json，需要键为ArrayID，值为int数组的json</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<InfoResult<string>> CanceRecycle(JObject jsondata)
        {
            List<int> ls = new List<int>();
            foreach (var item in jsondata["AllRecycleID"])
            {
                ls.Add(Convert.ToInt32(item));
            }

            if (ls.Count == 0)
            {
                return new InfoResult<string>("请确保该文件存在！") { Code = 204 };
            }

            ls.ForEach(r =>
           {
               var ac = _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxResourceAccountId == r).Result.FirstOrDefault();
               HttpClient httpclient = new HttpClient();
               var z = httpclient.DeleteAsync($"http://localhost:63833/api/Recycle/CanceRecycleFile?UserID={UserId}&LxResourceAccountID={ac.LxResourceAccountId}").Result;
           });
            return new InfoResult<string>("恢复成功");
        }

        /// <summary>
        /// 从回收站回收文件
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<int> CanceRecycleFile(int UserID, int LxResourceAccountID)
        {
            //获取该文件与账号所关联的信息
            var ac = (await _ILxResourceAccountService.Search(u => u.LxResourceAccountId == LxResourceAccountID && u.LxUsersId == UserID)).FirstOrDefault();

            //获取该文件资源类型
            var lr = await _ILxResourceService.FindAsyncById(ac.LxResourceId);

            //如果是文件夹则要获取该文件夹下面的所有文件
            if (lr.LxResourceCategoryId == 1)
            {
                //获取该文件下面的所有文件
                List<LxResourceAccount> FileList = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserID && u.LxPid == LxResourceAccountID)).ToList();
                if (FileList.Count == 0)
                {
                    var ss = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserID && u.LxResourceAccountId == LxResourceAccountID)).FirstOrDefault();
                    ss.LxRecycle = false;
                    ss.LxResourceAccountTime = DateTime.Now;
                    await _ILxResourceAccountService.Edit(ss);
                }
                FileList.ForEach(u =>
                {
                    var t = CanceRecycleFile(UserID, u.LxResourceAccountId).Result;
                    var ss = _ILxResourceAccountService.Search(u => u.LxUsersId == UserID && u.LxResourceAccountId == LxResourceAccountID).Result.FirstOrDefault();
                    ss.LxRecycle = false;
                    ss.LxResourceAccountTime = DateTime.Now;
                    var tt = _ILxResourceAccountService.Edit(ss).Result;
                });
                return 1;
            }
            else
            {
                ac.LxRecycle = false;
                ac.LxResourceAccountTime = DateTime.Now;
                await _ILxResourceAccountService.Edit(ac);
                return 1;
            }
        }
    }
}