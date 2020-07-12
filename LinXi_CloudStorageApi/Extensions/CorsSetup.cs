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
                    policy.WithOrigins("http://localhost:3001", "http://localhost:3001", " http://192.168.43.38:3001", "http://127.0.0.1:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    policy.WithExposedHeaders("Content-Disposition");
                });
            });
        }
    }
}