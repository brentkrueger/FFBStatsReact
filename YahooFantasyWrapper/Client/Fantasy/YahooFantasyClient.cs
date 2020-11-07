namespace YahooFantasyWrapper.Client
{
    public class YahooFantasyClient : IYahooFantasyClient
    {
        public GameResourceManager GameResourceManager
        {
            get
            {
                return new GameResourceManager();
            }
        }

        public GameCollectionsManager GameCollectionsManager
        {
            get
            {
                return new GameCollectionsManager();
            }
        }

        public UserResourceManager UserResourceManager
        {
            get
            {
                return new UserResourceManager();
            }
        }

        public LeagueResourceManager LeagueResourceManager
        {
            get
            {
                return new LeagueResourceManager();
            }
        }

        public LeaguesCollectionManager LeaguesCollectionManager
        {
            get
            {
                return new LeaguesCollectionManager();
            }
        }

        public PlayerResourceManager PlayerResourceManager
        {
            get
            {
                return new PlayerResourceManager();
            }
        }

        public PlayersCollectionManager PlayersCollectionManager
        {
            get
            {
                return new PlayersCollectionManager();
            }
        }

        public RosterResourceManager RosterResourceManager
        {
            get
            {
                return new RosterResourceManager();
            }
        }

        public TeamResourceManager TeamResourceManager
        {
            get
            {
                return new TeamResourceManager();
            }
        }

        public TeamsCollectionManager TeamsCollectionManager
        {
            get
            {
                return new TeamsCollectionManager();
            }
        }

        public TransactionResourceManager TransactionResourceManager
        {
            get
            {
                return new TransactionResourceManager();
            }
        }

        public TransactionsCollectionManager TransactionsCollectionManager
        {
            get
            {
                return new TransactionsCollectionManager();
            }
        }
    }
}