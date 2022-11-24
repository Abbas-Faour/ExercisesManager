using ExercisesManager.API.InputModels;
using ExercisesManager.Identity.Entities;

namespace ExercisesManager.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> IsUserAuthenticated(LoginUserInputModel loginInputModel);
        Task<ApplicationUser> GetUserByName(string username);
        Task<ApplicationUser> GetUserByID(long id);
    }
}