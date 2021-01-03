using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models
{
    public class LeagueWideStats
    {
        public double? HighScore { get; set; }
        public string HighScoreTeamName { get; set; }
        public double? LowScore { get; set; }
        public string LowScoreTeamName { get; set; }
        public int LowScoreWeek { get; set; }
        public int HighScoreWeek { get; set; }
    }
}