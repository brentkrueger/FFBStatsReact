using System.Collections.Generic;

namespace FFBStats.Web
{
    public class YahooApiCommon
    {
        public const string PostGameStatus = "postevent"; //indicates that a game is complete
        public const string NFLGameKey = "nfl";
        public static int?[] GetSeasonWeekNumbers(bool includePlayoffs=false)
        {
            var weeks = new List<int?>()
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17};

            if (includePlayoffs)
            {
                weeks.AddRange(new List<int?>{ 18, 19, 20, 21, 22, 23, 24, 25 });
            }

            return weeks.ToArray();
        }
    }
}