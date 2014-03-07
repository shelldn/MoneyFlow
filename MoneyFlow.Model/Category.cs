using System.ComponentModel.DataAnnotations;

namespace MoneyFlow.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
    }
}