using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Market.Simulator.Models.Quotes;
using Market.Simulator.Server.Common;
using Market.Simulator.Server.Common.Services;
using Market.Simulator.Server.Quotes.Services;

namespace Market.Simulator.Server.Quotes.Publishing
{
    public interface IQuotesPublisher
    {
        Task GenerateAndPublishQuotes();
    }

    public class QuotesPublisher : IQuotesPublisher
    {
        private readonly IQuoteGenerator _quoteGenerator;
        private readonly IContext _context;
        private readonly IMarketEventPublisher _eventPublisher;
        private readonly IMapper _mapper;

        public QuotesPublisher(
            IQuoteGenerator quoteGenerator, 
            IContext context, 
            IMarketEventPublisher eventPublisher, 
            IMapper mapper)
        {
            _quoteGenerator = quoteGenerator;
            _context = context;
            _eventPublisher = eventPublisher;
            _mapper = mapper;
        }
        
        public async Task GenerateAndPublishQuotes()
        {
            var quotes = await _quoteGenerator.GenerateQuotes();
            _context.AddRange(quotes);
            await _context.SaveAsync();

            var publishTasks = quotes
                .Select(_mapper.Map<QuoteModel>)
                .Select(q => _eventPublisher.Publish(q));
            await Task.WhenAll(publishTasks);
        }
    }
}