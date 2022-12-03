namespace Core
{
    public abstract class AbstractObjectFactory<T>
    {
        public abstract T Create(params object[] args);
    }
}