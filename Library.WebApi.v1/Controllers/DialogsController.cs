using Library.Contracts.MobileAndLibraryAPI.DTO.Dialog;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Dialog;
using Library.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class DialogsController : ControllerBase
    {
        private readonly IDialogService _dialogService;

        public DialogsController(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        [HttpGet]
        [Route("dialogsbyuser/{internalUserId}")]
        public async Task<DialogPreview[]> PreviewDialogs(
            [FromRoute]string internalUserId,
            [FromHeader] string internalBearerToken) 
        {
            DialogPreview[] dialogs = await _dialogService.PreviewDialogs(internalUserId);
            return dialogs;
        }

        [HttpGet]
        [Route("dialog/{dialogId}")]
        public async Task<Dialog> OpenDialog(
            [FromHeader] string internalUserId,
            [FromHeader] string internalBearerToken,
            [FromRoute] string dialogId
            )
        {
            Dialog dialog = await _dialogService.OpenDialog(dialogId);
            return dialog;
        }

        [HttpGet]
        [Route("dialog/{dialogId}/moremessages")]
        public async Task<Message[]> MoreMessages(
            [FromHeader] string internalUserId,
            [FromHeader] string internalBearerToken,
            [FromRoute] string dialogId,
            [FromQuery] int offset,
            [FromQuery] int count) 
        {
            var oldMessages = await _dialogService.MoreMessages(dialogId, offset, count);
            return oldMessages;
        }


        [HttpPost]
        [Route("dialog/{dialogId}")]
        public async Task<bool> SendMessage(
            [FromHeader] string internalUserId,
            [FromHeader] string internalBearerToken,
            [FromRoute]string dialogId,
            [FromBody] SendMessageIntoDialogRequest request) 
        {
            return await _dialogService.SendMessageIntoDialog(internalUserId, dialogId, request.MessageText);
        }



        [HttpDelete]
        [Route("dialog/{internalId}")]
        public async Task<bool> DeleteDialog(
            [FromRoute]string internalId,
            [FromHeader] string internalBearerToken) 
        {
            bool deleted = await _dialogService.DeleteDialog(internalId);
            return deleted;
        }

    }
}