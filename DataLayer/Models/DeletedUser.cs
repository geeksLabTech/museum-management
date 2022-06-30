
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Models {
    public class DeletedUser
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public List<string>? Role { get; set; }

        public string DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }

        public DeletedUser() {
        }

        public static DeletedUser Create (IdentityUser user, string deletedBy, List<string> roles) {
            var deletedUser = new DeletedUser {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                DeletedBy = deletedBy,
                DeletedDate = DateTime.Now,
                Role = roles
            };
            
            return deletedUser; 
        }
    }
    
}
