using Microsoft.AspNetCore.Mvc;
using task_hub_backend.Models;
using task_hub_backend.Services;

namespace task_hub_backend.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _data;
        public MessageController(MessageService data)
        {
            _data = data;
        }

        [HttpPost]
        [Route("DirectMessage/{userID1}/{userID2}")]
        public bool DirectMessage(int userID1, int userID2)
        {
            return _data.DirectMessage(userID1, userID2);
        }
    }