using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorWebbApp.Models;

public class DynamicGraphQlResponse
{
    [JsonPropertyName("data")]
    public JsonElement Data { get; set; }
}
