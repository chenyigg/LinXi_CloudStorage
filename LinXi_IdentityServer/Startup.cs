using Autofac;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using LinXi_CloudStorageApi.Extensions;
using LinXi_Common;
using LinXi_IdentityServer.ServerModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace LinXi_IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 配置钥匙

            //using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048))
            //{
            //    Console.WriteLine("PublicKey" + Convert.ToBase64String(provider.ExportCspBlob(false)));   //PublicKey

            //    Console.WriteLine("PrivateKey" + Convert.ToBase64String(provider.ExportCspBlob(true)));    //PrivateKey
            //}
            //var rsa = new RSACryptoServiceProvider();
            ////从配置文件获取加密证书
            //rsa.ImportCspBlob(Convert.FromBase64String(Configuration.GetValue<string>("SigningCredential")));
            //Console.WriteLine("公钥" + Convert.ToBase64String(rsa.ExportCspBlob(false)));
            //Console.WriteLine("私钥" + Convert.ToBase64String(rsa.ExportCspBlob(true)));

            #endregion 配置钥匙

            services.AddControllersWithViews();
            services.AddMyCors();
            services.AddIdentityServer()
              .AddInMemoryIdentityResources(Config.GetIdentityResources())
               //.AddDeveloperSigningCredential()
               //.AddSigningCredential(new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256))    //设置加密证书
               //设置自定义加密证书
               .AddSigningCredential(
                        new X509Certificate2(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Configuration["Certificates:Path"]),
                         Configuration["Certificates:Password"]))
              .AddInMemoryApiResources(Config.GetApiResources())

              .AddInMemoryApiScopes(Config.GetApiScopes())

              .AddInMemoryClients(Config.GetClients())
             .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>();

            //Inject the classes we just created
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();

            //配置跨域，必须放在UseMVC之前UseRouting和UseEndpoints之间
            app.UseCors("AllowCors");
            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}