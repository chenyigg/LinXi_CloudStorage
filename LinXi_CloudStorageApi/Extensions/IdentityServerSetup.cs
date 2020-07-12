using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LinXi_CloudStorageApi.Extensions
{
    public static class IdentityServerSetup
    {
        public static void AddMyIdentityServer(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer", option =>
                {
                    option.ApiName = "api2";
                    option.RequireHttpsMetadata = false;
                    option.Authority = "http://localhost:63834";
                    option.JwtValidationClockSkew = TimeSpan.FromSeconds(0);
                });
        }
    }
}