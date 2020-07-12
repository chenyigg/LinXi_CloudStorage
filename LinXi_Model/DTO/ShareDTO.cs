using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.DTO
{
    public class ShareDTO
    {
        /// <summary>
        /// 提取链接
        /// </summary>
        public string ShareLink { get; set; }

        /// <summary>
        /// 提取码
        /// </summary>
        public string ValidCode { get; set; }

        /// <summary>
        /// 资源ID
        /// </summary>
        public int ShareLxResourceAccountID { get; set; }
    }
}