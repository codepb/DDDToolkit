using DDDToolkit.ApplicationLayer;
using DDDToolkit.Samples.Library.Domain;
using FluentQueries;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDToolkit.Samples.Library.UI.Web.Controllers
{
    [Route("api/Author")]
    [Produces("application/json")]
    public class AuthorController : Controller
    {
        private readonly IApplicationService<Book, int> _applicationService;

        public AuthorController(IApplicationService<Book, int> applicationService)
        {
            _applicationService = applicationService;
        }

        [Route("{firstName} {lastName}/Books")]
        public async Task<IReadOnlyCollection<Book>> GetBooksByAuthor(string firstName, string lastName)
        {
            var authorHasName = new AuthorHasName(firstName, lastName);
            var query = Query<Book>.Has(b => b.Author).Satisfying(authorHasName);
            var result = await _applicationService.GetWhere(query);
            return result;
        }
    }
}
