using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sample_ToDo_WebApp.Models
{
    public class ToDo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("fK_User")]
        public int FK_User { get; set; }

        [JsonPropertyName("done")]
        public bool Done { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Update Date")]
        [DataType(DataType.Date)]
        //Format
        public DateTime LastUpdatedDate { get; set; }
    }

    public class ToDo_Dto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int FK_User { get; set; }
        public int FK_Category { get; set; }
        public DateTime RequiredDate { get; set; }

    }
}
