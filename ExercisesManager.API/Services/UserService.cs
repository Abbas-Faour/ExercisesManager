using ExercisesManager.API.InputModels;
using ExercisesManager.API.Services.Interfaces;
using ExercisesManager.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExercisesManager.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> IsUserAuthenticated(LoginUserInputModel loginInputModel)
        {
            var applicationUser = _userManager.Users.
                Include(u => u.UserRoles).
                    ThenInclude(ur => ur.Role).
                FirstOrDefault(u => u.UserName.ToLower() == loginInputModel.Username.ToLower());

            if (applicationUser is not null)
            {
                if (await _userManager.CheckPasswordAsync(applicationUser, loginInputModel.Password))
                    return applicationUser;
            }

            return null;
        }

        public async Task<ApplicationUser> GetUserByName(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.Equals(username));
        }

        public async Task<ApplicationUser> GetUserByID(long id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}