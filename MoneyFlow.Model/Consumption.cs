﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyFlow.Model
{
    public class Consumption
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
    }
}