using Microsoft.AspNetCore.Mvc;
using AngularTodo.Models;
using AngularTodo.Services;

namespace AngularTodo.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProjectsController : ControllerBase
{
	private readonly IProjectService _service;

	public ProjectsController(IProjectService service)
	{
		_service = service;
	}

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		try
		{
			var models = await _service.GetAll();
			return Ok(models);
		}
		catch
		{
			return BadRequest();
		}
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetOne(int id, bool includeTodos = true)
	{
		try
		{
			var model = await _service.GetOne(id, includeTodos);
			return Ok(model);
		}
		catch
		{
			return BadRequest();
		}
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] ProjectModel model)
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
	public async Task<IActionResult> Update(int id, [FromBody] ProjectModel model)
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

	[HttpGet("search")]
	public async Task<IActionResult> Search(string term)
	{
		try
		{
			var model = await _service.Search(term);
			return Ok(model);
		}
		catch
		{
			return BadRequest();
		}
	}
}
