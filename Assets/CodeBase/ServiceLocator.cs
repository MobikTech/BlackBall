namespace BlackBall
{
    public class ServiceLocator
    {
        public static readonly ServiceLocator ServiceLocatorInstance = new ServiceLocator(new GameScore());

        public ServiceLocator(GameScore gameScore)
        {
            GameScore = gameScore;
        }

        public GameScore GameScore { get; }
    }
}