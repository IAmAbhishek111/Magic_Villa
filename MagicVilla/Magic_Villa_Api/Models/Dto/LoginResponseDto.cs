using System.Globalization;

namespace Magic_Villa_Api.Models.Dto
{
    public class LoginResponseDto
    {
        public LocalUser User { get; set; } 
        public string Token { get; set; }

    }
}
