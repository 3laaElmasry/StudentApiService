using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        public string? PersonName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDateTime { get; set; }

    }
}
