using Magic_Villa_Api.Data;
using Magic_Villa_Api.Models;
using Magic_Villa_Api.Models.Dto;
using Magic_Villa_Api.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace Magic_Villa_Api.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _db;
        private string SecretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            SecretKey = configuration.GetValue<string>(
                "ApiSettings:Secret");
        }
        public bool IsUniqueUser(string username)
        {
            //retrieving the user :
            var user = _db.LocalUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName && u.Password == loginRequestDto.Password);

            if (user == null)
            {
                return new LoginResponseDto() { 
                    Token = "",
                    User = null
                };

            }

            // if user was found generate the JWT Token

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name , user.Id.ToString()),
                    new Claim(ClaimTypes.Role , user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            
                LoginResponseDto loginResponseDto = new LoginResponseDto()
                {
                    Token = tokenHandler.WriteToken(token),
                    User = user
                };
                return loginResponseDto;
        }
        public async Task<LocalUser> Register(RegistrationRequestDto registrationRequestDto)
        {
            //Adding new User 

            LocalUser user = new ()
            {
                UserName = registrationRequestDto.UserName,
                Password = registrationRequestDto.Password,
                Name = registrationRequestDto.Name,
                Role = registrationRequestDto.Role,
            };

            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;


        }
    }
}
