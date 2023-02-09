using Microsoft.EntityFrameworkCore;
using AngularTodo.Data.Entities;

namespace AngularTodo.Data.Context;

public class ApiContext : DbContext
{
	public ApiContext(DbContextOptions<ApiContext> options)
		: base(options)
	{
		Database.EnsureCreated();
	}

	public DbSet<TodoEntity> Todos { get; set; }

	public DbSet<ProjectEntity> Projects { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<ProjectEntity>().HasKey(p => p.ProjectEntityId);
		modelBuilder.Entity<ProjectEntity>().Property(p => p.ProjectEntityId).ValueGeneratedOnAdd();
		modelBuilder.Entity<ProjectEntity>().HasData(
			new ProjectEntity
			{
				ProjectEntityId = 2,
				Title = "Organize Charity for Veterans",				
				CreatedDate = DateTime.Now,
				RemindDate= DateTime.Now.AddMinutes(1)
			},
			new ProjectEntity
			{
				ProjectEntityId = 3,
				Title = "Prepare for Summer Adventure",				
				CreatedDate = DateTime.Now				
			},
			new ProjectEntity
			{
				ProjectEntityId = 1,
				Title = "Build Adventure Bike",
				Description = "For surveying TET Georgia route",
				CreatedDate = DateTime.Now,
				DueDate = DateTime.Now.AddMonths(1),
				RemindDate = DateTime.Now.AddMinutes(2)
			});
		modelBuilder.Entity<ProjectEntity>().HasMany(b => b.Todos).WithOne(p => p.Project)
			.HasForeignKey(p => p.ProjectEntityId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<TodoEntity>().HasKey(p => p.TodoEntityId);
		modelBuilder.Entity<TodoEntity>().Property(p => p.TodoEntityId).ValueGeneratedOnAdd();		
		modelBuilder.Entity<TodoEntity>().Property(p => p.Notes).IsRequired(false);
		modelBuilder.Entity<TodoEntity>().Property(p => p.CreatedDate).IsRequired();
		modelBuilder.Entity<TodoEntity>().HasData(
			new TodoEntity
			{
				TodoEntityId = 1,
				ProjectEntityId = 1,
				Title = "Get DRZ-400",
				Notes = "Must be used and in good condition.",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 2,
				ProjectEntityId = 1,
				Title = "Modify Jetting",
				Notes = "Get JD Jet Kit from jdjetting.com.",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 3,
				ProjectEntityId = 1,
				Title = "Upgrade FMF Exchaust",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 4,
				ProjectEntityId = 1,
				Title = "Get Luggage Rack",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 5,
				ProjectEntityId = 1,
				Title = "Get Saddlebags",
				Notes = "Enduristan vs. Mosko Moto vs. Giany Loop.",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 6,
				ProjectEntityId = 1,
				Title = "Ship DRZ400 to Georgia",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 7,
				Title = "Fix Chimney",
				RemindDate = DateTime.Now.AddMinutes(2)
			},
			new TodoEntity
			{
				TodoEntityId = 8,
				Title = "Cleanout Shed",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 9,
				Title = "Rotate Tires on Tacoma",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 10,
				Title = "Change Oil in KTM 1090",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 11,
				Title = "Buy Milk & Coffee",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 12,
				Title = "Check Toma's Homework",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 13,
				ProjectEntityId = 2,
				Title = "Talk to Chuck and him friends",
				CreatedDate = DateTime.Now,
				RemindDate = DateTime.Now.AddMinutes(2)
			},
			new TodoEntity
			{
				TodoEntityId = 14,
				ProjectEntityId = 2,
				Title = "Setup GoFundMe",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 15,
				ProjectEntityId = 3,
				Title = "Look for AirBnb Houses",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 16,
				ProjectEntityId = 3,
				Title = "Learn some Spanish",
				CreatedDate = DateTime.Now
			},
			new TodoEntity
			{
				TodoEntityId = 17,
				ProjectEntityId = 3,
				Title = "Reserve Side by Sides for 3 adults",
				CreatedDate = DateTime.Now,
				RemindDate = DateTime.Now.AddMinutes(1)
			}
		);
	}
}
