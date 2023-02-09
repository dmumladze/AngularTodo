namespace TodoPlanner.Models;

public class ProjectModel
{
	public int Id { get; set; }
	public string? Title { get; set; } 
	public string? Description { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime? DueDate { get; set; }
	public DateTime? RemindDate { get; set; }
	public DateTime? CompletedDate { get; set; }
	public IList<TodoModel>? Todos { get; set; }
}
