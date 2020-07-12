using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.DTO
{
    public class AllFileDTO
    {
        /// <summary>
        /// 文件资源类型ID
        /// </summary>
        public int LxResourceCategoryId { get; set; }

        /// <summary>
        /// 个人保存文件名
        /// </summary>
        public string lxResourceAccountName { get; set; }

        /// <summary>
        /// 个人保存文件ID
        /// </summary>
        public int lxResourceAccountID { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string lxResourceSize { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string lxResourceAccountTime { get; set; }

        /// <summary>
        /// 资源ID
        /// </summary>
        public int lxResourceID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string lxUsersID { get; set; }

        /// <summary>
        /// 文件父ID
        /// </summary>
        public int lxPid { get; set; }

        /// <summary>
        /// 文件父名称
        /// </summary>
        public string lxPName { get; set; }
    }
}