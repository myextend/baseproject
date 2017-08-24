using System;
using Application.Core.Persistence.Interfaces;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
