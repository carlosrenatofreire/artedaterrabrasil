using ArteDaTerraBrasil.Business.Enums;
using ArteDaTerraBrasil.Business.Interfaces.Auxiliares;
using ArteDaTerraBrasil.Business.Interfaces.Parameters;
using ArteDaTerraBrasil.Business.Interfaces.Shareds;
using ArteDaTerraBrasil.Business.Models.Parameters;
using ArteDaTerraBrasil.Helper.StaticVariables;
using ArteDaTerraBrasil.Mvc.Controllers.Shared;
using ArteDaTerraBrasil.Mvc.ViewModels.Parameters;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArteDaTerraBrasil.Mvc.Controllers.Parameters
{
    public class ModuleController : BaseController
    {

        private readonly IModuleRepository _moduleRepository;
        private readonly IModuleService _moduleService;
        private readonly IMapper _mapper;


        public ModuleController(INotifierService notifier, IAccessorService accessorService,
                                                                IAuditLogService auditLogService,
                                                                IModuleRepository moduleRepository,
                                                                IModuleService moduleService,
                                                                IMapper mapper
                                                                ) : base(notifier, accessorService, auditLogService)
        {

            _moduleRepository = moduleRepository;
            _moduleService = moduleService;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [Route("Module-List")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ModuleViewModel>>(await _moduleRepository.GetAllAsync()));
        }

        [Route("Module-Details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var moduleViewModel = await GetModuleAndClaimsById(id);

            if (moduleViewModel == null)
                return Json(new { success = false, error = "Módulo não encontrado!" });

            return View(moduleViewModel);
        }

        [Route("Module-Create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Route("Module-Create")]
        [HttpPost]
        public async Task<IActionResult> Create(ModuleViewModel moduleViewModel)
        {
            try
            {
                // 1. Validation of attributes by Model State
                if (ModelStateResponse() != null) return Json(new { success = false, message = "", errors = ModelStateResponse() });

                moduleViewModel.Tag = FormattTag(moduleViewModel.Tag);

                // 2. Map ViewModel entity to Model
                var module = _mapper.Map<Module>(moduleViewModel);

                // 3. Execute the creation order (persistence in the database) 
                await _moduleService.AddAsync(module);

                // 4. Business rules validation response in the backend (fluent Validation + DataBase)
                if (PersistenceResponse() != null) return Json(new { success = false, message = "", errors = PersistenceResponse() });

                // 5. Log creation success and submit message to screen
                AuditLog(LogType.Info, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, MessageHelper.GenericsControllers.SuccessCreationRegistration("módulo", moduleViewModel.Name));

                return Json(new { success = true, message = MessageHelper.GenericsControllers.SuccessCreationRegistration("módulo", moduleViewModel.Name) });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                AuditLog(LogType.Error, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }

        [Route("Module-Edit/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var module = await _moduleRepository.GetByIdAsync(id);

            if (module == null)
                return Json(new { success = false, error = MessageHelper.GenericsControllers.ErrorParameterNotFound("Id", id.ToString()) });

            ModuleViewModel moduleViewModel = _mapper.Map<ModuleViewModel>(module);

            return View(moduleViewModel);
        }


        [Route("Module-Edit/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ModuleViewModel moduleViewModel)
        {
            try
            {
                // 1. Validation Id enter paramenter
                if (id != moduleViewModel.Id) return Json(new { success = false, error = MessageHelper.GenericsControllers.ErrorParameterNotFound("Id", id.ToString()) });

                // 2. Validation of attributes by Model State
                if (ModelStateResponse() != null) return Json(new { success = false, message = "", errors = ModelStateResponse() });

                // 3. Map ViewModel entity to Model
                var module = _mapper.Map<Module>(moduleViewModel);

                // 4. Execute the update order (persistence in the database) 
                await _moduleService.UpdateAsync(module);

                // 5. Business rules validation response in the backend (fluent Validation + DataBase)
                if (PersistenceResponse() != null) return Json(new { success = false, message = "", errors = PersistenceResponse() });

                // 6. Log update success and submit message to screen
                AuditLog(LogType.Info, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, MessageHelper.GenericsControllers.SuccessChangeRegistration("módulo", moduleViewModel.Name));

                return Json(new { success = true, message = MessageHelper.GenericsControllers.SuccessChangeRegistration("módulo", moduleViewModel.Name) });
            }
            catch (Exception ex)
            {
                AuditLog(LogType.Error, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, ex.Message);
                return Json(new { success = false, error = ex.Message });
            }

        }

        [Route("Module-Remove/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                // 1. Get entity by Id
                var moduleViewModel = await GetModuleAndClaimsById(id);

                // 2. Validate if exist in database
                if (moduleViewModel == null) return Json(new { success = false, error = MessageHelper.GenericsControllers.ErrorParameterNotFound("Id", id.ToString()) });

                // 3. Execute the remove (persistence in the database) 
                await _moduleService.RemoveAsync(id);

                // 4. Business rules validation response in the backend (fluent Validation + DataBase)
                if (PersistenceResponse() != null) return Json(new { success = false, message = "", errors = PersistenceResponse() });

                // 5. Log remove success and submit message to screen
                AuditLog(LogType.Info, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, MessageHelper.GenericsControllers.SuccessRemovalRegistration("módulo", moduleViewModel.Name));

                return Json(new { success = true, message = MessageHelper.GenericsControllers.SuccessRemovalRegistration("módulo", moduleViewModel.Name) });
            }
            catch (Exception ex)
            {
                AuditLog(LogType.Error, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }

        [Route("Module-Activate/{id:guid}")]
        [HttpPost, ActionName("Activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            try
            {
                // 1. Get entity by Id
                var module = await _moduleRepository.GetByIdAsync(id);

                // 2. Validate if exist in database
                if (module == null) return Json(new { success = false, error = MessageHelper.GenericsControllers.ErrorParameterNotFound("Id", id.ToString()) });

                // 3. Execute the remove (persistence in the database) 
                await _moduleService.ActivatedAsync(id);

                // 4. Business rules validation response in the backend (fluent Validation + DataBase)
                if (PersistenceResponse() != null) return Json(new { success = false, message = "", errors = PersistenceResponse() });

                // 5. Log remove success and submit message to screen
                AuditLog(LogType.Info, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, MessageHelper.GenericsControllers.SuccessActivateRegistration("módulo", module.Name));
                return Json(new { success = true, message = MessageHelper.GenericsControllers.SuccessActivateRegistration("módulo", module.Name) });
            }
            catch (Exception ex)
            {
                AuditLog(LogType.Error, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }

        #region Private Methods

        private async Task<ModuleViewModel> GetModuleAndClaimsById(Guid id)
        {
            return _mapper.Map<ModuleViewModel>(await _moduleRepository.GetModuleAndClaimsById(id));
        }

        #endregion

    }
}
