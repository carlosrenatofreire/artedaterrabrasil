using ArteDaTerraBrasil.Business.Models.Relationships;
using ArteDaTerraBrasil.Business.Models.Shareds;

namespace ArteDaTerraBrasil.Business.Models.Parameters
{
    public class Module : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public bool Deleted { get; set; }
        public bool Activated { get; set; }

        // * Ef Relationships *//
        public IEnumerable<Claim> Claims { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }
}
