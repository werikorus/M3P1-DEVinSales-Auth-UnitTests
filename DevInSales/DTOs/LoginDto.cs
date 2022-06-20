﻿using System.Diagnostics.CodeAnalysis;

namespace DevInSales.DTOs
{
    [ExcludeFromCodeCoverage]
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginDto(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
