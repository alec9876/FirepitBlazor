using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirepitAPI.DTO;
using FirepitAPI.Logging;
using FirepitAPI.Models;
using FirepitAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirepitAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingRepository _meetingRepo;
        private readonly IPersonRepository _personRepo;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public MeetingController(IMeetingRepository meetingRepo, ILoggerService logger, IMapper mapper,
            IPersonRepository personRepo)
        {
            _meetingRepo = meetingRepo;
            _logger = logger;
            _mapper = mapper;
            _personRepo = personRepo;
        }

        /// <summary>
        /// Find all meetings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMeetings()
        {
            var location = GetControllerActionsNames();
            try
            {
                var meetings = await _meetingRepo.FindAll();
                var response = _mapper.Map<IList<MeetingDTO>>(meetings);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Find meeting by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMeeting(int id)
        {
            var location = GetControllerActionsNames();
            try
            {
                var meeting = await _meetingRepo.FindById(id);
                if (meeting == null)
                    return NotFound();

                var response = _mapper.Map<MeetingDTO>(meeting);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Create a new meeting
        /// </summary>
        /// <param name="meetingDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateMeeting([FromBody] MeetingCreateDTO meetingDTO)
        {
            var location = GetControllerActionsNames();
            try
            {
                if (meetingDTO == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var meeting = _mapper.Map<Meeting>(meetingDTO);

                var isSuccessMeeting = await _meetingRepo.Create(meeting);
                var isSuccessUserMeeting = await SaveUserMeeting(meeting);

                if(!isSuccessMeeting)
                    return InternalError($"{location}: Creation failed");


                return Created("CreateMeeting", new { meeting });

            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Update meeting data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="meetingDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMeeting(int id, [FromBody] MeetingUpdateDTO meetingDTO)
        {
            var location = GetControllerActionsNames();
            try
            {
                if (id < 1 || meetingDTO == null || id != meetingDTO.Id)
                    return BadRequest();

                var isExists = await _meetingRepo.isExists(id);
                if (!isExists)
                    return NotFound();

                if (!ModelState.IsValid)
                    return NotFound(ModelState);

                var meeting = _mapper.Map<Meeting>(meetingDTO);
                var isSuccess = await _meetingRepo.Update(meeting);
                if(!isSuccess)
                    return InternalError($"{location}: Update failed for record with id: {id}");

                return NoContent();
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Delete meeting profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            var location = GetControllerActionsNames();
            try
            {
                if (id < 1)
                    return BadRequest();

                var isExists = await _meetingRepo.isExists(id);
                if (!isExists)
                    return NotFound();

                var meeting = await _meetingRepo.FindById(id);
                var isSuccess = await _meetingRepo.Delete(meeting);
                if(!isSuccess)
                    return InternalError($"{location}: Delete failed for record with id: {id}");

                return NoContent();
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        private string GetControllerActionsNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong.  Please contact admin.");
        }

        // UserMeeting Logic
        private async Task<OkResult> SaveUserMeeting(Meeting meeting)
        {
            var person = await _personRepo.FindAll();
            foreach(var item in person)
            {
                var newUserMeeting = new UserMeeting()
                {
                    MeetingId = meeting.Id,
                    PersonId = item.Id,
                    Going = null
                };
                await _meetingRepo.CreateUserMeeting(newUserMeeting);
            }
            return Ok();
        }

        /// <summary>
        /// Updates UserMeetingTable
        /// </summary>
        /// <param name="MeetingId"></param>
        /// <param name="userMeetingDTO"></param>
        /// <returns></returns>
        [HttpPut("updateusermeeting/{MeetingId}")]
        public async Task<IActionResult> UpdateUserMeeting(int MeetingId, UserMeetingUpdateDTO userMeetingDTO)
        {
            var location = GetControllerActionsNames();
            try
            {
                if (userMeetingDTO == null)
                    return BadRequest();

                var isExists = await _meetingRepo.isExists(MeetingId);
                var isPersonExists = await _personRepo.isExists(userMeetingDTO.PersonId);
                if (!isExists || !isPersonExists)
                    return NotFound();

                if (!ModelState.IsValid)
                    return NotFound(ModelState);

                var userMeeting = _mapper.Map<UserMeeting>(userMeetingDTO);
                var isSuccess = await _meetingRepo.UpdateUserMeeting(userMeeting);
                if (!isSuccess)
                    return InternalError($"{location}: Update failed for UserMeeting Record.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }
    }
}
