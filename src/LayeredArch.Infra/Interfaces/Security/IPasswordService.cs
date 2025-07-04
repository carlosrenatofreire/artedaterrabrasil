using LayeredArch.Business.Models.Entities;

namespace LayeredArch.Infra.Interfaces.Security
{
    public interface IPasswordService
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string providedPassword);
    }
}
