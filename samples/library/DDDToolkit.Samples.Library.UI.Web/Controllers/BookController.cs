using System.Threading.Tasks;
using DDDToolkit.Samples.Library.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DDDToolkit.Samples.Library.Application;

namespace DDDToolkit.Samples.Library.UI.Web.Controllers
{
    [Route("api/Book")]
    [Produces("application/json")]
    public class BookController : Controller
    {
        private readonly BookService _applicationService;

        public BookController(BookService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _applicationService.GetById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _applicationService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            var validator = new BookValidator();
            var brokenRules = validator.GetBrokenRules(book);
            if(brokenRules.Any())
            {
                return BadRequest(brokenRules.Select(b => b.Rule));
            }
            await _applicationService.Add(book);
            return CreatedAtRoute(nameof(GetById), book.Id);
        }
    }
}
