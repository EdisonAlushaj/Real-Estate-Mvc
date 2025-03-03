using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactRequestController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactRequestController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<ContactRequest>>> GetAllContactRequests()
        {
            var contactRequests = await _context.ContactRequests.ToListAsync();
            return Ok(contactRequests);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ContactRequest>> Contact([FromBody] ContactRequest contactRequest)
        {
            Console.WriteLine("Received contact request for Name: " + contactRequest.Name);

            var contactResponse = new ContactRequest
            {
                Name = contactRequest.Name,
                Email = contactRequest.Email,
                Message = contactRequest.Message
            };

            _context.ContactRequests.Add(contactResponse);
            await _context.SaveChangesAsync();

            return Ok(contactResponse);
        }
    }
}

