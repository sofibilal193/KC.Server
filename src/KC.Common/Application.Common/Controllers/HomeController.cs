#pragma warning disable CA1822

using KC.Application.Common.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace KC.Application.Common.Controllers
{
    [OpenApiIgnore]
    [AllowAnonymous]
    [Route("Home")]
    public class HomeController : BaseController
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
