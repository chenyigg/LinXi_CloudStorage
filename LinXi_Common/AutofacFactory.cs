using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Common
{
    public class AutofacFactory
    {
        private AutofacFactory()
        {
        }

        private static IConfigurationBuilder ConfigurationBuilder = null;

        public static IConfiguration CreateMyContainerBuilder()
        {
            if (ConfigurationBuilder == null)
            {
                ConfigurationBuilder = new ConfigurationBuilder();
                ConfigurationBuilder.AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "Autofac.json");
            }
            return ConfigurationBuilder.Build();
        }
    }
}