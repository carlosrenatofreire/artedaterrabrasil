using LayeredArch.Business.Models.Parameters;
using LayeredArch.Mvc.ViewModels.Parameters;
using System.ComponentModel.DataAnnotations;

namespace LayeredArch.Mvc.ViewModels.Relationships
{
    public class PermissionViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public RoleViewModel Role { get; set; }
        public Module Module { get; set; }
        public IEnumerable<ModuleViewModel> ModuleList { get; set; }
    }
}
