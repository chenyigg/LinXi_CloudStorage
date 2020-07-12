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

namespace LinXi_CloudStorageApi.Controllers
{
    /// <summary>
    /// 文件分享
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ShareController : ControllerBase
    {
        #region 字段

        private ILogger<ShareController> _logger;
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
        /// 用户构造
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ILxUsersService"></param>
        /// <param name="service"></param>
        /// <param name="ILxResourceAccountService"></param>
        /// <param name="ILxResourceService"></param>
        /// <param name="ILxShareService"></param>
        /// <param name="IMapper"></param>
        /// <param name="httpContextAccessor"></param>
        public ShareController(
            ILogger<ShareController> logger,
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
        /// 获取分享码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<InfoResult<ShareDTO>> GetShareCode([FromForm] int LxResourceAccountID, [FromForm] string LxShareTime)
        {
            //判断该文件是否存在和是否被回收
            if ((await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxResourceAccountId == LxResourceAccountID && u.LxRecycle == false)).FirstOrDefault() == null)
            {
                return new InfoResult<ShareDTO>(new ShareDTO()) { Msg = "您分享的文件有误" };
            }

            //创建分享
            LxShare share = _ILxShareService.CreateShare(UserId, LxResourceAccountID, LxShareTime);
            if (share == null)
            {
                return new InfoResult<ShareDTO>(new ShareDTO()) { Msg = "分享失败" };
            }
            else
            {
                return new InfoResult<ShareDTO>(_IMapper.Map<ShareDTO>(share));
            }
        }

        /// <summary>
        /// 判断该链接是否存在
        /// </summary>
        /// <param name="ShareLink"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<string>> HasShareLink(string ShareLink)
        {
            var share = (await _ILxShareService.Search(u => u.LxShareLink == ShareLink)).FirstOrDefault();
            if (share == null)
            {
                return new InfoResult<string>("不存在");
            }
            else
            {
                if (share.LxShareTime != "永久有效")
                {
                    var day = Convert.ToInt32(share.LxShareTime.Split('天')[0]);
                    return share.LxCreateTime.AddDays(day) < DateTime.Now ? new InfoResult<string>("不存在") : new InfoResult<string>("存在");
                }
                return new InfoResult<string>("存在");
            }
        }

        /// <summary>
        /// 比对提取码
        /// </summary>
        /// <param name="ShareLink">分享链接</param>
        /// <param name="ValidCode">提取码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<ShareDetailsDTO>> CompareValidCode(string ShareLink, string ValidCode)
        {
            var share = (await _ILxShareService.Search(u => u.LxShareLink == ShareLink && u.LxValidCode == ValidCode)).FirstOrDefault();
            return share == null ?
                   new InfoResult<ShareDetailsDTO>(new ShareDetailsDTO()) { Msg = "不存在" } :
                   new InfoResult<ShareDetailsDTO>(new ShareDetailsDTO() { LxResourceAccountId = share.LxResourceAccountId, LxShareId = share.LxShareId }) { Msg = "存在" };
        }

        /// <summary>
        /// 分享表获取文件自身
        /// </summary>
        /// <param name="LxShareId">分享表ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<AllFileDTO>> FileOwn(int LxShareId)
        {
            //通过分享表ID获取资源
            var lxShare = await _ILxShareService.FindAsyncById(LxShareId);

            //查询该用户所有的文件夹
            var FileList = (await _ILxResourceAccountService.Search(u => u.LxUsersId == lxShare.LxUserId && u.LxResourceAccountId == lxShare.LxResourceAccountId && u.LxRecycle == false)).FirstOrDefault();

            //通过AutoMapper进行转化
            var allFileDTO = _IMapper.Map<AllFileDTO>(FileList);

            //准备容器装每个文件夹的文件大小和文件类型ID
            var Size = new List<string>();
            var CategoryID = new List<int>();

            //填充容器

            Size.Add((await _ILxResourceService.FindAsyncById(allFileDTO.lxResourceID)).LxResourceSize);
            CategoryID.Add((await _ILxResourceService.FindAsyncById(allFileDTO.lxResourceID)).LxResourceCategoryId);

            //赋值并返回数据
            int index = 0;
            allFileDTO.lxResourceSize = Size[index];
            allFileDTO.LxResourceCategoryId = CategoryID[index];

            return new InfoResult<AllFileDTO>(allFileDTO);
        }

        /// <summary>
        /// 获取分享表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<IEnumerable<ShareFileDTO>>> GetShareList()
        {
            //先获得该用户所有的分享信息
            var t = (await _ILxShareService.Search(u => u.LxUserId == UserId)).ToList();

            //转化成DTO
            List<ShareFileDTO> FileList = _IMapper.Map<List<ShareFileDTO>>(t);

            if (FileList.Count == 0)
            {
                return new InfoResult<IEnumerable<ShareFileDTO>>(FileList) { Code = 204 };
            }

            //获取文件名、文件资源类型ID、文件大小
            FileList.ForEach(r =>
             {
                 var id = _ILxResourceAccountService.FindAsyncById(r.LxResourceAccountID).Result.LxResourceId;
                 r.LxResourceAccountName = _ILxResourceAccountService.FindAsyncById(r.LxResourceAccountID).Result.LxResourceAccountName;
                 r.LxResourceSize = _ILxResourceService.FindAsyncById(id).Result.LxResourceSize;
                 r.LxResourceCategoryId = _ILxResourceService.FindAsyncById(id).Result.LxResourceCategoryId;
             });

            return new InfoResult<IEnumerable<ShareFileDTO>>(FileList);
        }

        /// <summary>
        /// 取消分享
        /// </summary>
        /// <param name="jsondata">前台传的json，需要键为ArrayID，值为int数组的json</param>
        /// <returns></returns>
        [HttpDelete]
        public InfoResult<string> CanceShare(JObject jsondata)
        {
            List<int> ls = new List<int>();
            foreach (var item in jsondata["ArrayID"])
            {
                ls.Add(Convert.ToInt32(item));
            }
            if (ls.Count == 0)
            {
                return new InfoResult<string>("请确保该分享存在！") { Code = 204 };
            }
            ls.ForEach(r =>
           {
               var share = _ILxShareService.Search(u => u.LxUserId == UserId && u.LxShareId == r).Result.FirstOrDefault();
               var z = _ILxShareService.Delete(share).Result;
           });
            return new InfoResult<string>("取消分享成功");
        }

        /// <summary>
        /// 获取所有图片
        /// </summary>
        /// <param name="SearchName">查询名</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<IEnumerable<ShareFileDTO>>> GetAll(string SearchName = "")
        {
            //查询出所有符合条件的照片
            List<LxResourceAccount> ac = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && EF.Functions.Like(u.LxResourceAccountName, $"%{SearchName}%"))).ToList();

            //分享容器
            List<LxShare> lxShares = new List<LxShare>();

            //遍历所有的账号资源id
            ac.ForEach(u =>
           {
               //判断该资源id在该用户的分享表内是否存在
               var ss = _ILxShareService.Search(z => z.LxUserId == UserId && z.LxResourceAccountId == u.LxResourceAccountId).Result.ToList();
               //如果存在，则将这些记录存进容器
               if (ss.Count != 0)
               {
                   ss.ForEach(r =>
                   {
                       lxShares.Add(r);
                   });
               }
           });

            //转化成DTO
            List<ShareFileDTO> FileList = _IMapper.Map<List<ShareFileDTO>>(lxShares);

            if (FileList.Count == 0)
            {
                return new InfoResult<IEnumerable<ShareFileDTO>>(FileList) { Code = 204 };
            }

            //获取文件名、文件资源类型ID、文件大小
            FileList.ForEach(r =>
           {
               var id = _ILxResourceAccountService.FindAsyncById(r.LxResourceAccountID).Result.LxResourceId;
               r.LxResourceAccountName = _ILxResourceAccountService.FindAsyncById(r.LxResourceAccountID).Result.LxResourceAccountName;
               r.LxResourceSize = _ILxResourceService.FindAsyncById(id).Result.LxResourceSize;
               r.LxResourceCategoryId = _ILxResourceService.FindAsyncById(id).Result.LxResourceCategoryId;
           });

            return new InfoResult<IEnumerable<ShareFileDTO>>(FileList);
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="LxShareId">分享表id</param>
        /// <param name="LxResourceAccountId">资源账户ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> FileDown(int LxShareId, int LxResourceAccountId)
        {
            var share = (await _ILxShareService.Search(u => u.LxShareId == LxShareId && u.LxResourceAccountId == LxResourceAccountId)).FirstOrDefault();
            if (share == null)
            {
                return NoContent();
            }
            var ac = (await _ILxResourceAccountService.Search(u => u.LxResourceAccountId == LxResourceAccountId && u.LxRecycle == false)).FirstOrDefault();
            if (ac == null)
            {
                return NoContent();
            }
            var lr = await _ILxResourceService.FindAsyncById(ac.LxResourceId);
            var filepath = $@"{Directory.GetCurrentDirectory()}\AllFile\{lr.LxResourcePath}";
            var provider = new FileExtensionContentTypeProvider();
            FileInfo fileInfo = new FileInfo(filepath);
            var ext = fileInfo.Extension;
            new FileExtensionContentTypeProvider().Mappings.TryGetValue(ext, out var contenttype);
            return File(System.IO.File.ReadAllBytes(filepath), contenttype ?? "application/octet-stream", ac.LxResourceAccountName + ext);
        }

        /// <summary>
        /// 获取文件所有者图像
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFileOwnPic(int ShareID)
        {
            var UserID = (await _ILxShareService.FindAsyncById(ShareID)).LxUserId;
            var filepath = $@"{Directory.GetCurrentDirectory()}\AllFile\LoginPic\{(await _ILxUsersService.FindAsyncById(UserID)).LxUsersPic}";
            var provider = new FileExtensionContentTypeProvider();
            FileInfo fileInfo = new FileInfo(filepath);
            var ext = fileInfo.Extension;
            new FileExtensionContentTypeProvider().Mappings.TryGetValue(ext, out var contenttype);
            return File(System.IO.File.ReadAllBytes(filepath), contenttype ?? "application/octet-stream", $"{ fileInfo.Name.Substring(0, fileInfo.Name.IndexOf("."))}{ ext}");
        }
    }
}