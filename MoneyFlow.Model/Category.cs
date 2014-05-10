using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MoneyFlow.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [XmlAttribute]
        public string Description { get; set; }
    }
}