using YahooFantasyWrapper.Models;

namespace FFBStats.Web.Models
{
    public class UserModel
    {
        public string AccessToken { get; set; }

        public UserInfo UserInfo { get; set; }
    }

    public class PostModel
    {
        public string AccessToken { get; set; }

        public string Key { get; set; }
    }
}
