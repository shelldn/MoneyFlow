using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyFlow.Model
{
    public class Consumption
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}