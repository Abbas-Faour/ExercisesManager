using ExercisesManager.API.InputModels;
using ExercisesManager.API.Services.Interfaces;
using ExercisesManager.Data.Entites;
using ExercisesManager.Data.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExercisesManager.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersService(UserManager<ApplicationUser> userManager)
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

        public async Task<ApplicationUser> GetUserByName(string username, CancellationToken token)
        {
            
           return await _userManager.Users.Include(x => x.UserExercises).FirstOrDefaultAsync(x => x.UserName.Equals(username),token);
        }


    }
}