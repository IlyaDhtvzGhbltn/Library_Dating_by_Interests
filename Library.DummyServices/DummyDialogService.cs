using Library.Contracts.MobileAndLibraryAPI.DTO.Dialog;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Dialog;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.DummyServices
{
    public class DummyDialogService : IDialogService
    {
        public async Task<DialogPreview[]> PreviewDialogs(string internalUserId)
        {
            var dialogs = new DialogPreview[2];
            dialogs[0] = new DialogPreview()
            {
                DialogId = "4acf214e-8136-4c5c-932b-98c4a063946f",
                Interlocutor = new Interlocutor()
                {
                    Avatar = new Uri("https://yt3.ggpht.com/ytc/AKedOLTBuPyM_vDb0HhVAdaiEQosYPsDxBo7FHgPDGFJbQ=s176-c-k-c0x00ffffff-no-rj"),
                    Name = "Denis Kazanskyi"
                },
                LastMessageCuttedText = "все буде добре!",
                LastMessageSentDate = new DateTime(2022, 05, 16),
                UnreadMessagesCount = 1
            };
            dialogs[1] = new DialogPreview()
            {
                DialogId = "98c4a063946f-8136-4c5c-932b-4acf214e",
                Interlocutor = new Interlocutor()
                {
                    Avatar = new Uri("https://yt3.ggpht.com/8jwjxBVOsqIFY00sfeAUDdIPvW2EbSv6CEvdPl-ys5GcranurSeb0U8gaGeXU8W-qTf0QjrEvQ=s176-c-k-c0x00ffffff-no-rj"),
                    Name = "Максим Кац"
                },
                LastMessageCuttedText = "посмотри моё новое видео если интересно",
                LastMessageSentDate = new DateTime(2022, 05, 16),
                UnreadMessagesCount = 2
            };

            return dialogs;
        }

        public async Task<Dialog> OpenDialog(string dialogId)
        {
            if (dialogId == "4acf214e-8136-4c5c-932b-98c4a063946f")
            {
                var messagesFromDenis = new List<Message>();
                messagesFromDenis.Add(new Message() { Im = true, MessageText = "привiт. какие планы?", SentDate = DateTime.Now.AddMinutes(-16) });
                messagesFromDenis.Add(new Message() { Im = false, MessageText = "доброго ранку. повернути своє)", SentDate = DateTime.Now.AddMinutes(-15) });
                messagesFromDenis.Add(new Message() { Im = true, MessageText = "думаешь все будет ок?", SentDate = DateTime.Now.AddMinutes(-14) });
                messagesFromDenis.Add(new Message() { Im = false, MessageText = "все буде добре!", SentDate = DateTime.Now.AddMinutes(-12) });

                return new Dialog()
                {
                    FriendsOnlineStatus = "Online",
                    IsFriendOnline = true,
                    Interlocutor = new Interlocutor()
                    {
                        Avatar = new Uri("https://yt3.ggpht.com/ytc/AKedOLTBuPyM_vDb0HhVAdaiEQosYPsDxBo7FHgPDGFJbQ=s176-c-k-c0x00ffffff-no-rj"),
                        Name = "Denis Kazanskyi"
                    },
                    Messages = messagesFromDenis
                };
            }
            else if (dialogId == "98c4a063946f-8136-4c5c-932b-4acf214e")
            {
                var messagesFromMaxim = new List<Message>();
                messagesFromMaxim.Add(new Message() { Im = false, MessageText = "привет", SentDate = DateTime.Now.AddDays(-2) });
                messagesFromMaxim.Add(new Message() { Im = false, MessageText = "посмотри моё новое видео если интересно", SentDate = DateTime.Now.AddDays(-2) });

                return new Dialog()
                {
                    FriendsOnlineStatus = "Был онлайн 2 дня назад",
                    IsFriendOnline = false,
                    Interlocutor = new Interlocutor() 
                    {
                        Avatar = new Uri("https://yt3.ggpht.com/8jwjxBVOsqIFY00sfeAUDdIPvW2EbSv6CEvdPl-ys5GcranurSeb0U8gaGeXU8W-qTf0QjrEvQ=s176-c-k-c0x00ffffff-no-rj"),
                        Name = "Максим Кац"
                    },
                    Messages = messagesFromMaxim
                };
            }
            else return null;
        }

        public async Task<Message[]> MoreMessages(string dialogId, int offset, int count)
        {
            return null;
        }

        public async Task<bool> SendMessageIntoDialog(string senderId, string dialogId, string text)
        {
            return true;
        }

        public async Task<bool> DeleteDialog(string dialogId)
        {
            return true;
        }
    }
}
