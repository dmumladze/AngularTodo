using Microsoft.AspNetCore.Mvc;
using AngularTodo.Models;
using AngularTodo.Services;

namespace AngularTodo.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TodosController : ControllerBase 
{
	private readonly ITodoService _service;

	public TodosController(ITodoService service)
	{
		_service = service;
	}

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		try
		{			
			var model = await _service.GetAll();
			return Ok(model);
		}
		catch
		{			
			return BadRequest();
		}
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] TodoModel model)
	{
		try
		{
			var newModel = await _service.Create(model);
			return Ok(newModel);
		}
		catch
		{
			return BadRequest();
		}
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id, [FromBody]TodoModel model)
	{
		try
		{
			await _service.Update(id, model);
			return Ok();
		}
		catch
		{			
			return BadRequest();
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		try
		{
			await _service.Delete(id);
			return Ok();
		}
		catch
		{
			return BadRequest();
		}
	}
}
