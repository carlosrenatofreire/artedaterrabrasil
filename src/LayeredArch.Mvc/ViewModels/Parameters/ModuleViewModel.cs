using LayeredArch.Mvc.ViewModels.Relationships;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LayeredArch.Mvc.ViewModels.Parameters
{
    public class ModuleViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }
        public string Tag { get; set; }

        [DisplayName("Ativado")]
        public bool Activated { get; set; }

        // * Ef Relationships *//
        public IEnumerable<ClaimViewModel> Claims { get; set; }
        public IEnumerable<PermissionViewModel> Permissions { get; set; }


    }
}
