using ArteDaTerraBrasil.Business.Models.Parameters;
using ArteDaTerraBrasil.Business.Models.Shareds;

namespace ArteDaTerraBrasil.Business.Models.Entities
{
    public class User : Entity
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ChangedDate { get; set; }
        public bool PasswordChanged { get; set; }
        public bool Deleted { get; set; }
        public bool Activated { get; set; }


        /* EF Relations */
        public Role Role { get; set; }
    }
}
