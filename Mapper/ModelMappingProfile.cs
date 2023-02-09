using AutoMapper;
using TodoPlanner.Data.Entities;
using TodoPlanner.Models;

namespace TodoPlanner.Mapper
{
	public class ModelMappingProfile : Profile
	{
		public ModelMappingProfile()
		{
			CreateMap<TodoEntity, TodoModel>()
				.ForMember(p => p.Id, opt => opt.MapFrom(p => p.TodoEntityId))
				.ForMember(p => p.ProjectId, opt => opt.MapFrom(p => p.ProjectEntityId))
				.ReverseMap();

			CreateMap<ProjectEntity, ProjectModel>()
				.ForMember(p => p.Id, opt => opt.MapFrom(p => p.ProjectEntityId))				
				.ReverseMap();
		}
	}
}
