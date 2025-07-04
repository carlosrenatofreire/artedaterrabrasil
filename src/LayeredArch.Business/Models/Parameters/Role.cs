using LayeredArch.Business.Models.Entities;
using LayeredArch.Business.Models.Relationships;
using LayeredArch.Business.Models.Shareds;

namespace LayeredArch.Business.Models.Parameters
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public Guid? SupervisorId { get; set; }
        public bool Deleted { get; set; }
        public bool Activated { get; set; }

        /* EF Relations */
        public IEnumerable<User> Users { get; set; }
        public User Supervisor { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }

    }
}
