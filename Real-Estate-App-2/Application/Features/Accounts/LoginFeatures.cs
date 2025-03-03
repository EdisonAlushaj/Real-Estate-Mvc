using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Application.DTO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Azure;

namespace Application.Features.Accounts
{
    public class LoginFeatures
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly TokenHelper _tokenHelper;

        public LoginFeatures(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, TokenHelper tokenHelper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _tokenHelper = tokenHelper;
        }

        public async Task<LoginResultDTO> AuthenticateUser(Login loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Username);

            if (user == null)
            {
                return LoginResultDTO.Failure("User not found");
            }

            if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return LoginResultDTO.Failure("Password incorrect.");
            }

            var token = _tokenHelper.GenerateTokenAsync(user);

            var refreshToken = _tokenHelper.GenerateRefreshToken();
            _tokenHelper.SetRefreshToken(user, refreshToken);

            return LoginResultDTO.Success(await token);
        }
    }
}
