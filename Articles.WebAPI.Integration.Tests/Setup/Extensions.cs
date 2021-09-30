using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Articles.WebAPI.Integration.Tests.Setup
{
    public static class Extensions
    {
        public static async Task<T> GetContentAsync<T>(this HttpContent content)
        {
            using var stream = await content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<T>(stream, Default);
        }

        public static JsonSerializerOptions Default { get; set; } = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
