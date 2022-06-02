using Library.Contracts.MobileAndLibraryAPI.DTO.Dialog;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Dialog;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    [Authorize]
    public class DialogsController : ControllerBase
    {
        private readonly IDialogService _dialogService;
        private Guid _apiUserId => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public DialogsController(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        [HttpGet]
        [Route("my_dialogs")]
        public async Task<DialogPreview[]> PreviewDialogs() 
        {
            DialogPreview[] dialogs = await _dialogService.PreviewDialogs(_apiUserId);
            return dialogs;
        }

        [HttpGet]
        [Route("dialog/{dialogId}")]
        public async Task<Dialog> OpenDialog([FromRoute] Guid dialogId)
        {
            Dialog dialog = await _dialogService.OpenDialog(dialogId, _apiUserId);
            if (dialog == null)
            { 
                Response.StatusCode = 404; 
            }
            return dialog;
        }

        [HttpGet]
        [Route("dialog/{dialogId}/moremessages")]
        public async Task<Message[]> MoreMessages(
            [FromRoute] Guid dialogId,
            [FromQuery] int offset,
            [FromQuery] int count) 
        {
            var oldMessages = await _dialogService.MoreMessages(dialogId, _apiUserId, offset, count);
            if (oldMessages == null)
            {
                Response.StatusCode = 404;
            }
            return oldMessages;
        }


        [HttpPost]
        [Route("dialog/{dialogId}/send_message")]
        public async Task<bool> SendMessage(
            [FromRoute]Guid dialogId,
            [FromBody] SendMessageIntoDialogRequest request) 
        {
            return await _dialogService.SendMessageIntoDialog(_apiUserId, dialogId, request.MessageText, request.ClientTime);
        }



        [HttpDelete]
        [Route("dialog/{dialogId}")]
        public async Task<bool> DeleteDialog(Guid dialogId) 
        {
            bool deleted = await _dialogService.DeleteDialog(dialogId);
            return deleted;
        }

    }
}