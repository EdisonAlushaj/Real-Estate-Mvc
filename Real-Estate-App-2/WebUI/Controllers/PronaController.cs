using Application.Interface;
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
    public class PronaController : ControllerBase
    {
        private readonly IPronaFeature _pronaFeature;

        public PronaController(IPronaFeature pronaFeature)
        {
            _pronaFeature = pronaFeature;
        }

        [HttpGet("GetByCategory"), Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            try
            {
                var filteredProperties = await _pronaFeature.GetByCategoryAsync(category);
                return Ok(filteredProperties);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProperties()
        {
            var allProperties = await _pronaFeature.GetAllPropertiesAsync();
            return Ok(allProperties);
        }

        [HttpGet("GetFilteredProperties"), Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetFilteredProperties(
            string? location,
            string? category,
            double? maxPrice,
            string? propertyType)
        {
            try
            {
                var filteredProperties = await _pronaFeature.GetFilteredPropertiesAsync(location, category, maxPrice, propertyType);
                return Ok(filteredProperties);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPropertyDetails"), Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetPropertyDetails([FromQuery] int id)
        {
            try
            {
                var property = await _pronaFeature.GetPropertyDetailsAsync(id);
                if (property == null)
                {
                    return NotFound("Property not found.");
                }

                return Ok(property);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching property details: {ex.Message}");
                return StatusCode(500, "Internal server error while fetching property details.");
            }
        }
    }
}
