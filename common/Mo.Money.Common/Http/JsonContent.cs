using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Mo.Money.Common.Http
{
    public class JsonContent<T> : StringContent
    {
        public JsonContent(T model)
            : base(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
        {
            
        }
    }
}