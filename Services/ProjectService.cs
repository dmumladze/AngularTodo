using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using AngularTodo.Data.Entities;
using AngularTodo.Data.Repositiory;
using AngularTodo.Hubs;
using AngularTodo.Models;

namespace AngularTodo.Services;

public interface IProjectService
{
	Task<IList<ProjectModel>> GetAll();
	Task<ProjectModel> GetOne(int id, bool includeTodos); 
	Task<ProjectModel> Create(ProjectModel model);
	Task Update(int id, ProjectModel model);
	Task Delete(int id);
	Task<IList<ProjectModel>> Search(string term);
}

public class ProjectService : IProjectService
{
	private readonly IApiRepository _repo;
	private readonly IMapper _mapper;
	private readonly IBackgroundJobClient _jobClient;
	private readonly IHubContext<ApiHub> _hub;
	private readonly ILogger<ProjectService> _logger;

	public ProjectService(IApiRepository repo, IMapper mapper, IBackgroundJobClient jobClient,
		IHubContext<ApiHub> hub,
		ILogger<ProjectService> logger)
	{
		_repo = repo;
		_mapper = mapper;
		_jobClient = jobClient;
		_hub = hub;
		_logger = logger;
	}

	public async Task<IList<ProjectModel>> GetAll()
	{
		try
		{
			var projects = await _repo.GetAllProjects();
			var models = _mapper.Map<IList<ProjectModel>>(projects);
			return models;
		}
		catch (Exception ex)
		{
			_logger.LogError("Failed to get to-dos.", ex);
			throw;
		}
	}

	public async Task<ProjectModel> GetOne(int id, bool includeTodos = false)
	{
		try
		{
			var project = await _repo.GetProject(id, includeTodos);
			var model = _mapper.Map<ProjectModel>(project);
			return model;
		}
		catch (Exception ex)
		{
			_logger.LogError($"Failed to get project {id}.", ex);
			throw;
		}
	}

	public async Task<ProjectModel> Create(ProjectModel model)
	{
		try
		{
			var project = _mapper.Map<ProjectEntity>(model);
			var newProject = await _repo.Create(project);
			var newModel = _mapper.Map<ProjectModel>(newProject);
			return newModel;
		}
		catch (Exception ex)
		{
			_logger.LogError($"Failed to create project {model.Title}.", ex);
			throw;
		}
	}

	public async Task Update(int id, ProjectModel model)
	{
		try
		{
			var prevProj = await _repo.GetProject(id);
			var currProj = _mapper.Map<ProjectEntity>(model);

			if (!string.IsNullOrEmpty(prevProj.JobId))
			{
				_jobClient.Delete(prevProj.JobId);
			}
			if (currProj.RemindDate != null)
			{
				currProj.JobId = _jobClient.Schedule<ProjectService>(j => j.RemindProject(id, model.Title), currProj.RemindDate.Value);
			}

			await _repo.Update(currProj);
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
			var project = await _repo.GetProject(id);
			if (project != null)
			{
				if (!string.IsNullOrEmpty(project.JobId))
				{
					_jobClient.Delete(project.JobId);
				}
				await _repo.DeleteProject(id);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError($"Failed to delete to-do {id}.", ex);
			throw;
		}
	}

	public async Task<IList<ProjectModel>> Search(string term)
	{
		var projects = await _repo.SearchProjects(term);
		var models = _mapper.Map<IList<ProjectModel>>(projects);
		return models;
	}

	[JobDisplayName("Project: {1} ({0})")]
	public async Task RemindProject(int id, string title)
	{
		//View scheduled jobs http://localhost:5252/hangfire/jobs/scheduled

		var project = await GetOne(id);
		if (project == null)
		{
			return;
		}
		await _hub.Clients.All.SendAsync("remindProject", project);
	}
}
