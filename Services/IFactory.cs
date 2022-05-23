namespace Library.Services
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
