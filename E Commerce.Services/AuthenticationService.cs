using E_Commerce.Domain.IdentityModule;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.IdentityDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var User = await userManager.FindByEmailAsync(loginDTO.Email);
            if (User == null)
                Error.InvaidCredentials("User.InvaidCredentials");

            var IsPasswordValid = await userManager.CheckPasswordAsync(User, loginDTO.Password);
            if (!IsPasswordValid)
                Error.InvaidCredentials("User.InvaidCredentials");

            return new UserDTO(User.Email, User.DisplayName, "Token");

        }

        public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var User = new ApplicationUser()
            {
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
                UserName = registerDTO.UserName,
                PhoneNumber = registerDTO.PhoneNumber,
            };

            var IdentityResult = await userManager.CreateAsync(User);
            if(IdentityResult.Succeeded)
                return new UserDTO(User.Email, User.DisplayName, "Token");


            return IdentityResult.Errors.Select(E=>Error.Validation(E.Code , E.Description)).ToList();    
        }
    }
}
