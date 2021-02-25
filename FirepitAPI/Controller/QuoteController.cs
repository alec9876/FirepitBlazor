using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteRepository _quoteRepo;
        private readonly IPersonRepository _personRepo;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private IHttpContextAccessor _http;

        public QuoteController(IQuoteRepository quoteRepo, IMapper mapper, ILoggerService logger, 
            IPersonRepository personRepo, IHttpContextAccessor http)
        {
            _quoteRepo = quoteRepo;
            _mapper = mapper;
            _logger = logger;
            _personRepo = personRepo;
            _http = http;
        }

        /// <summary>
        /// Returns all quotes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetQuotes()
        {
            var location = GetControllerActionNames();
            try
            {
                var quotes = await _quoteRepo.FindAll();
                var response = _mapper.Map<IList<QuoteDTO>>(quotes);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Get single quote by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuote(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                var quotes = await _quoteRepo.FindById(id);
                if (quotes == null)
                    return NotFound();

                var response = _mapper.Map<QuoteDTO>(quotes);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Create a new quote
        /// </summary>
        /// <param name="quoteDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateQuote([FromBody] QuoteCreateDTO quoteDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                if (quoteDTO == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var getUser = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = _personRepo.FindByEmail(getUser);

                quoteDTO.PersonId = userId.Result.Id;

                var quote = _mapper.Map<Quotes>(quoteDTO);
                var isSuccess = await _quoteRepo.Create(quote);
                if (!isSuccess)
                    return InternalError($"{location}: Creation failed");

                return Created("CreateQuote", new { quote });
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Update quote
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quoteDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuote(int id, [FromBody] QuoteUpdateDTO quoteDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                if (id < 1 || quoteDTO == null || id != quoteDTO.Id)
                    return BadRequest();

                var isExists = await _quoteRepo.isExists(id);
                if (!isExists)
                    return NotFound();

                if (!ModelState.IsValid)
                    return NotFound(ModelState);

                var quote = _mapper.Map<Quotes>(quoteDTO);
                var isSuccess = await _quoteRepo.Update(quote);
                if (!isSuccess)
                    return InternalError($"{location}: Update failed for record with id: {id}");

                return NoContent();
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Delete quote profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                if (id < 1)
                    return BadRequest();

                var isExists = await _quoteRepo.isExists(id);
                if (!isExists)
                    return NotFound();

                var quote = await _quoteRepo.FindById(id);
                var isSuccess = await _quoteRepo.Delete(quote);
                if (!isSuccess)
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
