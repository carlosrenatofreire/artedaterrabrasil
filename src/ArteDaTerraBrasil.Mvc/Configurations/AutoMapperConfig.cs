using ArteDaTerraBrasil.Business.Models.Auxiliares;
using ArteDaTerraBrasil.Business.Models.Entities;
using ArteDaTerraBrasil.Business.Models.Parameters;
using ArteDaTerraBrasil.Mvc.ViewModels.Entities;
using ArteDaTerraBrasil.Mvc.ViewModels.Parameters;
using ArteDaTerraBrasil.Mvc.ViewModels.Shareds;
using AutoMapper;

namespace ArteDaTerraBrasil.Mvc.Configurations
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
