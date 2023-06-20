using Microsoft.EntityFrameworkCore.Storage;
using NetChill.Project.Foundation.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetChill.Project.DataAccess.Data.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
        OperationResult Commit();
        void Rollback();
        IDbContextTransaction BeginTransaction();
        Task<int> SaveAsyc();
    }
}
