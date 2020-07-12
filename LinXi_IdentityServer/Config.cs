using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinXi_IdentityServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        /// <summary>
        /// 允许访问哪些Api（就像我允许我家里的哪些东西可以让顾客访问使用，如桌子，椅子等等）   CreateDate：2019-12-26 14:08:29；Author：Ling_bug
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("api1", "LinXi Api1"){Scopes = { "LinXi_scope"}},
                   new ApiResource("api2", "LinXi Api2"){Scopes = { "LinXi_scope2"}}
            };
        }

        internal static IEnumerable<ApiScope> GetApiScopes()
        {
            return new ApiScope[]
            {
                  new ApiScope("LinXi_scope"),
                              new ApiScope("LinXi_scope2")
            };
        }

        /// <summary>
        /// 允许哪些客户端访问（就像我要求顾客必须具备哪些条件才可以拿到进入我家的钥匙）   CreateDate：2019-12-26 14:09:51；Author：Ling_bug
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "LinXi_Client Clound",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(Encrypt("ChenyiggSecret")),
                    },
                    AllowedScopes = { "LinXi_scope" },
                    //token过期 3600秒，秒为单位
                    AccessTokenLifetime=900,

                 #region 注解

		   //// 指定登录成功跳转回来的 uri
                    //RedirectUris=
                    //{
                    //      "http://localhost:3001/404"
                    //},
                    //LogoUri="http://localhost:3001/404",
                    //// 登出 以后跳转的页面
                    //PostLogoutRedirectUris=
                    //{
                    //      "http://localhost:3001/404"
                    //},
                    //  // vue 和 IdentityServer 不在一个域上，需要指定跨域
                    //  AllowedCorsOrigins = { "http://localhost:3001"},

	#endregion 注解
                },
                  new Client
               {
                   ClientId = "client2",
                   AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                   ClientSecrets =
                   {
                       new Secret("secret".Sha256())
                   },
                   AllowedScopes = { "LinXi_scope2",IdentityServerConstants.StandardScopes.OpenId, //必须要添加，否则报forbidden错误
                 IdentityServerConstants.StandardScopes.Profile },
                    //token过期 3600秒，秒为单位
                    AccessTokenLifetime=900,
                    AbsoluteRefreshTokenLifetime=300,
               }
            };
        }

        /// <summary>
        /// 加密   CreateDate：2019-12-26 11:19:04；Author：Ling_bug
        /// </summary>
        /// <param name="valueString"></param>
        /// <returns></returns>
        private static string Encrypt(string valueString)
        {
            return string.IsNullOrWhiteSpace(valueString) ? null : valueString.Sha256();
        }
    }
}