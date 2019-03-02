using DDDToolkit.ApplicationLayer;
using DDDToolkit.Samples.Library.Application;
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
        private readonly BookService _applicationService;

        public AuthorController(BookService applicationService)
        {
            _applicationService = applicationService;
        }

        [Route("{firstName} {lastName}/Books")]
        public async Task<IReadOnlyCollection<Book>> GetBooksByAuthor(string firstName, string lastName)
        {
            var result = await _applicationService.GetBooksByAuthor(firstName, lastName);
            return result;
        }
    }
}
