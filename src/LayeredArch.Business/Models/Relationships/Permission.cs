using LayeredArch.Business.Models.Parameters;
using LayeredArch.Business.Models.Shareds;

namespace LayeredArch.Business.Models.Relationships
{
    public class Permission : Entity
    {
        public Guid RoleId { get; set; }
        public Guid ModuleId { get; set; }
        public Guid ClaimId { get; set; }

        // * Ef Relationships *//
        public Role Role { get; set; }
        public Module Module { get; set; }
        public Claim Claim { get; set; }
    }
}
