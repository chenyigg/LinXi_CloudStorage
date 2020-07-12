using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Common
{
    public class CustomAutofacModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            #region 依赖于配置文件配置服务

            // 实例化
            IConfigurationBuilder config = new ConfigurationBuilder();
            //指定配置文件  这里的默认配置文件的路径在根目录下，课根据实际情况调整
            config.AddJsonFile("Autofac.json");
            // Register the ConfigurationModule with Autofac.
            IConfigurationRoot configBuild = config.Build();
            //读取配置文件里配置需要注册的服务
            var module = new ConfigurationModule(configBuild);
            containerBuilder.RegisterModule(module);

            #endregion 依赖于配置文件配置服务
        }
    }
}