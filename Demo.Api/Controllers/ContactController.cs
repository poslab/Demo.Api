using AutoMapper;
using Demo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DSM = Demo.Services.Model;
using VM = Demo.Api.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Api.Controllers
{
    // Allow Anonymous When Running Unit Test
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : BaseController
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IMapper _mapper;
        private readonly IContactService _contactService;

        public ContactController(
            IMapper mapper,
            IContactService contactService,
            ILogger<ContactController> logger)
        {
            _mapper = mapper;
            _contactService = contactService;
            _logger = logger;
        }


        // GET: api/<ContactController>
        [HttpGet("{userID}")]
        public async Task<IActionResult> Get(int userID)
        {

            if (userID == 0) return BadRequest("Wrong UserID");

            try
            {
                var contact = await _contactService.GetContactAsync(userID);
                var contactVM = _mapper.Map<VM.Contact>(contact);
                return Ok(contactVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, "Error retrieving contact: {0}", new { userID });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string email = null, [FromQuery] string phonenumber = null)
        {
            
            if (email == null && phonenumber == null) return BadRequest("Missing email or phone number");

            if(phonenumber != null && !ValidPhoneNumber(phonenumber)) return BadRequest("Wrong phone number");

            // email would have similar verification function. 

            try
            {
                var contact = await _contactService.SearchContactAsync(email, phonenumber);
                var contactVM = _mapper.Map<VM.Contact>(contact);
                return Ok(contactVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, "Error searching contact: {0},{1}", new { email, phonenumber });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private bool ValidPhoneNumber(string phoneNumber)
        {
            const string phoneRegex = @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$";
            if (Regex.IsMatch(phoneNumber, phoneRegex))
            {
                return true;
            }
            return false;
        }

        [HttpGet("GetContacts")]
        public async Task<IActionResult> GetContacts([FromQuery] string state = null, [FromQuery] string city = null)
        {
            if (state == null && city == null) return BadRequest("Missing state or city");

            // state would be checked at here, such as it should be two letters.

            try
            {
                var contact = await _contactService.GetContactsAsync(state, city);
                var contactVM = _mapper.Map<List<VM.Contact>>(contact);
                return Ok(contactVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, "Error get contacts: {0},{1}", new { state, city });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<ContactController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VM.Contact contact)
        {
            try
            {
                // A validation would happen here to check all the required filed of contact. 

                int res = 0;
                var rdm_contact = _mapper.Map<DSM.Contact>(contact);
                if (rdm_contact.UserID == 0)
                {
                    res = await _contactService.CreateContactAsync(rdm_contact);
                } else
                {
                    res = await _contactService.UpdateContactAsync(rdm_contact);
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, "Error creating contact: {0}", new { contact.Email });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{UserID}")]
        public async Task<IActionResult> Delete(int userID)
        {
            try
            {
                int res = await _contactService.DeleteContactAsync(userID);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, "Error deleting contact: {0}", new { userID });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
