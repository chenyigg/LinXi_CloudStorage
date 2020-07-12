using Autofac;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Reflection;

namespace LinXi_Common
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //E:\G3项目\LinXi_CloudStorage\LinXi_CloudStorageApi\bin\Debug\netcoreapp3.1
            var basePath = AppContext.BaseDirectory;
            var mainDllFile = Path.Combine(basePath, "LinXi_CloudStorageApi.dll");
            var servicesDllFile = Path.Combine(basePath, "LinXi_Service.dll");
            var repositoryDllFile = Path.Combine(basePath, "LinXi_Repository.dll");

            if (!(File.Exists(servicesDllFile) && File.Exists(repositoryDllFile)))
            {
                var msg = "Repository.dll和service.dll 丢失，因为项目解耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。";

                throw new Exception(msg);
            }

            //builder.RegisterType().As().InstancePerLifetimeScope();

            //获取所有控制器类型并使用属性注入
            var controllerBaseType = typeof(ControllerBase);
            Assembly assemblysMain = Assembly.LoadFrom(mainDllFile);
            builder.RegisterAssemblyTypes(assemblysMain)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();

            // 获取 DAL.dll 程序集服务，并注册
            Assembly assemblysServicess = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysServicess)
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()//同一个Lifetime生成的对象是同一个实例
                                                 //.InstancePerDependency() //默认模式，每次调用，都会重新实例化对象；每次请求都创建一个新的对象；
                      .PropertiesAutowired();

            // 获取 Service.dll 程序集服务，并注册
            Assembly assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      //.InstancePerDependency()
                      .PropertiesAutowired();
        }
    }
}