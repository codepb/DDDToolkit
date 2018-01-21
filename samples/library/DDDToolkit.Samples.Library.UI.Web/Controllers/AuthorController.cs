using DDDToolkit.ApplicationLayer;
using DDDToolkit.Querying;
using DDDToolkit.Samples.Library.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Task<IReadOnlyCollection<Book>> GetBooksByAuthor(string firstName, string lastName)
        {
            var query = Query<Book>.Has(b => b.Author.FirstName).EqualTo(firstName).And().Has(b => b.Author.LastName).EqualTo(lastName);
            return _applicationService.Query(query);
        }
    }
}
