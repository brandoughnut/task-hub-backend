namespace task_hub_backend.Models;

    public class ProjectModel
    {
        // project ID
        public int ID { get; set; }
        
        // user who created project
        public int UserID { get; set; }
        public string? ProjectName { get; set; }
        public bool IsDeleted { get; set; }

        public ProjectModel()
        {

        }
    }
