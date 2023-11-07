using SocialNetwork.API.Models.Domain;

namespace SocialNetwork.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Registration> AuthenticateAsync(string email, string password);
    }
}
