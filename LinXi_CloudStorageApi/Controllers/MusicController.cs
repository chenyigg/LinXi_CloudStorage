using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LinXi_Common;
using LinXi_IService;
using LinXi_Model;
using LinXi_Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LinXi_CloudStorageApi.Controllers
{
    /// <summary>
    /// 音乐控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MusicController : ControllerBase
    {
        #region 字段

        private ILogger<MusicController> _logger;
        private ILxUsersService _ILxUsersService;
        private ILxResourceAccountService _ILxResourceAccountService;
        private ILxResourceService _ILxResourceService;
        private IServiceProvider _service;
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
        /// 音乐构造
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ILxUsersService"></param>
        /// <param name="service"></param>
        /// <param name="ILxResourceAccountService"></param>
        /// <param name="ILxResourceService"></param>
        /// <param name="IMapper"></param>
        /// <param name="httpContextAccessor"></param>
        public MusicController(
            ILogger<MusicController> logger,
            ILxUsersService ILxUsersService,
            IServiceProvider service,
            ILxResourceAccountService ILxResourceAccountService,
            ILxResourceService ILxResourceService,
            IMapper IMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _ILxUsersService = ILxUsersService;
            _ILxResourceAccountService = ILxResourceAccountService;
            _ILxResourceService = ILxResourceService;
            _service = service;
            _IMapper = IMapper;
            _httpContext = httpContextAccessor;
        }

        #endregion 构造函数注入

        /// <summary>
        /// 预览音乐流
        /// </summary>
        /// <param name="LxResourceAccountId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PreviewMusic(int LxResourceAccountId)
        {
            var ac = _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxResourceAccountId == LxResourceAccountId && u.LxResource.LxResourceCategoryId == 4).Result.FirstOrDefault();
            if (ac == null)
            {
                return NoContent();
            }
            var lr = _ILxResourceService.Search(u => u.LxResourceId == ac.LxResourceId).Result.FirstOrDefault();
            var filepath = $@"{Directory.GetCurrentDirectory()}\AllFile\{lr.LxResourcePath}";
            var provider = new FileExtensionContentTypeProvider();
            FileInfo fileInfo = new FileInfo(filepath);
            var ext = fileInfo.Extension;
            new FileExtensionContentTypeProvider().Mappings.TryGetValue(ext, out var contenttype);
            return File(System.IO.File.ReadAllBytes(filepath), contenttype ?? "application/octet-stream", ac.LxResourceAccountName + ext);
        }

        /// <summary>
        /// 获取所有文件
        /// </summary>
        /// <param name="SearchName">模糊查询</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<IEnumerable<AllFileDTO>>> GetAll(string SearchName = "")
        {
            //查询出所有符合条件的视频
            List<LxResourceAccount> FileList = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxRecycle == false && u.LxResource.LxResourceCategoryId == 4 && EF.Functions.Like(u.LxResourceAccountName, $"%{SearchName}%"))).ToList();

            //通过AutoMapper进行转化
            var allFileDTO = _IMapper.Map<List<AllFileDTO>>(FileList);

            //准备容器装每个文件夹的文件大小和文件类型ID
            var Size = new List<string>();
            var CategoryID = new List<int>();

            //填充容器
            //填充容器
            FileList.ForEach(r =>
           {
               Size.Add(_ILxResourceService.FindAsyncById(r.LxResourceId).Result.LxResourceSize);
               CategoryID.Add(_ILxResourceService.FindAsyncById(r.LxResourceId).Result.LxResourceCategoryId);
           });

            //赋值并返回数据
            int index = 0;
            allFileDTO.ForEach((u) =>
            {
                u.lxResourceSize = Size[index];
                u.LxResourceCategoryId = CategoryID[index];
                index++;
            });

            return new InfoResult<IEnumerable<AllFileDTO>>(allFileDTO);
        }
    }
}