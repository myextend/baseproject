namespace Persistence.Interfaces
{
    public interface IDatabaseContext
    {
        //IDbSet<T> Set<T>() where T : class, IEntity;

        void SaveChanges();
    }
}
