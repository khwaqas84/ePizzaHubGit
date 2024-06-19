using ePizzaHub.Core.Entities;
using ePizzaHub.Models;

namespace ePizzaHub.Repositories.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {

        UserModel ValidateUser(string username,string Password);
        bool CreatUser(User user,string role);//signup


    }
}
