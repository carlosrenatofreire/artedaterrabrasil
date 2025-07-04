using AutoMapper;
using LayeredArch.Business.Models.Auxiliares;
using LayeredArch.Business.Models.Entities;
using LayeredArch.Business.Models.Parameters;
using LayeredArch.Mvc.ViewModels.Entities;
using LayeredArch.Mvc.ViewModels.Parameters;
using LayeredArch.Mvc.ViewModels.Shareds;

namespace LayeredArch.Mvc.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<Module, ModuleViewModel>().ReverseMap();
            CreateMap<AuditLog, AuditLogViewModel>().ReverseMap();
            CreateMap<Claim, ClaimViewModel>().ReverseMap();

        }
    }
}
