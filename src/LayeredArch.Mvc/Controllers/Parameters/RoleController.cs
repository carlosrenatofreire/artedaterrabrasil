using AutoMapper;
using LayeredArch.Business.Enums;
using LayeredArch.Business.Interfaces.Auxiliares;
using LayeredArch.Business.Interfaces.Entities;
using LayeredArch.Business.Interfaces.Parameters;
using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Models.Parameters;
using LayeredArch.Helper.StaticVariables;
using LayeredArch.Mvc.Controllers.Shared;
using LayeredArch.Mvc.ViewModels.Entities;
using LayeredArch.Mvc.ViewModels.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayeredArch.Mvc.Controllers.Parameters
{
    public class RoleController : BaseController
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleService _roleService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public RoleController(INotifierService notifier, IAccessorService accessorService,
                                                                IAuditLogService auditLogService,
                                                                IRoleRepository roleRepository,
                                                                IRoleService roleService,
                                                                IUserRepository userRepository,
                                                                IMapper mapper
                                                                ) : base(notifier, accessorService, auditLogService)
        {

            _roleRepository = roleRepository;
            _roleService = roleService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("Role-List")]
        public async Task<IActionResult> Index()
        {
            var roles = _mapper.Map<IEnumerable<RoleViewModel>>(await _roleRepository.GetAllAsync());
            return View(roles);
        }

        [Route("Role-Details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var roleViewModel = await GetRoleAndDetailsById(id);

            if (roleViewModel == null)
                return Json(new { success = false, error = "Perfil não encontrado!" });

            return View(roleViewModel);
        }

        [Route("Role-Create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roleViewModel = await GetFullRoleViewModel(new RoleViewModel());

            return View(roleViewModel);
        }

        [Route("Role-Create")]
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {

            try
            {
                // 1. Validation of attributes by Model State
                if (ModelStateResponse() != null) return Json(new { success = false, message = "", errors = ModelStateResponse() });

                roleViewModel.Tag = FormattTag(roleViewModel.Tag);

                // 2. Map ViewModel entity to Model
                var role = _mapper.Map<Role>(roleViewModel);

                // 3. Execute the creation order (persistence in the database) 
                await _roleService.AddAsync(role);

                // 4. Business rules validation response in the backend (fluent Validation + DataBase)
                if (PersistenceResponse() != null) return Json(new { success = false, message = "", errors = PersistenceResponse() });

                // 5. Log creation success and submit message to screen
                AuditLog(LogType.Info, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, MessageHelper.GenericsControllers.SuccessCreationRegistration("perfil", roleViewModel.Name));

                return Json(new { success = true, message = MessageHelper.GenericsControllers.SuccessCreationRegistration("perfil", roleViewModel.Name) });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                AuditLog(LogType.Error, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }

        [Route("Role-Edit/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var roleViewModel = await GetRoleAndUsersByRoleId(id);

            if (roleViewModel == null)
                return Json(new { success = false, error = MessageHelper.GenericsControllers.ErrorParameterNotFound("Id", id.ToString()) });

            var fullRoleViewModel = await GetFullRoleViewModel(roleViewModel);

            return View(fullRoleViewModel);
        }


        [Route("Role-Edit/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, RoleViewModel roleViewModel)
        {
            try
            {
                // 1. Validation Id enter paramenter
                if (id != roleViewModel.Id) return Json(new { success = false, error = MessageHelper.GenericsControllers.ErrorParameterNotFound("Id", id.ToString()) });

                // 2. Validation of attributes by Model State
                if (ModelStateResponse() != null) return Json(new { success = false, message = "", errors = ModelStateResponse() });

                // 3. Map ViewModel entity to Model
                var role = _mapper.Map<Role>(roleViewModel);

                // 4. Execute the update order (persistence in the database) 
                await _roleService.UpdateAsync(role);

                // 5. Business rules validation response in the backend (fluent Validation + DataBase)
                if (PersistenceResponse() != null) return Json(new { success = false, message = "", errors = PersistenceResponse() });

                // 6. Log update success and submit message to screen
                AuditLog(LogType.Info, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, MessageHelper.GenericsControllers.SuccessChangeRegistration("perfil", roleViewModel.Name));

                return Json(new { success = true, message = MessageHelper.GenericsControllers.SuccessChangeRegistration("perfil", roleViewModel.Name) });
            }
            catch (Exception ex)
            {
                AuditLog(LogType.Error, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, ex.Message);
                return Json(new { success = false, error = ex.Message });
            }

        }

        [Route("Role-Remove/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                // 1. Get entity by Id
                var roleViewModel = await GetRoleAndUsersByRoleId(id);

                // 2. Validate if exist in database
                if (roleViewModel == null) return Json(new { success = false, error = MessageHelper.GenericsControllers.ErrorParameterNotFound("Id", id.ToString()) });

                // 3. Execute the remove (persistence in the database) 
                await _roleService.RemoveAsync(id);

                // 4. Business rules validation response in the backend (fluent Validation + DataBase)
                if (PersistenceResponse() != null) return Json(new { success = false, message = "", errors = PersistenceResponse() });

                // 5. Log remove success and submit message to screen
                AuditLog(LogType.Info, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, MessageHelper.GenericsControllers.SuccessRemovalRegistration("perfil", roleViewModel.Name));

                return Json(new { success = true, message = MessageHelper.GenericsControllers.SuccessRemovalRegistration("perfil", roleViewModel.Name) });
            }
            catch (Exception ex)
            {
                AuditLog(LogType.Error, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }

        [Route("Role-Activate/{id:guid}")]
        [HttpPost, ActionName("Activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            try
            {
                // 1. Get entity by Id
                var role = await GetSprintById(id);

                // 2. Validate if exist in database
                if (role == null) return Json(new { success = false, error = MessageHelper.GenericsControllers.ErrorParameterNotFound("Id", id.ToString()) });
                // 3. Execute the remove (persistence in the database) 
                await _roleService.ActivateAsync(id);

                // 4. Business rules validation response in the backend (fluent Validation + DataBase)
                if (PersistenceResponse() != null) return Json(new { success = false, message = "", errors = PersistenceResponse() });
                // 5. Log remove success and submit message to screen
                AuditLog(LogType.Info, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, MessageHelper.GenericsControllers.SuccessActivateRegistration("perfil", role.Name));
                return Json(new { success = true, message = MessageHelper.GenericsControllers.SuccessActivateRegistration("perfil", role.Name) });
            }
            catch (Exception ex)
            {
                AuditLog(LogType.Error, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }


        #region Private Methods
        private async Task<RoleViewModel> GetSprintById(Guid id)
        {
            return _mapper.Map<RoleViewModel>(await _roleRepository.GetByIdAsync(id));
        }
        private async Task<RoleViewModel> GetRoleAndUsersByRoleId(Guid id)
        {
            return _mapper.Map<RoleViewModel>(await _roleRepository.GetRoleAndUsersByRoleId(id));
        }

        private async Task<RoleViewModel> GetRoleAndDetailsById(Guid id)
        {
            return _mapper.Map<RoleViewModel>(await _roleRepository.GetRoleAndDetailsById(id));
        }

        private async Task<RoleViewModel> GetFullRoleViewModel(RoleViewModel roleViewModel)
        {
            roleViewModel.SupervisorSelect = _mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetAllAsync());

            return roleViewModel;
        }

        #endregion


    }
}
