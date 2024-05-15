namespace task_hub_backend.Models;

    public class MessageModel
    {
        public int ID { get; set; }
        public int UserID1 { get; set; }
        public int UserID2 { get; set; }
        public int Room { get; set; }
        
        public MessageModel()
        {
            
        }
    }
