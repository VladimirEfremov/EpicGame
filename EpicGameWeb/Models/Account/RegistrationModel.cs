namespace EpicGameWeb.Models.Account
{
    public class RegistrationModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PasswordHash { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
    }
}