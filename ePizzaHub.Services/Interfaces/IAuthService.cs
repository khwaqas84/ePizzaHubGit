using ePizzaHub.Core.Entities;
using ePizzaHub.Models;


namespace ePizzaHub.Services.Interfaces
{
    public interface IAuthService
    {
        UserModel ValidateUser(string username, string password);
        bool CreateUser(User user ,string roleName);
    }
}
