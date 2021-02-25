using FirepitAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirepitAPI.Logging;
using FirepitAPI.Models;
using FirepitAPI.DTO;
using System.Security.Claims;

namespace FirepitAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatHistoryRepository _chatRepo;
        private readonly IPersonRepository _personRepo;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public ChatController(IChatHistoryRepository chatRepo, IMapper mapper, ILoggerService logger, IPersonRepository personRepo)
        {
            _chatRepo = chatRepo;
            _mapper = mapper;
            _logger = logger;
            _personRepo = personRepo;
        }

        /// <summary>
        /// Get chats between users
        /// </summary>
        /// <param name="ToUserId"></param>
        /// <returns></returns>
        [HttpGet("getchats/{ToUserId}")]
        public async Task<IActionResult> GetChats(string ToUserId)
        {
            var location = GetControllerActionNames();
            try
            {
                var getUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userId = _personRepo.FindByEmail(getUser);

                var FromUserId = userId.ToString();

                var chats = await _chatRepo.GetChats(FromUserId, ToUserId);
                var response = _mapper.Map<IList<ChatHistoryDTO>>(chats);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong. Please contact the Administrator");
        }
    }
}
