

using System.ComponentModel.DataAnnotations;

namespace museum_management.Models{

    public class User {
        public string Name {get; set;}

        [DataType(DataType.EmailAddress)]
        public string Email {get; set;}

        [DataType(DataType.Password)]
        public string Password {get; set;}

        public Role Role {get; set;}
    }

    public enum Role {
        Director,
        ChiefRestorer,
        CatalogKeeper,
        Guest
    }
}


