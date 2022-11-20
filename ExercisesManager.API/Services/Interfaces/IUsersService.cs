using ExercisesManager.API.InputModels;
using ExercisesManager.Data.Entites.Identity;

namespace ExercisesManager.API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ApplicationUser> IsUserAuthenticated(LoginUserInputModel loginInputModel);
        Task<ApplicationUser> GetUserByName(string username);
    }
}