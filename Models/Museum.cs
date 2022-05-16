using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace museum_management.Models{
    public class Museum {
        public int Id {get; set;}

        public string Name {get; set;}
    }
}