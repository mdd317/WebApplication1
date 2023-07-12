using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class ApiResponse
    {
        [JsonProperty("results")]
        public List<Movie> Results { get; set; }
    }
}
