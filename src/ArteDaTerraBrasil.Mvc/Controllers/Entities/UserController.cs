using ArteDaTerraBrasil.Business.Interfaces.Auxiliares;
using ArteDaTerraBrasil.Business.Interfaces.Entities;
using ArteDaTerraBrasil.Business.Interfaces.Parameters;
using ArteDaTerraBrasil.Business.Interfaces.Shareds;
using ArteDaTerraBrasil.Mvc.Controllers.Shared;
using ArteDaTerraBrasil.Mvc.ViewModels.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArteDaTerraBrasil.Mvc.Controllers.Entities
{
    public class UserController : BaseController
    {

        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;


        public UserController(INotifierService notifier, IAccessorService accessorService,
                                                                IAuditLogService auditLogService,
                                                                IUserRepository userRepository,
                                                                IUserService userService,
                                                                IRoleRepository roleRepository,
                                                                IMapper mapper
                                                                ) : base(notifier, accessorService, auditLogService)
        {

            _userRepository = userRepository;
            _userService = userService;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("User-List")]
        public async Task<IActionResult> Index()
        {
            var users = _mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetAllWithRole());
            return View(users);
        }
    }
}
