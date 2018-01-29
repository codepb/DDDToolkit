using System.Threading.Tasks;
using DDDToolkit.API;
using DDDToolkit.ApplicationLayer;
using DDDToolkit.Samples.Library.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DDDToolkit.Samples.Library.UI.Web.Controllers
{
    [Route("api/Book")]
    [Produces("application/json")]
    public class BookController : AggregateController<Book, int>
    {
        public BookController(IApplicationService<Book, int> applicationService) : base(applicationService)
        {
        }

        [HttpPost]
        public async override Task<IActionResult> Create([FromBody] Book aggregate)
        {
            var validator = new BookValidator();
            var brokenRules = validator.Validate(aggregate);
            if(brokenRules.Any())
            {
                return BadRequest();
            }
            return await base.Create(aggregate);
        }
    }
}
