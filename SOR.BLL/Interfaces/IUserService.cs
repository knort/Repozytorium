using System.Collections.Generic;
using System.Threading.Tasks;
using SOR.Model;
using SOR.Model.ViewModels;

namespace SOR.BLL
{
    public interface IUserService
    {
        Task<(bool result, string message)> CreateAsync(AuthViewModel authViewModel);
        Task<(bool result, string message, JwtViewModel jwtViewModel)> LoginAsync(AuthViewModel authViewModel);
        Task<JwtViewModel> RefreshTokenAsync(JwtViewModel jwtViewModel);
        ICollection<SORUser> GetAllUsers();
    }
}