﻿using Library.Contracts.MobileAndLibraryAPI.DTO.Dialog;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Dialog;
using System;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IDialogService
    {
        Task<DialogPreview[]> PreviewDialogs(Guid apiUserId);
        Task<Dialog> OpenDialog(Guid dialogId);
        Task<Message[]> MoreMessages(Guid dialogId, int offset, int count);
        Task<bool> SendMessageIntoDialog(Guid senderId, Guid dialogId, string text);
        Task<bool> DeleteDialog(Guid dialogId);
    }
}
