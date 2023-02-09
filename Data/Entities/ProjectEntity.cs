namespace AngularTodo.Data.Entities;

public class ProjectEntity
{
	public int ProjectEntityId { get; set; }
	public string JobId { get; set; }	
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime? DueDate { get; set; }
	public DateTime? RemindDate { get; set; }
	public DateTime? CompletedDate { get; set; }
	public IList<TodoEntity> Todos { get;set; }
}
