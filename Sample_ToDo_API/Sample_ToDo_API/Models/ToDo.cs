using System;
using System.ComponentModel.DataAnnotations;

namespace Sample_ToDo_API.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public int FK_User { get; set; }
        public int FK_Category { get; set; }
        public string CategoryName { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }

    }
}
