using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LinXi_CloudStorageApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                 Host.CreateDefaultBuilder(args)
                 .ConfigureLogging(configureLogging =>
                 {
                     //过滤掉System和Microsoft开头的命名空间下的组件产生的警告级别一下的日志
                     //configureLogging.AddFilter("System", LogLevel.Warning);
                     //configureLogging.AddFilter("Microsoft", LogLevel.Warning);
                     configureLogging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);
                     //注册log4net
                     configureLogging.AddLog4Net(AppDomain.CurrentDomain.BaseDirectory + "Log4net.config");
                 })
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.ConfigureKestrel((context, options) =>
                     {
                         //设置应用服务器Kestrel请求体最大为150MB
                         options.Limits.MaxRequestBodySize = 152428800;
                     });
                     webBuilder.UseStartup<Startup>().UseIIS();
                 }).UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}