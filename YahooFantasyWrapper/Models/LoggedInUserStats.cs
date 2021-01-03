using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models
{
    public class LoggedInUserStats
    {
        public string TeamName { get; set; }
        public string Logo { get; set; }
        public string Wins { get; set; }
        public string Losses { get; set; }
        public string Ties { get; set; }
        public string WinningPercentage { get; set; }
        public double? HighScore { get; set; }
        public double? LowScore { get; set; }
        public int LowScoreWeek { get; set; }
        public int HighScoreWeek { get; set; }
        public string Rank { get; set; }
    }
}
