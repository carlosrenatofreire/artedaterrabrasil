using ArteDaTerraBrasil.Business.Models.Relationships;
using ArteDaTerraBrasil.Business.Models.Shareds;

namespace ArteDaTerraBrasil.Business.Models.Parameters
{
    public class Claim : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public bool Activated { get; set; }
        public Guid ModuleId { get; set; }

        // * Ef Relationships *//
        public Module Module { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }

}
