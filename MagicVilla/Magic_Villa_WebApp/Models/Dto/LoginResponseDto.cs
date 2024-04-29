using System.Globalization;

namespace Magic_Villa_WebApp.Models.Dto
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }

    }
}
