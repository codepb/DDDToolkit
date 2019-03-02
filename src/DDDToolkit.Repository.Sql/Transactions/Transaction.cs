using DDDToolkit.ApplicationLayer.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace DDDToolkit.Repository.Sql.Transactions
{
    class Transaction : ITransaction
    {
        private readonly IDbContextTransaction _dbContextTransaction;

        public Transaction(IDbContextTransaction dbContextTransaction)
        {
            _dbContextTransaction = dbContextTransaction;
        }

        public Guid Id => _dbContextTransaction.TransactionId;

        public void Commit()
        {
            _dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _dbContextTransaction.Dispose();
                }

                _disposedValue = true;
            }
        }
        
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
