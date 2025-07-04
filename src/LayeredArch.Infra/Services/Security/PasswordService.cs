using LayeredArch.Business.Models.Entities;
using LayeredArch.Infra.Interfaces.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Infra.Services.Security
{

    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<User> _hasher;

        public PasswordService()
        {
            _hasher = new PasswordHasher<User>();
        }

        public string HashPassword(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string providedPassword)
        {
            var result = _hasher.VerifyHashedPassword(user, user.Password, providedPassword);
            return result != PasswordVerificationResult.Failed;
        }
    }
}
