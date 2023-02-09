namespace TodoPlanner.Models
{
	public class Reminder<TModel>
	{		
		public string Name { get; set; }
		public DateTime RemindDate { get; set; }

		public TModel Model { get; set; }
	}
}
