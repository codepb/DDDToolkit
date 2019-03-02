using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.ApplicationLayer.Transactions
{
    public interface ITransaction : IDisposable
    {
        Guid Id { get; }
        void Commit();
        void Rollback();
    }
}
