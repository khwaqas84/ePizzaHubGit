using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ePizzaHub.Services.Implementation
{
    public class AuthService : IAuthService
    {
        IUserRepository _userRepository;
        IConfiguration _config;
        public AuthService(IUserRepository userRepository,IConfiguration config)
        {
            _userRepository=userRepository;
            _config=config;
        }
        public bool CreateUser(User user, string roleName)
        {
           return _userRepository.CreatUser(user, roleName);
        }

        //public UserModel ValidateUser(string username, string password)
        //{
        //    return _userRepository.ValidateUser(username, password);
        //}

        //for token base authentication
        public UserModel ValidateUser(string Email, string Password)
        {
            UserModel model = _userRepository.ValidateUser(Email, Password);
            if (model != null)
            {
                model.Token = GenerateJsonWebToken(model);
            }
            return model;
        }

        private string GenerateJsonWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                             new Claim(JwtRegisteredClaimNames.Sub, userInfo.Name),
                             new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                             };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                             _config["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
