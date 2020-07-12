using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinXi_IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class identityController : ControllerBase
    {
        public IActionResult Get()
        {
            return new JsonResult(
                from c in User.Claims select new { c.Type, c.Value }
                );
        }
    }
}