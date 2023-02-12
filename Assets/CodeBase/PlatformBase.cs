using BlackBall.Core;
using BlackBall.Factories.Core;

namespace BlackBall
{
    public abstract class PlatformBase : CoreBehaviour, IPoolItem<DefaultGOCreationOptions>
    {
        public abstract string GetItemTypeKey { get; }
        public void SetupCreationOptions(DefaultGOCreationOptions creationOptions)
        {
            transform.parent = creationOptions.Parent;
            transform.position = creationOptions.SpawnPoint;
            transform.rotation = creationOptions.Rotation;
        }
    }
}