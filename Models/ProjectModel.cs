namespace task_hub_backend.Models;

    public class ProjectModel
    {
        public int ProjectID { get; set; }
        public string? ProjectName { get; set; }
        public bool IsDeleted { get; set; }
    }
