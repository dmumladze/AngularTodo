namespace TodoPlanner.Data.Entities;

public class TodoEntity
{
	public int TodoEntityId { get; set; }
	public int? ProjectEntityId { get; set; }
	public ProjectEntity Project { get; set; }
	public int? PredecessorId { get; set; }
	public int? SuccessorId { get; set; } 
	public string JobId { get; set; }
	public string Title { get; set; }
	public string Notes { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime? DueDate { get; set; }
	public DateTime? RemindDate { get; set; }
	public DateTime? CompletedDate { get; set; }
}
