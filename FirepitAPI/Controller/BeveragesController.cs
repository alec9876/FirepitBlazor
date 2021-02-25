using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirepitAPI.Data;
using FirepitAPI.DTO;
using FirepitAPI.Logging;
using FirepitAPI.Models;
using FirepitAPI.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FirepitAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeveragesController : ControllerBase
    {
        private readonly IBeveragesRepository _beverageRepo;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IPersonRepository _personRepo;

        public BeveragesController(IBeveragesRepository beverageRepo, IMapper mapper, 
            ILoggerService logger, IPersonRepository personRepo)
        {
            _beverageRepo = beverageRepo;
            _mapper = mapper;
            _logger = logger;
            _personRepo = personRepo;
        }

        /// <summary>
        /// Get all beverages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBeverages()
        {
            var location = GetControllerActionNames();
            try
            {
                var beverages = await _beverageRepo.FindAll();
                var response = _mapper.Map<IList<BeverageDTO>>(beverages);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Get all beverage types
        /// </summary>
        /// <returns></returns>
        [Route("allbeveragetypes")]
        [HttpGet]
        public async Task<IActionResult> GetBeveragesTypes()
        {
            var location = GetControllerActionNames();
            try
            {
                var beverages = await _beverageRepo.FindTypes();
                var response = _mapper.Map<IList<BeverageDTO>>(beverages);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }
        
        /// <summary>
        /// Get one beverage types
        /// </summary>
        /// <returns></returns>
        [HttpGet("beveragetype/{type}")]
        public async Task<IActionResult> GetBeveragesType(string type)
        {
            var location = GetControllerActionNames();
            try
            {
                var beverages = await _beverageRepo.FindByType(type);
                var response = _mapper.Map<IList<BeverageDTO>>(beverages);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Get beverage by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBeverage(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                var beverage = await _beverageRepo.FindById(id);
                if (beverage == null)
                {
                    _logger.LogWarn($"{location}: Failed to retrieve {id}");
                    return NotFound();
                }
                var response = _mapper.Map<BeverageDTO>(beverage);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Create a beverage
        /// </summary>
        /// <param name="beverageDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateBeverage([FromBody] BeverageCreateDTO beverageDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                if (beverageDTO == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var getUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userId =  _personRepo.FindByEmail(getUser);

                beverageDTO.PersonId = userId.Result.Id;

                var beverage = _mapper.Map<Beverages>(beverageDTO);
                var isSuccess = await _beverageRepo.Create(beverage);
                if(!isSuccess)
                    return InternalError($"{location}: Creation failed");

                return Created("CreateBeverage", new { beverage }); 
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Update beverage data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beverageDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeverage(int id, [FromBody] BeverageUpdateDTO beverageDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                if (id < 1 || beverageDTO == null || id != beverageDTO.Id)
                    return BadRequest();

                var isExists = await _beverageRepo.isExists(id);
                if (!isExists)
                    return NotFound();

                if (!ModelState.IsValid)
                    return NotFound(ModelState);

                var beverage = _mapper.Map<Beverages>(beverageDTO);
                var isSuccess = await _beverageRepo.Update(beverage);
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
        /// Delete beverage profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBeverage(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                if (id < 1)
                    return BadRequest();

                var isExists = await _beverageRepo.isExists(id);
                if (!isExists)
                    return NotFound();

                var beverage = await _beverageRepo.FindById(id);
                var isSuccess = await _beverageRepo.Delete(beverage);
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
