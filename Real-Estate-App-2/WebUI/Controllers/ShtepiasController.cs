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
    public class ShtepiasController : ControllerBase
    {
        private readonly IShtepiaRepository _shtepiaRepository; // Inject the repository
        private readonly AppDbContext _context;

        public ShtepiasController(IShtepiaRepository shtepiaRepository, AppDbContext context)
        {
            _shtepiaRepository = shtepiaRepository;
            _context = context;
        }

        [HttpGet, Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<IEnumerable<Shtepia>>> GetShtepiat()
        {
            try
            {
                //return await _context.Shtepiat.ToListAsync();
                return Ok(await _shtepiaRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpGet("{id}"), Authorize(Policy = "AgentPolicy")]
        public async Task<ActionResult<Shtepia>> GetShtepiaById(int id)
        {
            try
            {
                //var shtepia = await _context.Shtepiat.FindAsync(id);
                var shtepia = await _shtepiaRepository.GetByIdAsync(id);

                if (shtepia == null)
                {
                    return NotFound();
                }

                return shtepia;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpPost, Authorize(Policy = "AgentPolicy")]
        public async Task<ActionResult<Shtepia>> PostShtepiat([FromForm] ShtepiaCreateDto shtepiaDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(); //Keep the dbContext transaction
            try
            {
                var shtepiaFeature = new ShtepiaFeature(_context, _shtepiaRepository); // Pass repository

                var documentFeature = new DocumentFeature(_context);

                var shtepia = await shtepiaFeature.CreateShtepiaAsync(shtepiaDto);

                await documentFeature.CreateDocumentForShtepiaAsync(shtepia.PronaID);

                await transaction.CommitAsync();

                return CreatedAtAction("GetShtepiat", new { id = shtepia.PronaID }, shtepia);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new record: " + ex.Message);
            }
        }

        [HttpPut("{id}"), Authorize(Policy = "AgentPolicy")]
        public async Task<IActionResult> PutShtepia(int id, Shtepia shtepia)
        {
            if (id != shtepia.PronaID)
            {
                return BadRequest();
            }

            //_context.Entry(shtepia).State = EntityState.Modified;

            try
            {
                await _shtepiaRepository.UpdateAsync(shtepia);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShtepiaExists(id))
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
        public async Task<IActionResult> DeleteShtepia(int id)
        {
            try
            {
                //var shtepia = await _context.Shtepiat.FindAsync(id);
                var shtepia = await _shtepiaRepository.GetByIdAsync(id);
                if (shtepia == null)
                {
                    return NotFound();
                }

                //_context.Shtepiat.Remove(shtepia);
                await _shtepiaRepository.DeleteAsync(shtepia);
                //await _context.SaveChangesAsync();

                return Ok(await _shtepiaRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
        }

        private bool ShtepiaExists(int id)
        {
            try
            {
                //return _context.Shtepiat.Any(e => e.PronaID == id);
                var shtepia = _shtepiaRepository.GetByIdAsync(id).Result;
                return shtepia != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
