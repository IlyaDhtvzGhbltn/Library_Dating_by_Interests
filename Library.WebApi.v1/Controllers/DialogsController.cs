using Library.Contracts.MobileAndLibraryAPI.DTO.Dialog;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Dialog;
using Library.Services;
using Library.WebApi.v1.Filters;
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
    [AuthenticationFilter]
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
            [FromRoute]string internalUserId) 
        {
            DialogPreview[] dialogs = await _dialogService.PreviewDialogs(internalUserId);
            return dialogs;
        }

        [HttpGet]
        [Route("dialog/{dialogId}")]
        public async Task<Dialog> OpenDialog(
            [FromRoute] string dialogId
            )
        {
            Dialog dialog = await _dialogService.OpenDialog(dialogId);
            return dialog;
        }

        [HttpGet]
        [Route("dialog/{dialogId}/moremessages")]
        public async Task<Message[]> MoreMessages(
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