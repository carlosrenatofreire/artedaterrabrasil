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
    public class ClaimController : BaseController
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IClaimService _claimService;
        private readonly IModuleRepository _moduleRepository;
        private readonly IModuleService _moduleService;
        private readonly IMapper _mapper;


        public ClaimController(INotifierService notifier, IAccessorService accessorService,
                                                                IClaimRepository claimRepository,
                                                                IClaimService claimService,
                                                                IAuditLogService auditLogService,
                                                                IModuleRepository moduleRepository,
                                                                IModuleService moduleService,
                                                                IMapper mapper
                                                                ) : base(notifier, accessorService, auditLogService)
        {

            _moduleRepository = moduleRepository;
            _moduleService = moduleService;
            _claimRepository = claimRepository;
            _claimService = claimService;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [Route("Claim-List")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ClaimViewModel>>(await _claimRepository.GetAllWithModule()).OrderBy(x => x.Module.Name));
        }

        [Route("Claim-Details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var claimViewModel = await GetClaimAndModulesById(id);

            if (claimViewModel == null)
                return Json(new { success = false, error = "Declaração não encontrada!" });

            return View(claimViewModel);
        }

        [Route("Claim-Create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var claimViewModel = await GetFullClaimViewModel(new ClaimViewModel());
            return View(claimViewModel);
        }

        [Route("Claim-Create")]
        [HttpPost]
        public async Task<IActionResult> Create(ClaimViewModel claimViewModel)
        {
            try
            {
                // 1. Validation of attributes by Model State
                if (ModelStateResponse() != null) return Json(new { success = false, message = "", errors = ModelStateResponse() });

                claimViewModel.Tag = FormattTag(claimViewModel.Tag);

                // 2. Map ViewModel entity to Model
                var claim = _mapper.Map<Claim>(claimViewModel);

                // 3. Execute the creation order (persistence in the database) 
                await _claimService.AddAsync(claim);

                // 4. Business rules validation response in the backend (fluent Validation + DataBase)
                if (PersistenceResponse() != null) return Json(new { success = false, message = "", errors = PersistenceResponse() });

                // 5. Log creation success and submit message to screen
                AuditLog(LogType.Info, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, MessageHelper.GenericsControllers.SuccessCreationRegistration("declaração", claimViewModel.Name));

                return Json(new { success = true, message = MessageHelper.GenericsControllers.SuccessCreationRegistration("declaração", claimViewModel.Name) });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                AuditLog(LogType.Error, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }

        [Route("Claim-Edit/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var claimViewModel = _mapper.Map<ClaimViewModel>(await _claimRepository.GetByIdAsync(id));

            if (claimViewModel == null)
                return Json(new { success = false, error = MessageHelper.GenericsControllers.ErrorParameterNotFound("Id", id.ToString()) });

            var fullClaimViewModel = await GetFullClaimViewModel(claimViewModel);

            return View(fullClaimViewModel);
        }


        [Route("Claim-Edit/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ClaimViewModel claimViewModel)
        {
            try
            {
                // 1. Validation Id enter paramenter
                if (id != claimViewModel.Id) return Json(new { success = false, error = MessageHelper.GenericsControllers.ErrorParameterNotFound("Id", id.ToString()) });

                // 2. Validation of attributes by Model State
                if (ModelStateResponse() != null) return Json(new { success = false, message = "", errors = ModelStateResponse() });

                // 3. Map ViewModel entity to Model
                var claim = _mapper.Map<Claim>(claimViewModel);

                // 4. Execute the update order (persistence in the database) 
                await _claimService.UpdateAsync(claim);

                // 5. Business rules validation response in the backend (fluent Validation + DataBase)
                if (PersistenceResponse() != null) return Json(new { success = false, message = "", errors = PersistenceResponse() });

                // 6. Log update success and submit message to screen
                AuditLog(LogType.Info, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, MessageHelper.GenericsControllers.SuccessChangeRegistration("declaração", claimViewModel.Name));

                return Json(new { success = true, message = MessageHelper.GenericsControllers.SuccessChangeRegistration("declaração", claimViewModel.Name) });
            }
            catch (Exception ex)
            {
                AuditLog(LogType.Error, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, ex.Message);
                return Json(new { success = false, error = ex.Message });
            }

        }

        [Route("Claim-Remove/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                // 1. Get entity by Id
                var claimViewModel = await _claimRepository.GetByIdAsync(id);

                // 2. Validate if exist in database
                if (claimViewModel == null) return Json(new { success = false, error = MessageHelper.GenericsControllers.ErrorParameterNotFound("Id", id.ToString()) });

                // 3. Execute the remove (persistence in the database) 
                await _claimService.RemoveAsync(id);

                // 4. Business rules validation response in the backend (fluent Validation + DataBase)
                if (PersistenceResponse() != null) return Json(new { success = false, message = "", errors = PersistenceResponse() });

                // 5. Log remove success and submit message to screen
                AuditLog(LogType.Info, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, MessageHelper.GenericsControllers.SuccessRemovalRegistration("declaração", claimViewModel.Name));

                return Json(new { success = true, message = MessageHelper.GenericsControllers.SuccessRemovalRegistration("declaração", claimViewModel.Name) });
            }
            catch (Exception ex)
            {
                AuditLog(LogType.Error, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }

        [Route("Claim-Activate/{id:guid}")]
        [HttpPost, ActionName("Activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            try
            {
                // 1. Get entity by Id
                var claim = await _claimRepository.GetByIdAsync(id);

                // 2. Validate if exist in database
                if (claim == null) return Json(new { success = false, error = MessageHelper.GenericsControllers.ErrorParameterNotFound("Id", id.ToString()) });
                // 3. Execute the remove (persistence in the database) 
                await _claimService.ActivatedAsync(id);

                // 4. Business rules validation response in the backend (fluent Validation + DataBase)
                if (PersistenceResponse() != null) return Json(new { success = false, message = "", errors = PersistenceResponse() });
                // 5. Log remove success and submit message to screen
                AuditLog(LogType.Info, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, MessageHelper.GenericsControllers.SuccessActivateRegistration("declaração", claim.Name));
                return Json(new { success = true, message = MessageHelper.GenericsControllers.SuccessActivateRegistration("declaração", claim.Name) });
            }
            catch (Exception ex)
            {
                AuditLog(LogType.Error, Request.Host.ToString(), Request.Path, VerbType.Post.ToString(), Ip, ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }

        #region Private Methods
        private async Task<ClaimViewModel> GetClaimAndModulesById(Guid id)
        {
            return _mapper.Map<ClaimViewModel>(await _claimRepository.GetClaimAndModuleById(id));
        }

        private async Task<ClaimViewModel> GetFullClaimViewModel(ClaimViewModel claimViewModel)
        {
            claimViewModel.ModuleSelect = _mapper.Map<IEnumerable<ModuleViewModel>>(await _moduleRepository.GetAllAsync()).OrderBy(x => x.Name);
            return claimViewModel;
        }
        #endregion

    }
}
