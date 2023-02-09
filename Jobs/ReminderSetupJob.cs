using AngularTodo.Services;

namespace AngularTodo.Jobs;

public interface IReminderSetupJob
{
	Task Execute();
}

public class ReminderSetupJob : IReminderSetupJob
{
	private readonly ITodoService _todoService;
	private readonly IProjectService _projectService;
	private readonly ILogger<ReminderSetupJob> _logger;

	public ReminderSetupJob(ITodoService todoService, IProjectService projectService, ILogger<ReminderSetupJob> logger)
	{
		_todoService = todoService;
		_projectService = projectService;
		_logger = logger;
	}

	public async Task Execute()
	{
		try
		{
			var todos = await _todoService.GetAll();
			if (todos != null) 
			{ 
				foreach (var todo in todos)
				{
					if (todo.RemindDate == null)
						continue;
					await _todoService.Update(todo.Id, todo);
				}
			}
			_logger.LogInformation("Setup To-do reminders completed.");

			var projects = await _projectService.GetAll();
			if (projects != null)
			{
				foreach (var project in projects)
				{
					if (project.RemindDate == null)
						continue;
					await _projectService.Update(project.Id, project);
				}
			}
			_logger.LogInformation("Setup Project reminders completed.");
		}
		catch (Exception ex)
		{
			_logger.LogError("Error registering reminders.", ex);
		}
	}
}

public static class ReminderSetupJobExtension
{
	public static void SetupReminders(this IApplicationBuilder app)
	{
		using (var serviceScope = app.ApplicationServices.CreateScope())
		{
			var services = serviceScope.ServiceProvider;
			var reminderJob = services.GetRequiredService<IReminderSetupJob>();		
			
			reminderJob.Execute();
		}
	}
}
