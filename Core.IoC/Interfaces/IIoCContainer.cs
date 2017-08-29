namespace Core.IoC.Interfaces
{
    public interface IIoCContainer
    {
        T Resolve<T>();
    }
}
