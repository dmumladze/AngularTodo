using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using AngularTodo.Data.Entities;
using AngularTodo.Data.Repositiory;
using AngularTodo.Hubs;
using AngularTodo.Models;

namespace AngularTodo.Services
{
	public interface ITodoService
	{
		Task<IList<TodoModel>> GetAll();
		Task<TodoModel> Create(TodoModel model);
		Task Update(int id, TodoModel model);
		Task Delete(int id);
		Task<IList<TodoModel>> Search(string term, int? projectId);
	}

	public class TodoService : ITodoService
	{
		private readonly IApiRepository _repo;
		private readonly IMapper _mapper;
		private readonly IBackgroundJobClient _jobClient;
		private readonly IHubContext<ApiHub> _hub;
		private readonly ILogger<TodoService> _logger;

		public TodoService(IApiRepository repo, IMapper mapper, IBackgroundJobClient jobClient, 
			IHubContext<ApiHub> hub, 
			ILogger<TodoService> logger)
		{
			_repo = repo;
			_mapper = mapper;
			_jobClient = jobClient;
			_hub = hub;
			_logger = logger;
		}

		public async Task<IList<TodoModel>> GetAll()
		{
			try
			{
				var todos = await _repo.GetAllTodos();
				var models = _mapper.Map<IList<TodoModel>>(todos);
				return models;
			}
			catch (Exception ex)
			{
				_logger.LogError("Failed to get to-dos.", ex);
				throw;
			}
		}

		public async Task<TodoModel> GetOne(int id)
		{
			try
			{
				var todo = await _repo.GetTodo(id);
				var model = _mapper.Map<TodoModel>(todo);
				return model;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get to-do {id}.", ex);
				throw;
			}
		}

		public async Task<TodoModel> Create(TodoModel model)
		{
			try
			{
				var todo = _mapper.Map<TodoEntity>(model);
				var newTodo  = await _repo.Create(todo);
				var newModel = _mapper.Map<TodoModel>(newTodo);
				return newModel;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to create to-do {model.Title}.", ex);
				throw;
			}
		}

		public async Task Update(int id, TodoModel model)
		{
			try
			{
				var prevTodo = await _repo.GetTodo(id);
				var currTodo = _mapper.Map<TodoEntity>(model);				

				if (!string.IsNullOrEmpty(prevTodo.JobId)) 
				{
					_jobClient.Delete(prevTodo.JobId);
				} 
				if (currTodo.RemindDate != null)
				{
					currTodo.JobId = _jobClient.Schedule<TodoService>(j => j.RemindTodo(id, model.Title), currTodo.RemindDate.Value);
				}					
				
				await _repo.Update(currTodo);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update to-do {model.Id}.", ex);
				throw;
			}
		}

		public async Task Delete(int id)
		{
			try
			{
				var todo = await _repo.GetTodo(id);
				if (todo != null)
				{
					if (!string.IsNullOrEmpty(todo.JobId))
					{
						_jobClient.Delete(todo.JobId);
					}
					await _repo.DeleteTodo(id);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete to-do {id}.", ex);
				throw;
			}
		}

		public async Task<IList<TodoModel>> Search(string term, int? projectId)
		{
			var todos  =  await _repo.SearchTodos(term, projectId);
			var models = _mapper.Map<IList<TodoModel>>(todos);
			return models;
		}

		[JobDisplayName("To-do: {1} ({0})")]				
		public async Task RemindTodo(int id, string title)
		{
			//View scheduled jobs http://localhost:5252/hangfire/jobs/scheduled

			var todo = await GetOne(id);
			if (todo == null)
			{
				return; 
			}
			if (todo.ProjectId == null)
			{
				await _hub.Clients.All.SendAsync("remindTodo", todo);
			}
			else
			{
				await _hub.Clients.All.SendAsync("remindProjectTodo", todo);
			}
		}
	}
}
