using API.DTOs;
using API.Extensions;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Domain.ModelsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class MessagesController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public MessagesController(IMapper mapper, IUnitOfWork uow)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetEmail();

            if (username == createMessageDto.RecipientEmail.ToLower())
                return BadRequest("You cannot send messages to yourself");

            var sender = await _uow.UserRepository.GetUserByEmailAsync(username);
            var recipient = await _uow.UserRepository.GetUserByEmailAsync(createMessageDto.RecipientEmail);

            if (recipient == null) return NotFound();

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderEmail = sender.UserName,
                RecipientEmail = recipient.UserName,
                Content = createMessageDto.Content
            };

            _uow.MessageRepository.AddMessage(message);

            if (await _uow.Complete()) return Ok(_mapper.Map<MessageDto>(message));

            return BadRequest("Failed to send message");
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MessageDto>>> GetMessagesForUser([FromQuery]
            MessageParams messageParams)
        {
            messageParams.Email = User.GetEmail();

            var messages = await _uow.MessageRepository.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(new PaginationHeader(messages.CurrentPage, messages.PageSize,
                messages.TotalCount, messages.TotalPages));

            return messages;
        }

        [HttpGet("unread")]
        public async Task<ActionResult<int>> GetUnreadMessagesCount()
        {
            var email = User.GetEmail();

            var count = await _uow.MessageRepository.CountUnreadMessagesForUser(email);

            return Ok(count);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(Guid id)
        {
            var username = User.GetEmail();

            var message = await _uow.MessageRepository.GetMessage(id);

            if (message.SenderEmail != username && message.RecipientEmail != username)
                return Unauthorized();

            if (message.SenderEmail == username) message.SenderDeleted = true;
            if (message.RecipientEmail == username) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
            {
                _uow.MessageRepository.DeleteMessage(message);
            }

            if (await _uow.Complete()) return Ok();

            return BadRequest("Problem deleting the message");

        }
    }
}