

using System.ComponentModel.DataAnnotations;
using DataLayer.Models.Auth;

namespace DataLayer.Models{

    public class User {
        public string Id {get; set;}
        public string? Name {get; set;}

        [DataType(DataType.EmailAddress)]
        public string? Email {get; set;}

        [DataType(DataType.Password)]
        public string? Password {get; set;}

        // public Role Role {get; set;}
        public List<string>? Role {get;set;}
        
        

    }
}


