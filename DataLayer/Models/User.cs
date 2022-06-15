

using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models{

    public class User {
        public int Id {get; set;}
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


