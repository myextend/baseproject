using System;

namespace Application.Core.Persistence.Interfaces
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
