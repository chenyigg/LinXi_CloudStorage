using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LinXi_CloudStorageApi.Extensions
{
    /// <summary>
    /// Swagger配置
    /// </summary>
    public static class SwaggerSetup
    {
        /// <summary>
        /// Swagger配置
        /// </summary>
        /// <param name="services"></param>
        public static void AddMySwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "swaggerTest",
                    Version = "v1",
                    Description = "灵犀云盘",
                    Contact = new OpenApiContact()
                    {
                        Email = "chenyigg123@163.com",
                        Name = "易",
                        //Url = new Uri("http://www.baidu.com")
                    },
                });
                // 开启加权小锁
                s.OperationFilter<AddResponseHeadersFilter>();
                s.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // 在header中添加token，传递到后台
                s.OperationFilter<SecurityRequirementsOperationFilter>();

                #region 客户端

                //s.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                //{
                //    Type = SecuritySchemeType.OAuth2,
                //    Flows = new OpenApiOAuthFlows
                //    {
                //        ClientCredentials = new OpenApiOAuthFlow
                //        {
                //            TokenUrl = new Uri("http://localhost:63834/connect/token"),
                //            //RefreshUrl = new Uri("http://localhost:63834/connect/token"),
                //            ////授权地址
                //            //AuthorizationUrl = new Uri("http://localhost:63834/connect/token"),
                //            Scopes = new Dictionary<string, string>
                //             {
                //                { "LinXi_scope", "灵犀网盘 Api" }
                //             },
                //        }
                //    }
                //});

                #endregion 客户端

                #region 密码

                s.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("http://localhost:63834/connect/token"),
                            Scopes = new Dictionary<string, string>
                             {
                                { "LinXi_scope2", "灵犀网盘 Api2" }
                             },
                        }
                    }
                });

                #endregion 密码

                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                ////此处为API的项目描述文件名
                var commentsFileName = "LinXi_CloudStorageApi.xml";
                var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                s.IncludeXmlComments(commentsFile, true);
            });
        }
    }
}