namespace task_hub_backend.Models;

    public class MessageDataModel
    {
        public int ID { get; set; }
        public int SenderID { get; set; }
        public int Room { get; set; }
        public string Message { get; set; }

        public MessageDataModel()
        {
            
        }
    }
