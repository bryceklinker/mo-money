using System;
using System.Linq;
using System.Threading.Tasks;
using Market.Simulator.Server.Common;
using Market.Simulator.Server.Companies.Entities;
using Market.Simulator.Server.Quotes.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Simulator.Server.Quotes.Services
{
    public interface IQuoteGenerator
    {
        Task<Quote[]> GenerateQuotes();
    }

    public class QuoteGenerator : IQuoteGenerator
    {
        private readonly Random _random;
        private readonly IContext _context;

        public QuoteGenerator(IContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task<Quote[]> GenerateQuotes()
        {
            return await _context.GetAll<Company>()
                .Select(c => new Quote
                {
                    Timestamp = DateTimeOffset.UtcNow,
                    Price = _random.NextDecimal(),
                    Company = c
                }).ToArrayAsync();
        }
    }
}