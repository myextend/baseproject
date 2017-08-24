using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Core
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        ITransaction BeginTransaction();
        ITransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}
