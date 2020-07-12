using LinXi_Common;
using LinXi_IRepository;
using LinXi_IService;
using LinXi_Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Service
{
    public class LxShareService : BaseService<LxShare>, ILxShareService
    {
        public LxShareService(ILxShareRepository LxShareRepository) : base(LxShareRepository)
        {
        }

        /// <summary>
        /// 生成链接ShareLink
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string CreateEncrypt(string str, string PublicKey)
        {
            AesEncrypt encrypt = new AesEncrypt();
            string ShareLink = encrypt.Encrypt(str, PublicKey).Replace("/", "1").Replace("+", "2");
            return ShareLink;
        }

        /// <summary>
        /// 创建分享
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="LxResourceAccountID">分享的资源ID</param>
        /// <returns></returns>
        public LxShare CreateShare(int UserID, int LxResourceAccountID, string LxShareTime)
        {
            string PublicKey = Guid.NewGuid().ToString("N");
            var ValidCode = new Random().Next(1000, 9999).ToString();
            string NoRpShareLink = CreateEncrypt(UserID + LxResourceAccountID + DateTime.Now.ToString(), PublicKey);
            LxShare share = new LxShare()
            {
                LxUserId = UserID,
                LxResourceAccountId = LxResourceAccountID,
                LxShareLink = NoRpShareLink,
                LxCodeKey = PublicKey,
                LxCreateTime = DateTime.Now,
                LxShareTime = LxShareTime,
                LxValidCode = ValidCode
            };
            Add(share);
            return share;
        }
    }
}