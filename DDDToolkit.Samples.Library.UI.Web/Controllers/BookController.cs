using DDDToolkit.API;
using DDDToolkit.Samples.Library.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDToolkit.Samples.Library.UI.Web.Controllers
{
    [Route("api/Book")]
    [Produces("application/json")]
    public class BookController : AggregateController<Book, int>
    {
        public BookController(ApplicationLayer.IApplicationService<Book, int> applicationService) : base(applicationService)
        {
        }
    }
}
