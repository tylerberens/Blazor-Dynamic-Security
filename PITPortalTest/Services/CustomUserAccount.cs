using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;


public class CustomUserAccount : RemoteUserAccount
{
    [JsonPropertyName("aud")]
    public string Aud { get; set; }

    [JsonPropertyName("roles")]
    public string[] Roles { get; set; } = Array.Empty<string>();

    [JsonPropertyName("wids")]
    public string[] Wids { get; set; } = Array.Empty<string>();

    [JsonPropertyName("groups")]
    public string[] Groups { get; set; } = Array.Empty<string>();

    [JsonPropertyName("oid")]
    public string? Oid { get; set; }
}