using System;
using System.Web;

namespace EpicGameWeb.Models.Account
{
    public struct AuthResponse
    {
        public bool IsCorrect { get; set; }

        public AuthResponse(bool isCorrect)
        {
            IsCorrect = isCorrect;
        }
    }
}