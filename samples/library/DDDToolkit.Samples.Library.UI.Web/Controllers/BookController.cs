using DDDToolkit.API;
using DDDToolkit.ApplicationLayer;
using DDDToolkit.Samples.Library.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DDDToolkit.Samples.Library.UI.Web.Controllers
{
    [Route("api/Book")]
    [Produces("application/json")]
    public class BookController : AggregateController<Book, int>
    {
        public BookController(IApplicationService<Book, int> applicationService) : base(applicationService)
        {
        }
    }
}
