using LinXi_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace LinXi_Common
{
    public class MyAuthorAttribute : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //context.Filters.Any(it => it is IAllowAnonymousFilter)
            var cad = (ControllerActionDescriptor)context.ActionDescriptor;
            bool allowanyone = cad.ControllerTypeInfo.GetCustomAttributes(typeof(IAllowAnonymous), true).Any()
           || cad.MethodInfo.GetCustomAttributes(typeof(IAllowAnonymous), true).Any();

            //如果没有AllAnonymous特性，则进行验证
            if (!allowanyone)
            {
                string Authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

                string key = $"token_{context.HttpContext.User.Claims.Where(u => u.Type == "UserId").FirstOrDefault().Value}";

                if (RedisHelper.Get<string>(key) != Authorization.Split(" ")[1])
                {
                    context.HttpContext.Response.StatusCode = 401;
                    //context.Result= new
                }
            }
        }
    }
}