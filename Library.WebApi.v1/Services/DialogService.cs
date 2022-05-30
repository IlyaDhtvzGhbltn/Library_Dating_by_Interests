using Library.Contracts.MobileAndLibraryAPI.DTO.Dialog;
using Library.Entities;
using Library.Services;
using System;
using DTOMessage = Library.Contracts.MobileAndLibraryAPI.DTO.Dialog.Message;
using DTODialog = Library.Contracts.MobileAndLibraryAPI.DTO.Dialog.Dialog;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Services
{
    public class DialogService : IDialogService
    {
        private readonly IFactory<LibraryDatabaseContext> _dbFactory;

        public DialogService(IFactory<LibraryDatabaseContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        
        
        public Task<bool> DeleteDialog(Guid dialogId)
        {
            throw new NotImplementedException();
        }

        public Task<DTOMessage[]> MoreMessages(Guid dialogId, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public Task<DTODialog> OpenDialog(Guid dialogId)
        {
            throw new NotImplementedException();
        }

        public Task<DialogPreview[]> PreviewDialogs(Guid apiUserId)
        {

            return null;
        }

        public Task<bool> SendMessageIntoDialog(Guid senderId, Guid dialogId, string text)
        {
            throw new NotImplementedException();
        }
    }
}
