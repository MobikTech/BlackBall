using BlackBall.Core;

namespace BlackBall.Factories.Core
{
    public interface IPoolItem<in TCreationOptions> where TCreationOptions : IOptions
    {
        public string GetItemTypeKey { get; }
        public void SetupCreationOptions(TCreationOptions creationOptions);
    }
}