

using DataAccessLayer.DTO;
using DataAccessLayer.Identity;

namespace BuisnessLogicLayer.IServiceContracts
{
    public interface IJwtService
    {
        AuthonticationResponse CreateJwtToken(ApplicationUser applicationUser);

        string GenerateRefreshToken();
    }
}
