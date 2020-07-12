using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LinXi_CloudStorageApi.Controllers
{
    /// <summary>
    /// 全部文件
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AllFileController : ControllerBase
    {
        #region 字段

        private ILogger<AllFileController> _logger;
        private ILxUsersService _ILxUsersService;
        private ILxResourceAccountService _ILxResourceAccountService;
        private ILxResourceService _ILxResourceService;
        private IServiceProvider _service;
        private IMapper _IMapper;
        private IHttpContextAccessor _httpContext;

        private int UserId
        {
            get
            {
                return int.Parse(_httpContext.HttpContext.User.Claims.Where(u => u.Type == "UserId").FirstOrDefault().Value);
            }
        }

        private readonly static object obj = new object();

        #endregion 字段

        #region 构造函数注入

        public AllFileController(
            ILogger<AllFileController> logger,
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
        /// 获取文件自身
        /// </summary>
        /// <param name="LxResourceAccountId">资源账号ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<AllFileDTO>> FileOwn(int LxResourceAccountId)
        {
            //查询该用户所有的文件夹
            var FileList = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxResourceAccountId == LxResourceAccountId && u.LxRecycle == false)).FirstOrDefault();

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
        /// 获取文件子级
        /// </summary>
        /// <param name="Pid">文件父ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<IEnumerable<AllFileDTO>>> MyAllFile(int Pid)
        {
            //查询该用户所有的文件夹
            var FileList = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxPid == Pid && u.LxRecycle == false)).ToList();

            //通过AutoMapper进行转化
            var allFileDTO = _IMapper.Map<List<AllFileDTO>>(FileList);

            //准备容器装每个文件夹的文件大小和文件类型ID
            var Size = new List<string>();
            var CategoryID = new List<int>();

            //填充容器
            await Task.Run(() =>
              FileList.ForEach(r =>
               {
                   Size.Add(_ILxResourceService.FindAsyncById(r.LxResourceId).Result.LxResourceSize);
                   CategoryID.Add(_ILxResourceService.FindAsyncById(r.LxResourceId).Result.LxResourceCategoryId);
               })
             );

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

        /// <summary>
        /// 获取文件父级
        /// </summary>
        /// <param name="Pid">文件父ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<string>> FileSuper(int Pid)
        {
            if (Pid == 0)
            {
                return new InfoResult<string>("当前页已经是首页");
            }

            //查询该用户所有的文件夹
            var FileList = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxResourceAccountId == Pid && u.LxRecycle == false)).FirstOrDefault();

            return new InfoResult<string>(FileList.LxPid.ToString());
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="LxResourceAccountId">资源账户ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> FileDown(int LxResourceAccountId)
        {
            var ac = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxResourceAccountId == LxResourceAccountId && u.LxRecycle == false)).FirstOrDefault();
            if (ac == null)
            {
                return NotFound();
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
        /// 模糊查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<InfoResult<IEnumerable<AllFileDTO>>> FuzzyQuery(string LxResourceAccountName)
        {
            //查询该用户所有的文件夹
            var FileList = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && EF.Functions.Like(u.LxResourceAccountName, $"%{LxResourceAccountName}%") && u.LxRecycle == false)).ToList();

            //通过AutoMapper进行转化
            var allFileDTO = _IMapper.Map<List<AllFileDTO>>(FileList);

            //准备容器装每个文件夹的文件大小和文件类型ID
            var Size = new List<string>();
            var CategoryID = new List<int>();
            var PName = new List<string>();
            allFileDTO.ForEach(async a =>
            {
                if (a.lxPid == 0)
                {
                    PName.Add("全部文件");
                }
                else
                {
                    PName.Add((await _ILxResourceAccountService.FindAsyncById(a.lxPid)).LxResourceAccountName);
                }
            });
            //填充容器
            await Task.Run(() =>
              FileList.ForEach(r =>
                    {
                        Size.Add(_ILxResourceService.FindAsyncById(r.LxResourceId).Result.LxResourceSize);
                        CategoryID.Add(_ILxResourceService.FindAsyncById(r.LxResourceId).Result.LxResourceCategoryId);
                    }));

            //赋值并返回数据
            int index = 0;
            allFileDTO.ForEach((u) =>
            {
                u.lxResourceSize = Size[index];
                u.LxResourceCategoryId = CategoryID[index];
                u.lxPName = PName[index];
                index++;
            });
            return allFileDTO.Count == 0 ? new InfoResult<IEnumerable<AllFileDTO>>(allFileDTO) { Code = 204 } :
            new InfoResult<IEnumerable<AllFileDTO>>(allFileDTO);
        }

        /// <summary>
        ///  如果该文件MD5码已存在则无需上传
        ///  Yes表示已存在，No表示不存在</summary>
        /// <param name="md5">文件md5码</param>
        /// <param name="pid">文件父目录</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<InfoResult<string>> ExistMd5([FromForm]string md5, [FromForm]int pid, [FromForm]string fileName)
        {
            //如果该文件已存在，则将该文件权限分配给该用户文件的所在父目录

            lock (obj)
            {
                var resourceList = _ILxResourceService.Search(u => u.LxResourceMdfive == md5).Result.FirstOrDefault();
                if (resourceList != null)
                {
                    _ILxResourceAccountService.Add(new LxResourceAccount()
                    {
                        LxGuid = Guid.NewGuid().ToString(),
                        LxPid = pid,
                        LxRecycle = false,
                        LxResourceAccountName = fileName,
                        LxUsersId = UserId,
                        LxResourceAccountTime = DateTime.Now,
                        LxResourceId = resourceList.LxResourceId,
                    });
                    return new InfoResult<string>("yes");
                }
                else
                {
                    return new InfoResult<string>("no");
                }
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="pid">父级id</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<InfoResult<string>> UpLoad([FromForm]int pid, [FromForm]string fileName)
        {
            lock (obj)
            {
                var file = HttpContext.Request.Form.Files[0];

                var md5 = StaticHelper.GetMD5HashFromStream(file.OpenReadStream());

                if (_ILxResourceService.Search(u => u.LxResourceMdfive == md5).Result.Count() > 0)
                {
                    return new InfoResult<string>("该文件已存在");
                }

                #region 网上

                //提取上传的文件文件后缀
                var suffix = Path.GetExtension(file.FileName);

                //文件保存路径
                string path = "";

                //文件类型id
                int CategoryId = 0;

                //通过文件后缀名指定当前文件保存路径
                switch (suffix.ToLower())
                {
                    //文档
                    case ".txt":
                    case ".doc":
                    case ".hlp":
                    case ".wps":
                    case ".rtf":
                    case ".html":
                    case "pdf":
                    case "xls":
                    case ".dbf":
                        path = "TxT";
                        CategoryId = 5;
                        break;
                    //图片
                    case ".bmp":
                    case ".gif":
                    case ".jpg":
                    case ".jpeg":
                    case ".pic":
                    case ".png":
                    case ".tif":
                    case ".img":
                    case ".psd":
                        path = "Pic";
                        CategoryId = 2;
                        break;
                    //压缩包
                    case ".rar":
                    case ".zip":
                    case ".arj":
                    case ".gz":
                    case ".z":
                        path = "Rar";
                        CategoryId = 6;
                        break;
                    //音乐
                    case ".wav":
                    case ".aif":
                    case ".au":
                    case ".mp3":
                    case ".ram":
                    case ".wma":
                    case ".mmf":
                    case ".amr":
                    case ".aac":
                    case ".flac":
                        path = "Music";
                        CategoryId = 4;
                        break;
                    //视频
                    case ".avi":
                    case ".mov":
                    case ".asf":
                    case ".rm":
                    case ".navi":
                    case ".mpeg":
                    case ".mpg":
                    case ".mp4":
                        path = "Video";
                        CategoryId = 3;
                        break;
                    //种子
                    case ".torrent":
                        path = "BT";
                        CategoryId = 7;
                        break;

                    default:
                        path = "Other";
                        CategoryId = 8;
                        break;
                }
                var Paths = $@"{Directory.GetCurrentDirectory()}\AllFile\{path}\{file.FileName.Substring(0, file.FileName.IndexOf("."))}{suffix}";
                using (FileStream fs = System.IO.File.Create(Paths))//注意路径里面最好不要有中文
                {
                    file.CopyTo(fs);//将上传的文件文件流，复制到fs中
                    fs.Flush();//清空文件流
                }

                //将文件添加至LxResource资源表中
                LxResource lxResource = new LxResource()
                {
                    LxResourceCategoryId = CategoryId,
                    LxResourceMdfive = md5,
                    LxResourceName = file.FileName,
                    LxResourceSize = (file.Length / 1048576.0).ToString("f2") + "mb",
                    LxResourcePath = $@"{path}\{file.FileName.Substring(0, file.FileName.IndexOf("."))}{suffix}"
                };
                int k = _ILxResourceService.Add(lxResource).Result;
                //将文件权限分配给该用户
                _ILxResourceAccountService.Add(new LxResourceAccount()
                {
                    LxGuid = Guid.NewGuid().ToString(),
                    LxPid = pid,
                    LxRecycle = false,
                    LxResourceAccountName = fileName.Substring(0, fileName.LastIndexOf(".")),
                    LxUsersId = UserId,
                    LxResourceAccountTime = DateTime.Now,
                    LxResourceId = lxResource.LxResourceId,
                });

                return new InfoResult<string>("上传成功");

                #endregion 网上
            }
        }

        /// <summary>
        /// 新建文件夹
        /// </summary>
        /// <param name="pid">父级id</param>
        /// <returns></returns>
        [HttpPost]
        public InfoResult<string> CreateFile([FromForm]int pid)
        {
            lock (obj)
            {
                //将文件权限分配给该用户
                _ILxResourceAccountService.Add(new LxResourceAccount()
                {
                    LxGuid = Guid.NewGuid().ToString(),
                    LxPid = pid,
                    LxRecycle = false,
                    LxResourceAccountName = "新建文件夹",
                    LxUsersId = UserId,
                    LxResourceAccountTime = DateTime.Now,
                    LxResourceId = 6,
                });

                return new InfoResult<string>("新建成功");
            }
        }

        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="LxResourceAccountName">要修改的名字</param>
        /// <param name="LxResourceAccountID">资源账户ID</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<InfoResult<string>> OverrideName([FromForm]string LxResourceAccountName, [FromForm]int LxResourceAccountID)
        {
            if (String.IsNullOrEmpty(LxResourceAccountName))
            {
                return new InfoResult<string>("修改后名字不可为空！");
            }
            else
            {
                var ac = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxResourceAccountId == LxResourceAccountID && u.LxRecycle == false)).FirstOrDefault();
                ac.LxResourceAccountName = LxResourceAccountName;
                ac.LxResourceAccountTime = DateTime.Now;
                return await _ILxResourceAccountService.Edit(ac) > 0 ?
                    new InfoResult<string>("修改成功") : new InfoResult<string>("修改失败");
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteFiles(int LxResourceAccountID)
        {
            return RedirectToAction("DeleteFile", new { Userid = this.UserId, LxResourceAccountID = LxResourceAccountID });
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<InfoResult<string>> DeleteFile(int UserId, int LxResourceAccountID)
        {
            //获取该文件与账号所关联的信息
            var ac = (await _ILxResourceAccountService.Search(u => u.LxResourceAccountId == LxResourceAccountID && u.LxUsersId == UserId)).FirstOrDefault();

            //获取该文件资源类型
            var lr = await _ILxResourceService.FindAsyncById(ac.LxResourceId);

            //如果是文件夹则要获取该文件夹下面的所有文件
            if (lr.LxResourceCategoryId == 1)
            {
                //获取该文件下面的所有文件
                List<LxResourceAccount> FileList = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxPid == LxResourceAccountID)).ToList();
                if (FileList.Count == 0)
                {
                    var ss = (await _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxResourceAccountId == LxResourceAccountID)).FirstOrDefault();
                    ss.LxRecycle = true;
                    ss.LxResourceAccountTime = DateTime.Now;
                    await _ILxResourceAccountService.Edit(ss);
                }

                FileList.ForEach(u =>
               {
                   var d = DeleteFile(UserId, u.LxResourceAccountId).Result;
                   var ss = _ILxResourceAccountService.Search(u => u.LxUsersId == UserId && u.LxResourceAccountId == LxResourceAccountID).Result.FirstOrDefault();
                   ss.LxRecycle = true;
                   ss.LxResourceAccountTime = DateTime.Now;
                   var dd = _ILxResourceAccountService.Edit(ss).Result;
               });

                return new InfoResult<string>("error");
            }
            else
            {
                ac.LxRecycle = true;
                ac.LxResourceAccountTime = DateTime.Now;
                await _ILxResourceAccountService.Edit(ac);
                return new InfoResult<string>("error");
            }

            #region 事务

            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
            //        _ls.ToList().ForEach(u =>
            //        {
            //            var scopeac = _ILxResourceAccountService.Search(z => z.LxResourceAccountId == u).FirstOrDefault();
            //            _ILxResourceAccountService.Search(d => d.LxResourceAccountId == scopeac.LxResourceAccountId).FirstOrDefault().LxRecycle = false;
            //        });
            //        scope.Complete();
            //        return new InfoResult<string>("删除成功");
            //    }
            //    catch (Exception ex)
            //    {
            //        scope.Dispose();
            //        return new InfoResult<string>("error");
            //    }
            //};

            #endregion 事务
        }
    }
}