using Application.Interface;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Property
{
    public class DocumentFeature
    {
        private readonly IAppDbContext _context;
        private readonly IDocumentRepository _documentRepository;


        public DocumentFeature(IAppDbContext context, IDocumentRepository documentRepository)
        {
            _context = context;
            _documentRepository = documentRepository;
        }
        public async Task<Documents> CreateDocumentForApartmentAsync(int apartmentId)
        {
            var document = new Documents
            {
                Type = "Apartment Document",
                PronaID = apartmentId,
                CreatedData = DateTime.Now,
                ExpiorationDate = DateTime.Now.AddYears(1)
            };

            //_context.Documents.Add(document);
            //await _context.SaveChangesAsync();
            await _documentRepository.AddAsync(document);

            return document;
        }

        public async Task<Documents> CreateDocumentForShtepiaAsync(int shtepiaId)
        {
            var document = new Documents
            {
                Type = "Shtepia Document",
                PronaID = shtepiaId,
                CreatedData = DateTime.Now,
                ExpiorationDate = DateTime.Now.AddYears(1)
            };

            // _context.Documents.Add(document);
            // await _context.SaveChangesAsync();
            await _documentRepository.AddAsync(document);

            return document;
        }

        public async Task<Documents> CreateDocumentForTokaAsync(int tokaId)
        {
            var document = new Documents
            {
                Type = "Toka Document",
                PronaID = tokaId,
                CreatedData = DateTime.Now,
                ExpiorationDate = DateTime.Now.AddYears(1)
            };

            //_context.Documents.Add(document);
            //await _context.SaveChangesAsync();
            await _documentRepository.AddAsync(document);

            return document;
        }
    }
}
