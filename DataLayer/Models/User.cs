

using System.ComponentModel.DataAnnotations;
using DataLayer.Models.Auth;

namespace DataLayer.Models{

    public class User {
        public int Id {get; set;}
        public string? Name {get; set;}

        [DataType(DataType.EmailAddress)]
        public string? Email {get; set;}

        [DataType(DataType.Password)]
        public string? Password {get; set;}

        // public Role Role {get; set;}
        public List<string>? Role {get;set;}
        public User (int Id,string Name , string Email , string Password , List<string> Role )
        {
            this.Id = Id;
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            this.Role = Role;
        }

    }
}


