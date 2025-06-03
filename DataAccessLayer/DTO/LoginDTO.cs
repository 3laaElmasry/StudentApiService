
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "The Email cann't be blank")]
        [EmailAddress(ErrorMessage = "The Email must be valid with email constraints")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "The Password cann't be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
