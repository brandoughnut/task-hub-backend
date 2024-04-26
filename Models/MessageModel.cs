namespace task_hub_backend.Models;

    public class MessageModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int SenderID { get; set; }
        public int Message { get; set; }
        
        public MessageModel()
        {
            
        }
    }
