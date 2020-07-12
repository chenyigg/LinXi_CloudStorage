using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.DTO
{
    public class RecycleDTO
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
        /// 创建时间
        /// </summary>
        public DateTime LxResourceAccountTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string PastTime
        {
            get
            {
                if ((LxResourceAccountTime.AddDays(10) - DateTime.Now).Days < 1)
                {
                    return (LxResourceAccountTime.AddDays(10) - DateTime.Now).Hours + "小时";
                }
                else
                {
                    return (LxResourceAccountTime.AddDays(10) - DateTime.Now).Days + "天";
                }
            }
        }
    }
}