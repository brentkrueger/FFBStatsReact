using System;
using FFBStats.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YahooFantasyWrapper.Client;
using YahooFantasyWrapper.Models;

namespace FFBStats.Web.Controllers
{
    [Route("api/[controller]")]
    public class InteractiveController : StatsController
    {
        private readonly IYahooAuthClient _authClient;
        private readonly IYahooFantasyClient _fantasyClient;

        public InteractiveController(IYahooAuthClient authClient, IYahooFantasyClient fantasyClient) : base(authClient)
        {
            this._authClient = authClient;
            this._fantasyClient = fantasyClient;
        }

        private async Task<List<string>> GetUserGameKeys(string gameCode)
        {
            await SetAuthAccessToken();
            var user = await _fantasyClient.UserResourceManager.GetUser(_authClient.Auth.AccessToken);
            return user.GameList.Games.Where(g => g.Code.Equals(gameCode)).Select(g=>g.GameKey).ToList();
        }

        [HttpGet]
        [Route("GetGames")]
        public async Task<List<Game>> GetGames()
        {
            await SetAuthAccessToken();
            var gameKeys = await GetUserGameKeys("nfl");
            var games = await _fantasyClient.GameCollectionsManager.GetGames(gameKeys.ToArray(), _authClient.Auth.AccessToken);
            return games.OrderByDescending(g=>g.Season).ToList();
        }

        [HttpGet]
        [Route("GetLeagues/{gameKey}")]
        public async Task<List<League>> GetLeagues(string gameKey)
        {
            await SetAuthAccessToken();
            var user = await _fantasyClient.UserResourceManager.GetUserGameLeagues(_authClient.Auth.AccessToken,
                new[] {gameKey},EndpointSubResourcesCollection.BuildResourceList(EndpointSubResources.Teams, EndpointSubResources.Scoreboard));
            var game = user.GameList.Games.FirstOrDefault();
            var leagues = game.LeagueList.Leagues;
            foreach (var league in leagues)
            {
                var loginOwnedTeam = league.TeamList.Teams.FirstOrDefault(t => t.IsOwnedByCurrentLogin);
                if(loginOwnedTeam != null)
                {
                    league.Logo = loginOwnedTeam.TeamLogos.TeamLogo.Url;
                }
                
                foreach (var matchup in league.Scoreboard.Matchups.Matchups)
                {
                    foreach (var scoreboardTeam in matchup.Teams.Teams)
                    {
                        if (!league.LowScore.HasValue || (scoreboardTeam.TeamPoints.Total < league.LowScore))
                        {
                            league.LowScore = scoreboardTeam.TeamPoints.Total;
                            league.LowScoreTeamName = scoreboardTeam.Name;
                        }

                        if (!league.HighScore.HasValue || (scoreboardTeam.TeamPoints.Total > league.HighScore))
                        {
                            league.HighScore = scoreboardTeam.TeamPoints.Total;
                            league.HighScoreTeamName = scoreboardTeam.Name;
                        }
                    }
                }
            }
            return leagues;
        }

        [HttpPost("[action]")]
        public async Task<League> GetLeagueMetadata([FromBody] PostModel model)
        {
            return await this._fantasyClient.LeagueResourceManager.GetMeta(model.Key, model.AccessToken);
        }

        [HttpPost("[action]")]
        public async Task<League> GetLeagueStandings([FromBody] PostModel model)
        {
            return await this._fantasyClient.LeagueResourceManager.GetStandings(model.Key, model.AccessToken);
        }

        [HttpPost("[action]")]
        public async Task<League> GetLeagueSettings([FromBody] PostModel model)
        {
            return await this._fantasyClient.LeagueResourceManager.GetSettings(model.Key, model.AccessToken);
        }

        [HttpPost("[action]")]
        public async Task<League> GetLeagueTeams([FromBody] PostModel model)
        {
            return await this._fantasyClient.LeagueResourceManager.GetTeams(model.Key, model.AccessToken);
        }

        [HttpPost("[action]")]
        public async Task<League> GetLeagueScoreboard([FromBody] PostModel model)
        {
            return await this._fantasyClient.LeagueResourceManager.GetScoreboard(model.Key, model.AccessToken);
        }

        [HttpPost("[action]")]
        public async Task<League> GetDraftResults([FromBody] PostModel model)
        {
            return await this._fantasyClient.LeagueResourceManager.GetDraftResults(model.Key, model.AccessToken);
        }
    }
}
