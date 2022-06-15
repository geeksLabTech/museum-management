using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models{
    public class Museum {
        public int Id {get; set;}

        public string Name {get; set;}

    }
}