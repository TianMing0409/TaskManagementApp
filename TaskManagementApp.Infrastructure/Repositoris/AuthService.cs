using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.Domain.Entities;
using TaskManagementApp.Infrastructure.Persistence.Contexts;

namespace TaskManagementApp.Infrastructure.Persistence.Repositoris
{
    public class AuthService : IAuthService
    {
        private readonly TaskManagementDbContext _dbContxt;
        private readonly IConfiguration _configuration;

        public AuthService(TaskManagementDbContext dbContext, IConfiguration configuration)
        {
            _dbContxt = dbContext;
            _configuration = configuration; 
        }

        public async Task<bool> RegisterAsync(string email, string password, string firstName, string lastName)
        {
            if (await _dbContxt.Users.AnyAsync(u => u.Email == email))
            {
                return false;    
            }

            string hashedPassword = HashedPassword(password);

            var user = new User
            {
                Email = email,
                Password = hashedPassword,
                FirstName = firstName,
                LastName = lastName
            };

            _dbContxt.Users.Add(user);
            await _dbContxt.SaveChangesAsync();

            return true;
        }

        private string HashedPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        public bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            try
            {
                var user = await _dbContxt.Users.SingleOrDefaultAsync(u => u.Email == email);

                if (user == null || !VerifyPassword(password, user.Password))
                {
                    return null;
                }

                //Generate JWT token
                string token = GenerateJwtToken(user);

                return token;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JWTSettings");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecurityKey"]));

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(jwtSettings["ExpirationInMinutes"])),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
