using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YahooFantasyWrapper.Client;

namespace FFBStats.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IYahooAuthClient _authClient;

        public BaseController(IYahooAuthClient authClient)
        {
            _authClient = authClient;
        }

        private protected async Task SetAuthAccessToken()
        {
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");
            var updatedAccessToken = await _authClient.GetCurrentToken(refreshToken);
            _authClient.Auth.AccessToken = updatedAccessToken;
        }
    }
}