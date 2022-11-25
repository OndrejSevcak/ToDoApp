using System;
using System.Text.Json.Serialization;

namespace Sample_ToDo_API.Models
{
    public class NewTodoDTO
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("fkUser")]
        public int FK_User { get; set; }

        [JsonPropertyName("fkCategory")]
        public int FK_Category { get; set; }

        [JsonPropertyName("targetDate")]
        public DateTime TargetDate { get; set; }
    }
}
