﻿using API.Constants;
using API.DbContexts;
using API.Dtos.Users;
using API.Entities;
using API.Exceptions;
using API.Services.Abstract;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Utils;

namespace API.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private ILogger? logger;

        public UserService(
          
            ApplicationDbContext dbContext,
            IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public void Create(CreateUserDto input)
        {
            if (_dbContext.Users.Any(u => u.Username == input.Username))
            {
                throw new UserFriendlyException($"Tên tài khoản \"{input.Username}\" đã tồn tại");
            }
            _dbContext.Users.Add(new User
            {
                Username = input.Username,
                Password = Utils.PasswordHasher.HashPassword(input.Password),
                UserType = input.UserType
            });
            _dbContext.SaveChanges();
        }

        public string Login(LoginDto input)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == input.Username);
            if (user == null)
            {
                throw new UserFriendlyException($"Tài khoản \"{input.Username}\" không tồn tại");
            }

            if (!Utils.PasswordHasher.VerifyPassword(input.Password, user.Password))
            {
                throw new UserFriendlyException($"Mật khẩu không chính xác");
            }
            else
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, user.Username),
                    new Claim(CustomClaimTypes.UserType, user.UserType.ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddSeconds(_configuration.GetValue<int>("JWT:Expires")),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }
}
