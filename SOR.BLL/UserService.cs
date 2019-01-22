using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SOR.Model;
using SOR.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SOR.BLL
{
    public class UserService : IUserService
    {
        private readonly UserManager<SORUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<SORUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public ICollection<SORUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<(bool result, string message)> CreateAsync(AuthViewModel authViewModel)
        {
            SORUser user = new SORUser
            {
                Email = authViewModel.Email,
                UserName = authViewModel.Email
            };
            var result = await _userManager.CreateAsync(user, authViewModel.Password);
            if (result.Succeeded)
            {
                var roleResult = await SetUserRolesAsync(user);
                if (roleResult)
                    return (true, "Stworzono użytkownika: " + authViewModel.Email);
                else
                    return (false, "Błąd podczas tworzenia użytkownika, nie udało się nadać ról");
            }
            return (false, "Błąd podczas tworzenia użytkownika: " + string.Join(" ", result.Errors.Select(err => err.Description)));
        }

        private async Task<bool> SetUserRolesAsync(SORUser user)
        {
            IdentityResult result;
            if (IsFirstUser())
            {
                result = await _userManager.AddToRoleAsync(user, "Administrator");
            }
            else
                result = await _userManager.AddToRoleAsync(user, "User");

            return result.Succeeded;
        }

        private bool IsFirstUser()
        {
            return _userManager.Users.ToList().Count == 1;
        }

        public async Task<(bool result, string message, JwtViewModel jwtViewModel)> LoginAsync(AuthViewModel authViewModel)
        {
            var user = await _userManager.FindByEmailAsync(authViewModel.Email);
            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, authViewModel.Password);
                if (result)
                {
                    var jwtViewModel = await GenerateJwtAsync(user);
                    return (result, "Pomyślnie zalogowano", jwtViewModel);
                }
                else
                {
                    return (result, "Podano nieprawidłowe hasło", null);
                }
            }
            return (false, "Niepoprawny email", null);
        }
        public async Task<JwtViewModel> RefreshTokenAsync(JwtViewModel jwtViewModel)
        {
            var jwtSettingsSection = _configuration.GetSection("JwtSettings");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuer = jwtSettingsSection.GetValue(typeof(string), "Issuer").ToString(),
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettingsSection.GetValue(typeof(string), "Secret").ToString()))
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token;
            var validateTokenResult = tokenHandler.ValidateToken(jwtViewModel.Token, tokenValidationParameters, out token);
            var userEmailClaim = validateTokenResult.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
            var jwtSecurityToken = token as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            var user = await _userManager.FindByEmailAsync(userEmailClaim.Value);
            if (user != null)
            {
                return await GenerateJwtAsync(user);
            }
            return null;
        }
        private async Task<JwtViewModel> GenerateJwtAsync(SORUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var jwtSettingsSection = _configuration.GetSection("JwtSettings");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettingsSection.GetValue(typeof(string), "Secret").ToString());
            List<Claim> claims = new List<Claim>();
            if (userRoles.Count > 0)
            {
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            var claimsIdentity = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = jwtSettingsSection.GetValue(typeof(string), "Issuer").ToString(),
                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettingsSection.GetValue(typeof(double), "Expire"))),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtViewModel { Token = tokenHandler.WriteToken(token) };
        }
    }
}
