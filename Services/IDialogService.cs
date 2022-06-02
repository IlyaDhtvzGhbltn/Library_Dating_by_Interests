using Library.Contracts.MobileAndLibraryAPI.DTO.Dialog;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Dialog;
using System;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IDialogService : IService
    {
        Task<DialogPreview[]> PreviewDialogs(Guid apiUserId);
        Task<Dialog> OpenDialog(Guid dialogId, Guid requesterId);
        Task<Message[]> MoreMessages(Guid dialogId, Guid requesterId, int offset, int count);
        Task<bool> SendMessageIntoDialog(Guid senderId, Guid dialogId, string text, DateTime userSendingTime);
        Task<bool> DeleteDialog(Guid dialogId, Guid senderId);
    }
}
