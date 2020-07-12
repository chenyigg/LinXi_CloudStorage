using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LinXi_IService;
using LinXi_Model;
using LinXi_Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;

namespace LinXi_CloudStorageApi.Controllers
{
    /// <summary>
    /// 获取用户信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        #region 字段

        private ILogger<UserController> _logger;
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
        /// 用户构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ILxUsersService"></param>
        /// <param name="service"></param>
        /// <param name="ILxResourceAccountService"></param>
        /// <param name="ILxResourceService"></param>
        /// <param name="ILxShareService"></param>
        /// <param name="IMapper"></param>
        /// <param name="httpContextAccessor"></param>
        public UserController(
            ILogger<UserController> logger,
            ILxUsersService ILxUsersService,
            IServiceProvider service,
            ILxResourceAccountService ILxResourceAccountService,
            ILxResourceService ILxResourceService,
            ILxShareService ILxShareService,
            IMapper IMapper, IHttpContextAccessor httpContextAccessor)
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
        /// 通过分享表id获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<ShareUsersDTO>> GetUserFormShare(int ShareID)
        {
            //先获取分享表信息
            var share = await _ILxShareService.FindAsyncById(ShareID);

            if (share == null)
            {
                return new InfoResult<ShareUsersDTO>(new ShareUsersDTO()) { Msg = "您输入的分享表ID有误" };
            }

            //再通过分享表信息获取用户信息并转化成需要的类型
            var shareuser = _IMapper.Map<ShareUsersDTO>(await _ILxUsersService.FindAsyncById(share.LxUserId));

            return new InfoResult<ShareUsersDTO>(shareuser);
        }

        /// <summary>
        /// 通过分享表id获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task EditUser([FromForm]EditUserDTO editUser)
        {
            try
            {
                //有更改图片
                var file = HttpContext.Request.Form.Files[0];
                //提取上传的文件文件后缀
                var suffix = Path.GetExtension(file.FileName);
                var Paths = $@"{Directory.GetCurrentDirectory()}\AllFile\LoginPic\{file.FileName.Substring(0, file.FileName.IndexOf("."))}{suffix}";
                using (FileStream fs = System.IO.File.Create(Paths))//注意路径里面最好不要有中文
                {
                    file.CopyTo(fs);//将上传的文件文件流，复制到fs中
                    fs.Flush();//清空文件流
                }
                var newUser = await _ILxUsersService.FindAsyncById(UserId);
                newUser.LxUsersLoginEmail = editUser.Email;
                newUser.LxUsersName = editUser.Nick;
                newUser.LxUsersPhone = editUser.Phone;
                newUser.LxUsersPic = $"{file.FileName.Substring(0, file.FileName.IndexOf("."))}{suffix}";
                await _ILxUsersService.Edit(newUser);
            }
            catch
            {
                //未更改图片
                var newUser = await _ILxUsersService.FindAsyncById(UserId);
                newUser.LxUsersLoginEmail = editUser.Email;
                newUser.LxUsersName = editUser.Nick;
                newUser.LxUsersPhone = editUser.Phone;
                await _ILxUsersService.Edit(newUser);
            }
        }

        /// <summary>
        /// 获取用户头像
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserPic()
        {
            var filepath = $@"{Directory.GetCurrentDirectory()}\AllFile\LoginPic\{(await _ILxUsersService.FindAsyncById(UserId)).LxUsersPic}";
            var provider = new FileExtensionContentTypeProvider();
            FileInfo fileInfo = new FileInfo(filepath);
            var ext = fileInfo.Extension;
            new FileExtensionContentTypeProvider().Mappings.TryGetValue(ext, out var contenttype);
            return File(System.IO.File.ReadAllBytes(filepath), contenttype ?? "application/octet-stream", fileInfo.Name + ext);
        }

        /// <summary>
        /// 通过id获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<EditUserDTO>> GetUserInfoByID()
        {
            return new InfoResult<EditUserDTO>(_IMapper.Map<EditUserDTO>(await _ILxUsersService.FindAsyncById(UserId)));
        }
    }
}