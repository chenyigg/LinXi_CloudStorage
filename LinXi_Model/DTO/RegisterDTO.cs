using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model.DTO
{
    public class RegisterDTO
    {
        /// <summary>
        /// 账号id
        /// </summary>
        public int LxUsersId { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string LxUsersLoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string LxUsersLoginPwd { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string LxUsersLoginEmail { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidCode { get; set; }

        /// <summary>
        /// 模型状态
        /// </summary>
        public string ModelState { get; set; }

        /// <summary>
        /// 账号密码是否匹配
        /// </summary>
        public string state { get; set; }
    }
}