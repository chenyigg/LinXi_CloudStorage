using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinXi_Common;
using LinXi_IService;
using LinXi_Model;
using LinXi_Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LinXi_CloudStorageApi.Controllers
{
    /// <summary>
    /// 图片
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PictureController : ControllerBase
    {
        #region 字段

        private ILogger<PictureController> _logger;
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
        /// 图片构造
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ILxUsersService"></param>
        /// <param name="service"></param>
        /// <param name="ILxResourceAccountService"></param>
        /// <param name="ILxResourceService"></param>
        /// <param name="IMapper"></param>
        /// <param name="httpContextAccessor"></param>
        public PictureController(
            ILogger<PictureController> logger,
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
        /// 预览图片流
        /// </summary>
        /// <param name="LxResourceAccountId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PreviewPic(int LxResourceAccountId)
        {
            var ac = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxResourceAccountId == LxResourceAccountId)).FirstOrDefault();
            var lr = await _ILxResourceService.FindAsyncById(ac.LxResourceId);
            var filepath = $@"{Directory.GetCurrentDirectory()}\AllFile\{lr.LxResourcePath}";
            var provider = new FileExtensionContentTypeProvider();
            FileInfo fileInfo = new FileInfo(filepath);
            var ext = fileInfo.Extension;
            new FileExtensionContentTypeProvider().Mappings.TryGetValue(ext, out var contenttype);
            return File(System.IO.File.ReadAllBytes(filepath), contenttype ?? "application/octet-stream", ac.LxResourceAccountName + ext);
        }

        /// <summary>
        /// 获取所有图片
        /// </summary>
        /// <param name="SearchName">查询名</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<Dictionary<DateTime, List<PictureDTO>>>> GetAll(string SearchName = "")
        {
            //查询出所有符合条件的照片
            List<LxResourceAccount> ac = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxRecycle == false && u.LxResource.LxResourceCategoryId == 2 && EF.Functions.Like(u.LxResourceAccountName, $"%{SearchName}%"))).ToList();

            //准备好日期容器
            Dictionary<DateTime, List<PictureDTO>> dc = new Dictionary<DateTime, List<PictureDTO>>();

            //转化
            List<PictureDTO> pictureDTOs = _IMapper.Map<List<PictureDTO>>(ac);

            //遍历，看日期是否一致
            foreach (var item in pictureDTOs)
            {
                if (dc.ContainsKey(item.PicTime))
                {
                    dc[item.PicTime].Add(item);
                }
                else
                {
                    dc.Add(item.PicTime, new List<PictureDTO>());
                    dc[item.PicTime].Add(item);
                }
            }
            dc.Reverse();
            return dc.Count == 0 ? new InfoResult<Dictionary<DateTime, List<PictureDTO>>>(dc) { Code = 204 } :
             new InfoResult<Dictionary<DateTime, List<PictureDTO>>>(dc);
        }
    }
}