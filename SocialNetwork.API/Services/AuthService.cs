using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Data;
using SocialNetwork.API.Models.Domain;
using SocialNetwork.API.Services.Interfaces;

namespace SocialNetwork.API.Services
{
    public class AuthService : IAuthService
    {
       private readonly SocialNetworkDbContext dataContext;

        public AuthService(SocialNetworkDbContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<Registration>> ListarAsync()
        {
            //Recupera todos os registros da tabela "Registration" da base de dados de forma assicrona
            return await dataContext.Registration.ToListAsync();
        }

        public async Task<Registration> AuthenticateAsync(string email, string password)
        {
            // Verifica se existe um usuário com o email e senha fornecidos
            var user = await dataContext.Registration.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return user;
        }
    }

}


