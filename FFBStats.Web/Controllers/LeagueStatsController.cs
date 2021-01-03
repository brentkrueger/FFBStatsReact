using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YahooFantasyWrapper.Client;
using YahooFantasyWrapper.Models;

namespace FFBStats.Web.Controllers
{
    [Route("api/[controller]")]
    public class LeagueStatsController : BaseController
    {
        private readonly IYahooAuthClient _authClient;
        private readonly IYahooFantasyClient _fantasyClient;

        public LeagueStatsController(IYahooAuthClient authClient, IYahooFantasyClient fantasyClient) : base(authClient)
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
            var gameKeys = await GetUserGameKeys(YahooApiCommon.NFLGameKey);
            var games = await _fantasyClient.GameCollectionsManager.GetGames(gameKeys.ToArray(), _authClient.Auth.AccessToken);
            return games.OrderByDescending(g=>g.Season).ToList();
        }

        [HttpGet]
        [Route("GetLeagues/{gameKey}")]
        public async Task<List<League>> GetLeagues(string gameKey)
        {
            var leagues = new List<League>();
            await SetAuthAccessToken();
            var user = await _fantasyClient.UserResourceManager.GetUserGameLeagues(_authClient.Auth.AccessToken,
                new[] {gameKey},
                EndpointSubResourcesCollection.BuildResourceList(EndpointSubResources.Teams, EndpointSubResources.Scoreboard, EndpointSubResources.Standings));
            var game = user.GameList.Games.FirstOrDefault();
            if (game != null)
            {
                leagues.AddRange(game.LeagueList.Leagues);
                foreach (var league in leagues)
                {
                    await PopulateLeagueRecords(league);
                }
            }
            return leagues;
        }

        private async Task PopulateLeagueRecords(League league)
        {
            var leagueWithAllMatchups = await _fantasyClient.LeagueResourceManager.GetScoreboard(league.LeagueKey,
                _authClient.Auth.AccessToken, YahooApiCommon.GetSeasonWeekNumbers(true));

            league.Scoreboard = leagueWithAllMatchups.Scoreboard;

            var loginOwnedTeam = league.Standings.TeamList.Teams.FirstOrDefault(t => t.IsOwnedByCurrentLogin);
            if (loginOwnedTeam != null)
            {
                league.LoggedInUserStats.TeamName = loginOwnedTeam.Name;
                league.LoggedInUserStats.Wins = loginOwnedTeam.TeamStandings.OutcomeTotals.Wins;
                league.LoggedInUserStats.Losses = loginOwnedTeam.TeamStandings.OutcomeTotals.Losses;
                league.LoggedInUserStats.Ties = loginOwnedTeam.TeamStandings.OutcomeTotals.Ties;
                league.LoggedInUserStats.WinningPercentage = loginOwnedTeam.TeamStandings.OutcomeTotals.Percentage;
                league.LoggedInUserStats.Rank = loginOwnedTeam.TeamStandings.Rank;
                league.LoggedInUserStats.Logo = loginOwnedTeam.TeamLogos.TeamLogo.Url;
            }

            foreach (var matchup in league.Scoreboard.Matchups.Matchups.Where(m => m.Status.Equals(YahooApiCommon.PostGameStatus)))
            {
                foreach (var scoreboardTeam in matchup.Teams.Teams)
                {
                    if (!league.LeagueWideStats.LowScore.HasValue || (scoreboardTeam.TeamPoints.Total < league.LeagueWideStats.LowScore))
                    {
                        league.LeagueWideStats.LowScore = scoreboardTeam.TeamPoints.Total;
                        league.LeagueWideStats.LowScoreTeamName = scoreboardTeam.Name;
                        league.LeagueWideStats.LowScoreWeek = scoreboardTeam.TeamPoints.Week;
                    }

                    if (!league.LeagueWideStats.HighScore.HasValue || (scoreboardTeam.TeamPoints.Total > league.LeagueWideStats.HighScore))
                    {
                        league.LeagueWideStats.HighScore = scoreboardTeam.TeamPoints.Total;
                        league.LeagueWideStats.HighScoreTeamName = scoreboardTeam.Name;
                        league.LeagueWideStats.HighScoreWeek = scoreboardTeam.TeamPoints.Week;
                    }

                    if (scoreboardTeam.IsOwnedByCurrentLogin)
                    {
                        if (!league.LoggedInUserStats.LowScore.HasValue ||
                            (scoreboardTeam.TeamPoints.Total < league.LoggedInUserStats.LowScore))
                        {
                            league.LoggedInUserStats.LowScore = scoreboardTeam.TeamPoints.Total;
                            league.LoggedInUserStats.LowScoreWeek = scoreboardTeam.TeamPoints.Week;
                        }

                        if (!league.LoggedInUserStats.HighScore.HasValue ||
                            (scoreboardTeam.TeamPoints.Total > league.LoggedInUserStats.HighScore))
                        {
                            league.LoggedInUserStats.HighScore = scoreboardTeam.TeamPoints.Total;
                            league.LoggedInUserStats.HighScoreWeek = scoreboardTeam.TeamPoints.Week;
                        }
                    }
                }
            }
        }
    }
}
