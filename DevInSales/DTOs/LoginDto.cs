using System.Diagnostics.CodeAnalysis;

namespace DevInSales.DTOs
{
    [ExcludeFromCodeCoverage]
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
