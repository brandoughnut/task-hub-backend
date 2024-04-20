
namespace task_hub_backend.Models;
public class TaskModel
{
    public int ID { get; set; }
    public int ProjectID { get; set; }
    public string? TaskName { get; set; }
    public string? TaskDescription { get; set; }
    public string? TaskDuration { get; set; }
    public int UserID { get; set; }
    public string? DueDate { get; set; }
    public string? Priority { get; set; }
    public string? Status { get; set; }
    public bool IsDeleted { get; set; }
}