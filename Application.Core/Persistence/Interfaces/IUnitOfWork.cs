namespace Application.Core.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        ITransaction BeginTransaction();
       // ITransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}
