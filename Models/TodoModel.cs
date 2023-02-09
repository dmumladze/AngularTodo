namespace AngularTodo.Models;
 
public class TodoModel
{
	public int Id { get; set; }
	public int? ProjectId { get; set; }
	public int? PredecessorId { get; set; }
	public int? SuccessorId { get; set; }
	public string? Title { get; set; }
	public string? Notes { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime? DueDate { get; set; }
	public DateTime? RemindDate { get; set; }
	public DateTime? CompletedDate { get; set; }
	
}
