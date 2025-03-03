using Application.Interface;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Sell_Rent
{
    public class KontrataFeature
    {
        private readonly IAppDbContext _context;
        private readonly IKontrataRepository _kontrataRepository;


        public KontrataFeature(IAppDbContext context, IKontrataRepository kontrataRepository)
        {
            _context = context;
            _kontrataRepository = kontrataRepository;
        }
        public async Task<Kontrata> CreateKontrataAsync(string userId, int pronaId, string type)
        {
            var kontrata = new Kontrata
            {
                PronaID = pronaId,
                UserID = userId,
                Type = type
            };

            //_context.Kontrata.Add(kontrata);
            //await _context.SaveChangesAsync();
            await _kontrataRepository.AddAsync(kontrata);

            return kontrata;
        }

        public async Task<Kontrata> CreateKontrataSellAsync(string userId, int pronaId)
        {
            var kontrata = new Kontrata
            {
                PronaID = pronaId,
                UserID = userId,
                Type = "Sell Contract"
            };

            //_context.Kontrata.Add(kontrata);
            //await _context.SaveChangesAsync();
            await _kontrataRepository.AddAsync(kontrata);

            return kontrata;
        }

        public async Task<Kontrata> CreateKontrataRentAsync(string userId, int pronaId)
        {
            var kontrata = new Kontrata
            {
                PronaID = pronaId,
                UserID = userId,
                Type = "Rent Contract"
            };

            //_context.Kontrata.Add(kontrata);
            //await _context.SaveChangesAsync();
            await _kontrataRepository.AddAsync(kontrata);

            return kontrata;
        }
    }
}
