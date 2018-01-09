using DDDToolkit.ApplicationLayer;
using DDDToolkit.Samples.Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using DDDToolkit.ApplicationLayer.Transactions;

namespace DDDToolkit.Samples.Library.Application
{
    public class BookService : ApplicationService<Book, int>
    {
        public BookService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
