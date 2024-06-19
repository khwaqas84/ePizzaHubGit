using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ePizzaHub.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        
        public UserRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
        public bool CreatUser(User user, string roleName)
        {
            try
            {
                
                Role role = _context.Roles.AsNoTracking().Where(r => r.Name == roleName).FirstOrDefault();
                if(role != null) 
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);//BCrypt.Net-Next library
                    user.Roles.Add(role);
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return true;
                
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return false;
        }

        public UserModel ValidateUser(string username, string Password)
        {
            User user=_context.Users.AsNoTracking().Include(r=>r.Roles).Where(u=>u.Email== username).FirstOrDefault();
            if (user != null) 
            {
                bool isValid = BCrypt.Net.BCrypt.Verify(Password, user.Password);
                if (isValid)
                {
                    UserModel userModel = new UserModel()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = user.Roles.Select(r => r.Name).ToArray(),

                    };
                    return userModel;
                }
            
            }
            return null;
        }
    }
}
