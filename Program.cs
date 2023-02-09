using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TodoPlanner.Data.Context;
using TodoPlanner.Data.Repositiory;
using TodoPlanner.Hubs;
using TodoPlanner.Services;

namespace TodoPlanner;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddCors(options =>
		{
			options.AddPolicy("CorsPolicy", config => config
				.WithOrigins("http://localhost:44455")
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials());
		});

		builder.Services.AddControllersWithViews();		
		builder.Services.AddApiVersioning(
			options =>
			{
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ReportApiVersions = true;
		});
		builder.Services.AddSignalR();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		var connectionBuilder = new SqliteConnectionStringBuilder()
		{
			DataSource = "memory.db",
			Mode = SqliteOpenMode.Memory,
			Cache = SqliteCacheMode.Shared
		};
		var connection = new SqliteConnection(connectionBuilder.ConnectionString);
		connection.Open();
		connection.EnableExtensions(true);

		builder.Services.AddDbContext<ApiContext>(options =>
		{
			options.UseSqlite(connection);
			options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		});		

		builder.Services.AddHangfire(conf => conf.UseInMemoryStorage());
		builder.Services.AddHangfireServer();

		builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		builder.Services.AddScoped<IApiRepository, ApiRepository>();
		builder.Services.AddScoped<ITodoService, TodoService>();
		builder.Services.AddScoped<IProjectService, ProjectService>();

		var app = builder.Build();
		
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseStaticFiles();
		app.UseRouting();

		app.UseCors("CorsPolicy");
		app.MapHub<ApiHub>("/hub");		

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller}/{action=Index}/{id?}"
		);		

		app.UseHangfireDashboard();
		app.MapFallbackToFile("index.html");

		app.Run();
	}
}