using Application.DTO;
using Application.Features.Sell_Rent;
using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KontrataController : ControllerBase
    {
        private readonly IKontrataRepository _kontrataRepository;
        private readonly AppDbContext _context;

        public KontrataController(IKontrataRepository kontrataRepository, AppDbContext context)
        {
            _kontrataRepository = kontrataRepository;
            _context = context;
        }

        [HttpGet, Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<IEnumerable<Kontrata>>> GetKontrata()
        {
            try
            {
                var kontrata = await _kontrataRepository.GetAllKontrataWithIncludesAsync();

                return Ok(kontrata);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpGet("{id}"), Authorize(Policy = "AgentPolicy")]
        public async Task<ActionResult<Kontrata>> GetKontrata(int id)
        {
            try
            {
                var kontrata = await _kontrataRepository.GetKontrataByIdWithIncludesAsync(id);

                if (kontrata == null)
                {
                    return NotFound("Sale not found.");
                }

                return kontrata;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpPut("{id}"), Authorize(Policy = "AgentPolicy")]
        public async Task<IActionResult> PutKontrata(int id, Kontrata kontrata)
        {
            if (id != kontrata.KontrataId)
            {
                return BadRequest();
            }

            //_context.Entry(kontrata).State = EntityState.Modified;

            try
            {
                await _kontrataRepository.UpdateAsync(kontrata);
                //  await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KontrataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the record. Concurrency issue.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the data.");
            }

            return NoContent();
        }

        [HttpPost, Authorize(Policy = "AgentPolicy")]
        public async Task<ActionResult<Kontrata>> PostKontrata([FromForm] KontrataDto kontrataDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _context.Users.FindAsync(kontrataDto.UserID);
                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                var prona = await _context.Pronas.FindAsync(kontrataDto.PronaID);
                if (prona == null)
                {
                    return NotFound(new { message = "Property not found" });
                }

                var kontrataFeature = new KontrataFeature(_context, _kontrataRepository);
                var kontrat = await kontrataFeature.CreateKontrataAsync(kontrataDto.UserID, kontrataDto.PronaID, kontrataDto.Type);

                return CreatedAtAction("GetKontrata", new { id = kontrat.KontrataId }, kontrat);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new record: " + ex.Message);
            }
        }

        [HttpDelete("{id}"), Authorize(Policy = "AgentPolicy")]
        public async Task<IActionResult> DeleteKontrata(int id)
        {
            try
            {
                //var kontrat = await _context.Kontrata.FindAsync(id);
                var kontrat = await _kontrataRepository.GetByIdAsync(id);
                if (kontrat == null)
                {
                    return NotFound();
                }

                // _context.Kontrata.Remove(kontrat);
                await _kontrataRepository.DeleteAsync(kontrat);

                //await _context.SaveChangesAsync();

                return Ok(await _kontrataRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
        }
        private bool KontrataExists(int id)
        {
            try
            {
                var kontrata = _kontrataRepository.GetByIdAsync(id).Result;
                return kontrata != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
