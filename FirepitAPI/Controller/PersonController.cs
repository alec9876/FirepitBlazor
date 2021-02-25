using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirepitAPI.DTO;
using FirepitAPI.Logging;
using FirepitAPI.Models;
using FirepitAPI.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirepitAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly UserManager<Person> _userManager;
        private readonly IPersonRepository _personRepo;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public PersonController(IPersonRepository personRepo, ILoggerService logger, IMapper mapper, UserManager<Person> userManager)
        {
            _personRepo = personRepo;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Get all people
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var location = GetControllerActionNames();
            try
            {
                var people = await _personRepo.FindAll();
                var response = _mapper.Map<IList<PersonDTO>>(people);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Gets one person via Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPerson(string id)
        {
            var location = GetControllerActionNames();
            try
            {
                var person = await _personRepo.FindById(id);
                if (person == null)
                    return NotFound();

                var response = _mapper.Map<PersonDTO>(person);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }


        /// <summary>
        /// Update person data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePerson(string id, [FromBody] PersonUpdateDTO personDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                if (personDTO == null)
                    return BadRequest();

                var isExists = await _personRepo.isExists(personDTO.Id.ToString());
                if (!isExists)
                    return NotFound();

                if (!ModelState.IsValid)
                    return NotFound(ModelState);

                var user = await _userManager.FindByIdAsync(id);

                user.FirstName = personDTO.FirstName;
                user.LastName = personDTO.LastName;
                user.GoesBy = personDTO.GoesBy;
                user.Bio = personDTO.Bio;

                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return InternalError($"{location}: Update failed for record with id: {id}");
                }
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Delete person profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(string id)
        {
            var location = GetControllerActionNames();
            try
            {
                if (id == null)
                    return BadRequest();

                var isExists = await _personRepo.isExists(id);
                if (!isExists)
                    return NotFound();

                var person = await _personRepo.FindById(id);
                var isSuccess = await _personRepo.Delete(person);
                if(!isSuccess)
                    return InternalError($"{location}: Delete failed for record with id: {id}");

                return NoContent();
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
