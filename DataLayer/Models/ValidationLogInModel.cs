using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class LogIn
    {
        // public int Id {get; set;}
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Name { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Password { get; set; }
    }
}