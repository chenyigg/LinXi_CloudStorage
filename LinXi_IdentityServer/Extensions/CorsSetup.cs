using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinXi_CloudStorageApi.Extensions
{
    public static class CorsSetup
    {
        public static void AddMyCors(this IServiceCollection services)
        {
            services.AddCors(o =>
            {
                o.AddPolicy("AllowCors", policy =>
                {
                    policy.WithOrigins("http://localhost:3001", "http://localhost:3001",
                        "http://test.chenyigg.com:3000", "http://www.chenyigg.com:3000",
                      "http://localhost:63833").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    policy.WithExposedHeaders("Content-Disposition");
                });
            });
        }
    }
}