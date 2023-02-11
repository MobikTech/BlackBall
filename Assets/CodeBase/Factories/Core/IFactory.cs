namespace BlackBall.Factories.Core
{
    public interface IFactory<TBase, in TOptions>
    {
        public TBase Create(TOptions options);
    }
}