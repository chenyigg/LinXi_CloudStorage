using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.DTO
{
    public class ShareDetailsDTO
    {
        /// <summary>
        /// 分享表ID
        /// </summary>
        public int LxShareId { get; set; }

        /// <summary>
        /// 分享资源ID
        /// </summary>
        public int LxResourceAccountId { get; set; }
    }
}