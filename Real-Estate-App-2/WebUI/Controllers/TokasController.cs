using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Application.DTO;
using Application.Features.Property;
using Application.Interface;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokasController : ControllerBase
    {
        private readonly ITokaRepository _tokaRepository; // Inject the repository
        private readonly AppDbContext _context;

        public TokasController(ITokaRepository tokaRepository, AppDbContext context)
        {
            _tokaRepository = tokaRepository;
            _context = context;
        }

        [HttpGet, Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<IEnumerable<Toka>>> GetTokat()
        {
            try
            {
                //return await _context.Tokat.ToListAsync();
                return Ok(await _tokaRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpGet("{id}"), Authorize(Policy = "AgentPolicy")]
        public async Task<ActionResult<Toka>> GetTokaById(int id)
        {
            try
            {
                //var toka = await _context.Tokat.FindAsync(id);
                var toka = await _tokaRepository.GetByIdAsync(id);

                if (toka == null)
                {
                    return NotFound();
                }

                return toka;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpPost, Authorize(Policy = "AgentPolicy")]
        public async Task<ActionResult<Toka>> PostToka([FromForm] TokaCreateDto tokaDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var tokaFeature = new TokaFeature(_context, _tokaRepository);

                var documentFeature = new DocumentFeature(_context);

                var toka = await tokaFeature.CreateTokaAsync(tokaDto);

                await documentFeature.CreateDocumentForTokaAsync(toka.PronaID);

                await transaction.CommitAsync();

                return CreatedAtAction("GetTokaById", new { id = toka.PronaID }, toka);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new record: " + ex.Message);
            }
        }

        [HttpPut("{id}"), Authorize(Policy = "AgentPolicy")]
        public async Task<IActionResult> PutToka(int id, Toka toka)
        {
            if (id != toka.PronaID)
            {
                return BadRequest();
            }

            //_context.Entry(toka).State = EntityState.Modified;

            try
            {
                await _tokaRepository.UpdateAsync(toka);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TokaExists(id))
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

        [HttpDelete("{id}"), Authorize(Policy = "AgentPolicy")]
        public async Task<IActionResult> DeleteToka(int id)
        {
            try
            {
                //var toka = await _context.Tokat.FindAsync(id);
                var toka = await _tokaRepository.GetByIdAsync(id);
                if (toka == null)
                {
                    return NotFound();
                }

                //_context.Tokat.Remove(toka);
                await _tokaRepository.DeleteAsync(toka);
                //await _context.SaveChangesAsync();

                return Ok(await _tokaRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
        }

        private bool TokaExists(int id)
        {
            try
            {
                //return _context.Tokat.Any(e => e.PronaID == id);
                var toka = _tokaRepository.GetByIdAsync(id).Result;
                return toka != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
