using System.ComponentModel.DataAnnotations;

namespace Backend.src.Models
{
    public class Category:BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}