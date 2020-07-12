using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.DTO
{
    public class ShareFileDTO
    {
        /// <summary>
        /// 文件资源类型ID
        /// </summary>
        public int LxResourceCategoryId { get; set; }

        /// <summary>
        /// 个人保存文件名
        /// </summary>
        public string LxResourceAccountName { get; set; }

        /// <summary>
        /// 个人保存文件ID
        /// </summary>
        public int LxResourceAccountID { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string LxResourceSize { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string LxUsersID { get; set; }

        /// <summary>
        /// 分享链接
        /// </summary>
        public string LxShareLink { get; set; }

        /// <summary>
        /// 分享ID
        /// </summary>
        public string LxShareID { get; set; }

        /// <summary>
        /// 提取码
        /// </summary>
        public string ValidCode { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string PastTime
        {
            get
            {
                if (LxShareTime != "永久有效")
                {
                    var day = Convert.ToInt32(LxShareTime.Split('天')[0]);
                    return LxCreateTime.AddDays(day) < DateTime.Now ? "分享已失效" : (LxCreateTime.AddDays(day) - DateTime.Now).Days + "天";
                }
                else
                {
                    return "永久有效";
                }
            }
        }

        /// <summary>
        /// 分享有效期
        /// </summary>
        public string LxShareTime { get; set; }

        /// <summary>
        /// 分享创建期
        /// </summary>
        public DateTime LxCreateTime { get; set; }
    }
}