using System.Net.Http;
using System.Threading.Tasks;

namespace Mo.Money.Common.Http
{
    public interface IHttpClientCreator
    {
        Task<HttpClient> CreateClient();
    }
}