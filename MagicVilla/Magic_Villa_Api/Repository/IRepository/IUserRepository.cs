using Magic_Villa_Api.Models;
using Magic_Villa_Api.Models.Dto;

namespace Magic_Villa_Api.Repository.IRepository
{
    public interface IUserRepository
    {

        bool IsUniqueUser(string username);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task <LocalUser> Register(RegistrationRequestDto registrationRequestDto);



    }
}
