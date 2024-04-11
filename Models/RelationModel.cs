namespace task_hub_backend.Models;
public class RelationModel
{
    public int ID { get; set; }
    public int ProjectID { get; set; }

    // users who are apart of the project
    public int UserID { get; set; }
    public RelationModel()
    {
        
    }
}
