namespace BlackBall.Factories.Core
{
    public interface IPoolingFactory<TBase, in TCreationOptions>
    {
        public TBase Create(TBase prefab, TCreationOptions options);
        public void Delete(TBase unit);
    }
}