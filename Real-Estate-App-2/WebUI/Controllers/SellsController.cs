using Application.Features.Sell_Rent;
using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellsController : ControllerBase
    {
        private readonly ISellRepository _sellRepository; // Inject the repository
        private readonly AppDbContext _context;

        public SellsController(ISellRepository sellRepository, AppDbContext context)
        {
            _sellRepository = sellRepository;
            _context = context;
        }

        [HttpGet, Authorize(Policy = "AgentPolicy")]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _sellRepository.GetAllSellsWithIncludesAsync();

            return Ok(sales);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetSaleByUserId(string id)
        {
            var sells = await _sellRepository.GetSellsByUserIdAsync(id);

            if (sells == null || !sells.Any())
            {
                return NotFound("No sells found for the given user.");
            }

            return Ok(sells);
        }

        [HttpPost, Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> CreateSell([FromQuery] string userId, [FromQuery] int pronaId, [FromQuery] DateTime saleDate,
             [FromQuery] double salePrice, [FromQuery] string paymentMethod)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) || pronaId <= 0)
                {
                    return BadRequest("Invalid userId or pronaId.");
                }
                var sale = new Sell
                {
                    SaleDate = saleDate,
                    SalePrice = salePrice,
                    Commision = 0.10 * salePrice,
                    PaymentMethod = paymentMethod,
                    UserID = userId,
                    PronaID = pronaId
                };

                var user = await _context.Users.FindAsync(userId);
                var property = await _context.Pronas.FindAsync(pronaId);

                if (user == null || property == null)
                {
                    return BadRequest("User or Property not found.");
                }

                sale.Users = user;
                sale.Pronat = property;

                if (!TryValidateModel(sale))
                {
                    return BadRequest(ModelState);
                }

                Console.WriteLine($"CreateSell called with: userId={userId}, pronaId={pronaId}, sale={JsonConvert.SerializeObject(sale)}");

                var sellFeature = new SellFeature(_context, _sellRepository); // Inject the repository
                var kontrataFeature = new KontrataFeature(_context);

                var createdSell = await sellFeature.CreateSellAsync(userId, pronaId, sale);
                var createdKontrata = await kontrataFeature.CreateKontrataSellAsync(userId, pronaId);

                return CreatedAtAction(nameof(GetSaleByUserId), new { id = createdSell.SellID }, createdSell);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateSell: {ex.Message}");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}"), Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> UpdateSale(int id, [FromBody] Sell sale)
        {
            if (id != sale.SellID)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_context.Entry(sale).State = EntityState.Modified;
            try
            {
                await _sellRepository.UpdateAsync(sale);
                //await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
                {
                    return NotFound("Sale not found.");
                }

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}"), Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await _sellRepository.GetByIdAsync(id);

            if (sale == null)
            {
                return NotFound("Sale not found.");
            }

            await _sellRepository.DeleteAsync(sale);


            return NoContent();
        }

        private bool SaleExists(int id)
        {
            var sell = _sellRepository.GetByIdAsync(id).Result;
            return sell != null;
        }
    }
}