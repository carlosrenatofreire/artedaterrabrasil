using LayeredArch.Mvc.ViewModels.Entities;
using System.ComponentModel;

namespace LayeredArch.Mvc.ViewModels.Parameters
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Perfil")]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }
        public Guid? SupervisorId { get; set; }
        public string Tag { get; set; }

        [DisplayName("Ativado")]
        public bool Activated { get; set; }

        /* EF Relations */
        public IEnumerable<UserViewModel> Users { get; set; }
        public UserViewModel Supervisor { get; set; }
        public IEnumerable<UserViewModel> SupervisorSelect { get; set; }
    }
}
