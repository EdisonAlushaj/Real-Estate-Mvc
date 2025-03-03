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
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentRepository _apartmentRepository; // Inject repository
        private readonly AppDbContext _context; // Inject AppDbContext

        public ApartmentsController(IApartmentRepository apartmentRepository, AppDbContext context)
        {
            _apartmentRepository = apartmentRepository;
            _context = context;
        }

        [HttpGet, Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetApartments()
        {
            try
            {
                //return await _context.Apartments.ToListAsync();
                return Ok(await _apartmentRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpGet("{id}"), Authorize(Policy = "AgentPolicy")]
        public async Task<ActionResult<Apartment>> GetApartmentById(int id)
        {
            try
            {
                //var apartment = await _context.Apartments.FindAsync(id);
                var apartment = await _apartmentRepository.GetByIdAsync(id);

                if (apartment == null)
                {
                    return NotFound();
                }

                return apartment;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpPost, Authorize(Policy = "AgentPolicy")]
        public async Task<ActionResult<Apartment>> PostApartment([FromForm] ApartmentCreateDto apartmentDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(); //Keep the dbContext transaction
            try
            {
                var apartmentFeature = new ApartmentFeature(_context, _apartmentRepository); //Pass the repository

                var documentFeature = new DocumentFeature(_context);

                var apartment = await apartmentFeature.CreateApartmentAsync(apartmentDto);

                await documentFeature.CreateDocumentForApartmentAsync(apartment.PronaID);

                await transaction.CommitAsync();

                return CreatedAtAction("GetApartmentById", new { id = apartment.PronaID }, apartment);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new record: " + ex.Message);
            }
        }

        [HttpPut("{id}"), Authorize(Policy = "AgentPolicy")]
        public async Task<IActionResult> PutApartment(int id, Apartment apartment)
        {
            if (id != apartment.PronaID)
            {
                return BadRequest();
            }

            //_context.Entry(apartment).State = EntityState.Modified;

            try
            {
                await _apartmentRepository.UpdateAsync(apartment);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApartmentExists(id))
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
        public async Task<IActionResult> DeleteApartment(int id)
        {
            try
            {
                //var apartment = await _context.Apartments.FindAsync(id);
                var apartment = await _apartmentRepository.GetByIdAsync(id);
                if (apartment == null)
                {
                    return NotFound();
                }

                //_context.Apartments.Remove(apartment);
                await _apartmentRepository.DeleteAsync(apartment);

                //await _context.SaveChangesAsync();

                return Ok(await _apartmentRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
        }

        private bool ApartmentExists(int id)
        {
            try
            {
                //return _context.Apartments.Any(e => e.PronaID == id);
                var apartment = _apartmentRepository.GetByIdAsync(id).Result;
                return apartment != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
