using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.DTO
{
    public class PictureDTO
    {
        /// <summary>
        /// 图片ID
        /// </summary>
        public int PicId { get; set; }

        /// <summary>
        /// 图片名
        /// </summary>
        public string PicName { get; set; }

        /// <summary>
        /// 图片上传/更新时间
        /// </summary>
        public DateTime PicTime { get; set; }

        /// <summary>
        /// 图片预览地址
        /// </summary>
        public string PicSrc { get; set; }
    }
}