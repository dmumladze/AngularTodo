using Microsoft.EntityFrameworkCore;
using TodoPlanner.Data.Context;
using TodoPlanner.Data.Entities;

namespace TodoPlanner.Data.Repositiory
{
	public interface IApiRepository : IDisposable
	{
		Task<ProjectEntity> GetProject(int projectId, bool includeTodos = false);
		Task<IList<ProjectEntity>> GetAllProjects();
		Task<TodoEntity> GetTodo(int id);
		Task<TodoEntity> Create(TodoEntity todo);
		Task<ProjectEntity> Create(ProjectEntity project);
		Task DeleteTodo(int id);
		Task DeleteProject(int id); 
		Task<IList<TodoEntity>> GetAllTodos();
		Task Update(TodoEntity todo);
		Task Update(ProjectEntity project);
	}

	public class ApiRepository : IApiRepository, IDisposable
	{
		private readonly ApiContext _context;
		private bool _disposed;

		public ApiRepository(ApiContext context)
		{
			_context = context;
		}

		public async Task<ProjectEntity> GetProject(int id, bool includeTodos = false)
		{
			if (!includeTodos)
			{
				return await _context.Projects					
					.SingleAsync(p => p.ProjectEntityId == id);
			}
			return await _context.Projects
				.Include(p => p.Todos)
				.SingleAsync(p => p.ProjectEntityId == id);
		}

		public async Task<IList<ProjectEntity>> GetAllProjects()
		{
			return await _context.Projects.ToListAsync();
		}

		public async Task<TodoEntity> GetTodo(int id)
		{
			return await _context.Todos.SingleAsync(p => p.TodoEntityId == id);
		}

		public async Task<TodoEntity> Create(TodoEntity todo)
		{
			await _context.Todos.AddAsync(todo);
			await _context.SaveChangesAsync();
			return todo;
		}

		public async Task<ProjectEntity> Create(ProjectEntity project)
		{
			await _context.Projects.AddAsync(project);
			await _context.SaveChangesAsync();
			return project;
		}

		public async Task DeleteTodo(int id)
		{
			var todo = await GetTodo(id);
			_context.Todos.Remove(todo);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteProject(int id)
		{
			var project = await GetProject(id);
			_context.Projects.Remove(project);
			await _context.SaveChangesAsync();
		}

		public async Task<IList<TodoEntity>> GetAllTodos()
		{
			return await _context.Todos
				.Where(p => p.ProjectEntityId == null).ToListAsync();
		}

		public async Task Update(TodoEntity todo)
		{
			_context.Entry(todo).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task Update(ProjectEntity project)
		{
			_context.Entry(project).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			_disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
