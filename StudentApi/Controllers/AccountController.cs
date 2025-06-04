using StudentApi.Controllers;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.Identity;
using DataAccessLayer.DTO;
using BuisnessLogicLayer.IServiceContracts;

namespace StudentApi.Controllers
{
    [AllowAnonymous]

    
    public class AccountController : CutstomControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ApplicationUser>> PostRegister(RegisterDTO registerDTO)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = String.Join(" , ",ModelState.Values.SelectMany(e => e.Errors)
                    .Select(m => m.ErrorMessage));

                return Problem(errorMessage,statusCode: StatusCodes.Status400BadRequest);
            }

            ApplicationUser applicationUser = new ApplicationUser
            {
                PersonName = registerDTO.PersonName,
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(applicationUser,registerDTO.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(applicationUser,false);

                AuthonticationResponse authoneticationResponse =
                      _jwtService.CreateJwtToken(applicationUser);

                //Add RefreshToken to user
                applicationUser.RefreshToken =  authoneticationResponse.RefreshToken;

                //Add RefreshToken Expiration to User
                applicationUser.RefreshTokenExpirationDateTime = 
                    authoneticationResponse.RefreshTokenExpirationDateTime;

                await _userManager.UpdateAsync(applicationUser);


                return Ok(authoneticationResponse);
            }
            else
            {
                string errorMessage = string
                    .Join(" , ", result.Errors.Select(e => e.Description));
                return Problem(errorMessage, statusCode: StatusCodes.Status400BadRequest);

            }
        }

        [HttpGet]
        public async Task<ActionResult> IsEmailAlreadyExist(string email)

        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if(user is null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUser>> PostLogin(LoginDTO loginDTO)
        {
            if(ModelState.IsValid is false)
            {
                string errorMessage = String.Join(" , ", 
                    ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }



            var result = await _signInManager
                .PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);

            if (result.Succeeded)
            {
                ApplicationUser? applicationUser = await _userManager.FindByEmailAsync (loginDTO.Email);
                if (applicationUser is not null)
                {
                    AuthonticationResponse authoneticationResponse = 
                        _jwtService.CreateJwtToken(applicationUser);

                    //Add RefreshToken Expiration to User
                    applicationUser.RefreshTokenExpirationDateTime =
                        authoneticationResponse.RefreshTokenExpirationDateTime;

                    await _userManager.UpdateAsync(applicationUser);

                    return Ok(authoneticationResponse);
                }
            }

            return Problem("Invaild Email or Password");

        }


        [HttpGet]
        public async Task<ActionResult> GetLogOut()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }
    }
}
