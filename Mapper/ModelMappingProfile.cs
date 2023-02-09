using AutoMapper;
using AngularTodo.Data.Entities;
using AngularTodo.Models;

namespace AngularTodo.Mapper
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
