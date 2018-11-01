using System.Linq;
using System.Net.Http;

namespace Mo.Money.Common
{
    public static class HttpResponseMessageExtensions
    {
        public static string GetIdFromLocationHeader(this HttpResponseMessage response)
        {
            return response.Headers.Location.Segments.Last();
        }
    }
}