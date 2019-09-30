using System;
using System.Web;

namespace EpicGameWeb.Models.Account
{
    public struct AuthResponse
    {
        public bool IsAuthorized { get; set; }
        public string Email { get; set; }

        public AuthResponse(bool isAuthorized, string email)
        {
            IsAuthorized = isAuthorized;
            Email = email;
        }
    }
}