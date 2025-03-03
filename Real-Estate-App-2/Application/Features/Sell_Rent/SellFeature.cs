using Application.Interface;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Sell_Rent
{
    public class SellFeature
    {
        private readonly ISellRepository _sellRepository;  // Inject the repository
        private readonly IAppDbContext _context;  // Need AppDbContext for User and Prona

        public SellFeature(IAppDbContext context, ISellRepository sellRepository)
        {
            _context = context;
            _sellRepository = sellRepository;
        }

        public async Task<Sell> CreateSellAsync(string userId, int pronaId, Sell sell)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            var prona = await _context.Pronas.FindAsync(pronaId);
            if (prona == null)
                throw new Exception("Property not found.");

            if (prona.Status != "Available")
                throw new Exception("Property is not Available.");

            if (prona.Type != "Sell")
                throw new Exception("Property is not for Sale, it is for Rent.");

            sell.UserID = userId;
            sell.PronaID = pronaId;
            sell.Users = user;
            sell.Pronat = prona;

            prona.Status = "Unavailable";
            var commissionRate = 0.10;
            sell.Commision = prona.Price * commissionRate;

            //_context.Sells.Add(sell);
            //await _context.SaveChangesAsync();
            await _sellRepository.AddAsync(sell);

            return sell;
        }
    }
}
