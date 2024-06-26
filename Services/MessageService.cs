using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.Mvc;
using task_hub_backend.Models;
using task_hub_backend.Services.Context;

namespace task_hub_backend.Services;

    public class MessageService : ControllerBase
    {
        private readonly DataContext _context;

        public MessageService(DataContext context)
        {
            _context = context;
        }

        public bool SaveChangesToDataBase()
        {
            return _context.SaveChanges() != 0;
        }

        public bool DoesUserExist(int userID)
        {
            return _context.UserInfo.SingleOrDefault(user => user.ID == userID) != null;
        }

        public bool IsUserAlreadyDM(int userID1, int userID2)
        {
            return _context.MessageInfo.SingleOrDefault(check => (check.UserID1 == userID1 && check.UserID2 == userID2) || (check.UserID1 == userID2 && check.UserID2 == userID1)) != null;
        }

        public bool DirectMessage(int userID1, int userID2)
        {
            bool result = false;
            if(IsUserAlreadyDM(userID1, userID2)){
                MessageModel existingMessage = _context.MessageInfo.SingleOrDefault(user => (user.UserID1 == userID1) && (user.UserID2 == userID2));
                existingMessage.IsVisible = true;
                _context.Update<MessageModel>(existingMessage);
                result = _context.SaveChanges() != 0;
            }
            else if(!IsUserAlreadyDM(userID1, userID2) && DoesUserExist(userID2) && (userID1 != userID2)){
                MessageModel newMessage = new MessageModel();
                newMessage.Room = _context.MessageInfo.Count() + 1;
                newMessage.UserID1 = userID1;
                newMessage.UserID2 = userID2;
                newMessage.IsVisible = true;
                _context.Add(newMessage);
                result = _context.SaveChanges() != 0;
            }
            

            return result;
        }

        public IEnumerable<MessageModel> GetAllDMS(int userID)
        {
            return _context.MessageInfo.Where(user => (user.UserID1 == userID) || (user.UserID2 == userID));
        }

        public bool DeleteDM(int ID)
        {
            MessageModel room = _context.MessageInfo.SingleOrDefault(yeah => yeah.ID == ID);
            bool result = false;
            if(room != null){
                room.IsVisible = false;
                _context.Update<MessageModel>(room);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public bool CreateMessage(MessageDataModel newMessage)
        {
            _context.Add(newMessage);

            return _context.SaveChanges() != 0;
        }

        public IEnumerable<MessageDataModel> GetAllMessagesWithinRoom(int roomID)
        {
            return _context.MessageDataInfo.Where(room => room.Room == roomID);
        }
    }
