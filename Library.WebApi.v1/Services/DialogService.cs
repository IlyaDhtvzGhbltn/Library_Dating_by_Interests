using Library.Contracts.MobileAndLibraryAPI.DTO.Dialog;
using Library.Entities;
using Library.Services;
using System;
using DTOMessage = Library.Contracts.MobileAndLibraryAPI.DTO.Dialog.Message;
using DTODialog = Library.Contracts.MobileAndLibraryAPI.DTO.Dialog.Dialog;
using System.Linq;
using System.Threading.Tasks;
using Library.WebApi.v1.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Library.WebApi.v1.Services
{
    public class DialogService : IDialogService
    {
        private readonly IFactory<LibraryDatabaseContext> _dbFactory;

        public DialogService(IFactory<LibraryDatabaseContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        
      

        public async Task<DialogPreview[]> PreviewDialogs(Guid apiUserId)
        {
            using (var context = _dbFactory.Create())
            {
                ApiUser user = await this.FindUserById(context, apiUserId);
                var userDialogs = context.Dialogs
                    .Include(x => x.Messages)
                    .Include(x => x.Participants).ThenInclude(p => p.Photos)
                    .Where(x => x.Participants.Contains(user))
                    .ToArray();

                var previews = new DialogPreview[userDialogs.Length];
                for (int i = 0; i < userDialogs.Length; i++) 
                {
                    var lastMessage = userDialogs[i].Messages.Last();
                    var interlocutor = userDialogs[i].Participants.First(p => p.Id != user.Id);

                    var dialogPreview = new DialogPreview();
                    dialogPreview.LastMessageSentDate = lastMessage.SendingTime;
                    string lastMessageText = lastMessage.Text;
                    string cutLastMessage = (lastMessageText.Length <= 200) ? lastMessageText : lastMessageText.Substring(0, 199);
                    dialogPreview.LastMessageCuttedText = cutLastMessage;
                    dialogPreview.DialogId = userDialogs[i].Id;
                    dialogPreview.Interlocutor = new Interlocutor
                    {
                        Avatar = interlocutor.Photos.First(p => p.IsAvatar == true).PhotoUrl,
                        Name = interlocutor.UserName
                    };

                    previews[i] = dialogPreview;

                }
                return previews;
            }
        }

        public async Task<DTODialog> OpenDialog(Guid dialogId, Guid requesterId)
        {
            string reqId = requesterId.ToString();
            using (var context = _dbFactory.Create())
            {
                ApiUser requester = await this.FindUserById(context, requesterId);
                var dialogEntry = context.Dialogs
                    .Include(x => x.Messages)
                    .Include(x=>x.Participants).ThenInclude(y => y.Photos)
                    .Where(x => x.Participants.Contains(requester))
                    .FirstOrDefault(x => x.Id == dialogId);
                if (dialogEntry == null)
                    return null;

                ApiUser interlocutorApiUser = dialogEntry.Participants
                    .First(x => x.Id != reqId);

                List<DTOMessage> messages = dialogEntry.Messages.Take(50)
                    .Select(x => new DTOMessage 
                    {
                        MessageText = x.Text, 
                        SentDate = x.SendingTime,
                        Im = x.Sender.Id == reqId

                    }).ToList();

                var dialog = new DTODialog();
                dialog.FriendsOnlineStatus = null;
                dialog.IsFriendOnline = true;
                dialog.Interlocutor = new Interlocutor
                {
                    Avatar = interlocutorApiUser.Photos.First(x => x.IsAvatar == true).PhotoUrl,
                    Name = interlocutorApiUser.UserName
                };
                dialog.Messages = messages;
                return dialog;
            }
        }

        public Task<DTOMessage[]> MoreMessages(Guid dialogId, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendMessageIntoDialog(Guid senderId, Guid dialogId, string text)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDialog(Guid dialogId)
        {
            throw new NotImplementedException();
        }
    }
}
