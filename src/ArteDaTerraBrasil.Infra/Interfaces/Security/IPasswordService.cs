using ArteDaTerraBrasil.Business.Models.Entities;

namespace ArteDaTerraBrasil.Infra.Interfaces.Security
{
    public interface IPasswordService
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string providedPassword);
    }
}
