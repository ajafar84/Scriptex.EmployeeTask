using Scriptex.EmployeeTask.Common.Interfaces;
using Scriptex.EmployeeTask.Data.Contexts;
using Scriptex.EmployeeTask.Data.Repositories;
using System;

namespace Scriptex.EmployeeTask.Common.Repositories
{
    public interface IUnitOfWork : IDisposable
    {

        #region Repositories
        IEmployeeRepository Employees { get; }
        IJobRepository Jobs { get; }
        #endregion

        IRepository<TEntity> BaseRepository<TEntity>() where TEntity : class;
        EmployeeTaskContext Context { get; }

        int Complete();
        void BeginTransaction();
        void RollBackTransaction();
        void CommitTransaction();
    }
}
