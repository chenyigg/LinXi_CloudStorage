#region ����

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Autofac;
using LinXi_Common;
using LinXi_Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using LinXi_CloudStorageApi.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;

#endregion ����

namespace LinXi_CloudStorageApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// �����ļ�����
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ���������ӵ�DI�����в���һЩ����.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region ע��ķ���

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton<IJWTTool, JWTTool>();

            #region ����Swagger

            services.AddMySwagger();

            #endregion ����Swagger

            services.AddSession();

            #region AutoMapper

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #endregion AutoMapper

            #region ���ÿ���Cors

            services.AddMyCors();

            #endregion ���ÿ���Cors

            #region ע��DbContext

            services.AddTransient<DbContext, LinXi_CloudStorageContext>();
            services.AddDbContext<LinXi_CloudStorageContext>(optionsAction =>
              {
                  optionsAction.UseSqlServer(Configuration.GetConnectionString("MyConn"));
              }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            #endregion ע��DbContext

            #region Id4��Ȩ��Ȩ

            services.AddMyIdentityServer();

            #endregion Id4��Ȩ��Ȩ

            #endregion ע��ķ���

            #region �������滻

            services.AddControllersWithViews(option =>
            {
                option.Filters.Add(typeof(MyAuthorAttribute));
                option.RespectBrowserAcceptHeader = true; // false by default
            })
                .AddNewtonsoftJson(setup =>
            {
                setup.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            #endregion �������滻
        }

        /// <summary>
        /// Autofac���ע��
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new AutofacModule());
        }

        /// <summary>
        /// ��������ܵ�
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region ��������

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //�쳣 / ������
                //app.UseExceptionHandler("/Home/Error");
                app.UseExceptionHandler(config =>
                {
                    config.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            var ex = error.Error;
                            await context.Response.WriteAsync(new
                            {
                                StatusCode = 500,
                                ErrorMessage = "����������"
                            }.ToString());
                        }
                    });
                });
                //HTTP �ϸ��䰲ȫЭ��
                app.UseHsts();
            }

            #endregion ��������

            #region ���ӵ��м��

            //HTTPS �ض���
            app.UseHttpsRedirection();

            //��̬�ļ�������
            app.UseStaticFiles();

            //����Swagger
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "swaggerTest");
                //s.DocInclusionPredicate((docName, description) => true);// ��עҪʹ�õ� XML �ĵ�
            });

            //Cookie ����ʵʩ
            app.UseCookiePolicy();

            //�Ự
            app.UseSession();

            app.UseRouting();

            //���ÿ��򣬱������UseMVC֮ǰUseRouting��UseEndpoints֮��
            app.UseCors("AllowCors");

            //������֤
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=WeatherForecast}/{id?}");

                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "/api/{controller}/{action}/{id?}");

                endpoints.MapControllers();
            });

            #endregion ���ӵ��м��
        }
    }
}