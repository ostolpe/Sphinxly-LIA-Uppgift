using System.Text.Json.Serialization;

namespace Easyweb.Site.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
