﻿using DDDToolkit.ApplicationLayer;
using DDDToolkit.Samples.Library.Domain;
using System.Collections.Generic;
using DDDToolkit.ApplicationLayer.Transactions;
using FluentQueries;
using System.Threading.Tasks;
using DDDToolkit.ApplicationLayer.Repositories;

namespace DDDToolkit.Samples.Library.Application
{
    public class BookService : ApplicationService<Book, int>
    {
        public BookService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IReadOnlyCollection<Book>> GetBooksByAuthor(string firstName, string lastName)
        {
            var authorHasName = new AuthorHasName(firstName, lastName);
            var query = Query<Book>.Has(b => b.Author).Satisfying(authorHasName);
            return _repository.Query(query);
        }

        public Task<Book> GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Task<IReadOnlyCollection<Book>> GetAll()
        {
            return _repository.Query();
        }

        public async Task Add(Book book)
        {
            await _repository.Add(book);
            await _unitOfWork.Save();
        }
    }
}
