using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LayeredArch.Mvc.ViewModels.Parameters
{
    public class ClaimViewModel
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

        [Required(ErrorMessage = "O módulo é obrigatório")]
        public Guid? ModuleId { get; set; }
        public bool hasPermission { get; set; } = false;

        // * Ef Relationships *//
        public ModuleViewModel Module { get; set; }
        public IEnumerable<ModuleViewModel> ModuleSelect { get; set; }
    }
}
