using DDDToolkit.ApplicationLayer;
using DDDToolkit.Samples.Library.Domain;
using System.Collections.Generic;
using DDDToolkit.ApplicationLayer.Transactions;
using FluentQueries;
using System.Threading.Tasks;
using DDDToolkit.ApplicationLayer.Repositories;
using System;

namespace DDDToolkit.Samples.Library.Application
{
    public class BookService : ApplicationService
    {
        private readonly IRepository<Book, int> _bookRepository;

        public BookService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _bookRepository = unitOfWork.Repository<Book, int>();
        }

        public Task<IReadOnlyCollection<Book>> GetBooksByAuthor(string firstName, string lastName)
        {
            var authorHasName = new AuthorHasName(firstName, lastName);
            var query = Query<Book>.Has(b => b.Author).Satisfying(authorHasName);
            return _bookRepository.Query(query);
        }

        public Task<Book> GetById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public Task<IReadOnlyCollection<Book>> GetAll()
        {
            return _bookRepository.Query();
        }

        public async Task Add(Book book)
        {
            await _bookRepository.Add(book);
            await _unitOfWork.Save();
        }
    }
}
