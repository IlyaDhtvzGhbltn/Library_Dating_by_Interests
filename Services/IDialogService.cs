using Library.Contracts.MobileAndLibraryAPI.DTO.Dialog;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Dialog;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IDialogService
    {
        Task<DialogPreview[]> PreviewDialogs(string internalUserId);
        Task<Dialog> OpenDialog(string dialogId);
        Task<Message[]> MoreMessages(string dialogId, int offset, int count);
        Task<bool> SendMessageIntoDialog(string senderId, string dialogId, string text);
        Task<bool> DeleteDialog(string dialogId);
    }
}
