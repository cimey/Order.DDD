using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.CommandsAndHandlers;
using Product.Application.Queries;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var query = new GetCategoryByIdQuery(id);
            var category = await _mediator.Send(query);
            return Ok(category);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCategories()
        {
            var query = new GetCategoriesQuery();
            var category = await _mediator.Send(query);
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid category data.");
            }
            var categoryId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryId }, null);
        }

        [HttpPost]
        [Route("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid category data.");
            }
            var categoryId = await _mediator.Send(command);
            return Ok();
        }

    }
}
