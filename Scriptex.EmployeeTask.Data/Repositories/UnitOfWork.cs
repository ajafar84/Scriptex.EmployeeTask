using Scriptex.EmployeeTask.Common.Interfaces;
using Scriptex.EmployeeTask.Common.Repositories;
using Scriptex.EmployeeTask.Data.Contexts;
using Scriptex.EmployeeTask.Data.Repositories;
using System;
using System.Data;
using System.Data.Entity;

namespace Scriptex.EmployeeTask.Common.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContextTransaction _dbContextTransaction { get; set; }

        public UnitOfWork()
        {
            Context = new EmployeeTaskContext();
        }

        #region Repositories
        private EmployeeRepository employees;
        public IEmployeeRepository Employees =>
            employees ?? (employees = new EmployeeRepository(Context));

        private JobRepository jobs;
        public IJobRepository Jobs =>
            jobs ?? (jobs = new JobRepository(Context));
        #endregion

        #region Methods
        

        public IRepository<TEntity> BaseRepository<TEntity>() where TEntity : class
        {
            return new Repository<EmployeeTaskContext, TEntity>(Context);
        }

        public EmployeeTaskContext Context { get; }

        public void BeginTransaction()
        {
            _dbContextTransaction = Context.Database.BeginTransaction(IsolationLevel.Serializable);
        }

        public void RollBackTransaction()
        {
            _dbContextTransaction.Rollback();
            _dbContextTransaction.Dispose();
        }

        public void CommitTransaction()
        {
            _dbContextTransaction.Commit();
            _dbContextTransaction.Dispose();
        }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            //Context.Dispose();
            //GC.SuppressFinalize(this);
        }
        #endregion
    }
}
