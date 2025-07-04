using LayeredArch.Mvc.ViewModels.Parameters;
using System.ComponentModel;

namespace LayeredArch.Mvc.ViewModels.Entities
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public Guid? RoleId { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }
        public string Email { get; set; }

        [DisplayName("Data de registro")]
        public DateTime CreatedDate { get; set; }
        public DateTime ChangedDate { get; set; }

        [DisplayName("Excluido")]

        public bool Deleted { get; set; }

        [DisplayName("Ativado")]
        public bool Activated { get; set; }

        /* EF Relations */
        public RoleViewModel Role { get; set; }
        public IEnumerable<RoleViewModel> RoleSelect { get; set; }
    }

}
