using Microsoft.AspNetCore.Mvc;
using POC_RavenDb_Docker.DTOs.Requests;
using POC_RavenDb_Docker.Services.Interfaces;

namespace POC_RavenDb_Docker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Find([FromQuery] int skip, [FromServices] IBookService bookService)
        {
            var response = await bookService.Load(skip);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id, [FromServices] IBookService bookService)
        {
            var response = await bookService.Load(id);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookRequest bookRequest, [FromServices] IBookService bookService)
        {
            await bookService.Store(bookRequest);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id, [FromServices] IBookService bookService)
        {
            await bookService.Delete(id);

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] BookRequest bookRequest, [FromServices] IBookService bookService)
        {
            await bookService.Update(id, bookRequest);

            return Ok();
        }
    }
}
