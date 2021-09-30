using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Articles.WebAPI.Application.Resources
{
    public class ErrorDetailsResource
    {
        public ErrorDetailsResource()
        {
        }

        public ErrorDetailsResource(params string[] messages)
        {
            Messages.AddRange(messages);
        }

        /// <summary>
        /// Error messages
        /// </summary>
        [JsonPropertyName("errorMessages")]
        public List<string> Messages { get; set; } = new List<string>();
    }
}