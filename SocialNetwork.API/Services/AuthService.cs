using SocialNetwork.API.Models.Domain;
using SocialNetwork.API.Services.Interfaces;

namespace SocialNetwork.API.Services
{
    public class AuthService : IAuthService
    {
        private List<Registration> users; // Simula uma lista de usuários registrados

        public AuthService()
        {
            // Inicialize a lista de usuários (substitua por sua lógica de persistência)
            users = new List<Registration>
        {
            new Registration {  Name = "Amancio", Email = "amancioubissejrr@gmail.com", Password = "testando", PhoneNo = "848122283" },
        };
        }

        public Registration Authenticate(string email, string password)
        {
            var user = users.SingleOrDefault(u => u.Email == email && u.Password == password);

            return user;
        }
    }

}
