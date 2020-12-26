using Microsoft.AspNetCore.Mvc;

namespace FFBStats.Web.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        [HttpGet("[action]")]
        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }
    }
}
