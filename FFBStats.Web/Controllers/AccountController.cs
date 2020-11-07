using FFBStats.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web;
using YahooFantasyWrapper.Client;

namespace FFBStats.Web.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IYahooAuthClient _authClient;
        private readonly IYahooFantasyClient _fantasyClient;

        public AccountController(IYahooAuthClient authClient, IYahooFantasyClient fantasyClient)
        {
            _authClient = authClient;
            _fantasyClient = fantasyClient;
        }

        private NameValueCollection Parameters
        {
            get
            {
                return HttpUtility.ParseQueryString(Request.QueryString.Value);
            }
        }

        [HttpGet("[action]")]
        public string Login()
        {
            return _authClient.GetLoginLinkUri();
        }

        [HttpGet("[action]")]
        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

        [HttpGet("[action]")]
        public async Task<UserModel> GetAuth(string code)
        {
            var authModel = new UserModel();
            if (Parameters != null & Parameters.Count > 0 || _authClient.UserInfo != null)
            {

                if (_authClient.UserInfo == null)
                {
                    _authClient.UserInfo = await _authClient.GetUserInfo(Parameters);

                    authModel.AccessToken = _authClient.Auth.AccessToken;
                    authModel.UserInfo = _authClient.UserInfo;
                }
                else
                {
                    authModel.AccessToken = _authClient.Auth.AccessToken;
                    authModel.UserInfo = _authClient.UserInfo;
                }
            }
            return authModel;
        }
    }
}
