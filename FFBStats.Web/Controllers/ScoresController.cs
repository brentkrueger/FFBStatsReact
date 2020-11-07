using FFBStats.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YahooFantasyWrapper.Client;
using YahooFantasyWrapper.Models;

namespace FFBStats.Web.Controllers
{
    public class ScoresController : Controller
    {
        private readonly IYahooFantasyClient _fantasyClient;

        public ScoresController(IYahooFantasyClient fantasyClient)
        {
            _fantasyClient = fantasyClient;
        }

        public IYahooFantasyClient FantasyClient { get; }

        public async Task<List<League>> GetLeagues([FromBody] PostModel model)
        {
            var user = await this._fantasyClient.UserResourceManager.GetUserGameLeagues(model.AccessToken, new string[] { model.Key }, EndpointSubResourcesCollection.BuildResourceList(EndpointSubResources.Teams));
            var Games = user.GameList.Games
                    .Where(a => a.GameKey == a.GameKey)
                    .Select(a => a.LeagueList.Leagues).FirstOrDefault();
            return Games;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
