using SocialNetwork.API.Models.Domain;

namespace SocialNetwork.API.Services.Interfaces
{
    public interface IAuthService
    {
        Registration Authenticate(string email, string password);
    }
}
