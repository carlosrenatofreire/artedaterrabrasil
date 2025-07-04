using LayeredArch.Business.Interfaces.Parameters;
using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Models.Parameters;
using LayeredArch.Business.Services.Shareds;
using LayeredArch.Business.Validations.Parameters;
using LayeredArch.Helper.StaticVariables;

namespace LayeredArch.Business.Services.Parameters
{
    public class ModuleService : BaseService, IModuleService
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleService(IModuleRepository moduleRepository,
                             INotifierService notifier,
                             IUnitOfWork unitOfWork) : base(notifier, unitOfWork)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task AddAsync(Module module)
        {
            // 1. Run Validation
            if (!RunValidation(new ModuleValidation(), module)) return;

            // 2. Validate that the role name doesnt already exist
            if (_moduleRepository.FindAsync(x => x.Name == module.Name).Result.Any())
            {
                NotifyWithMessage(MessageHelper.GenericErrors.FieldAlreadyExists("Módulo", "Nome", module.Name));
                return;
            }

            // 3. Run persistence
            await _moduleRepository.AddAsync(module);

            await Commit();
        }

        public async Task UpdateAsync(Module module)
        {
            // 1. Run Validation 
            if (!RunValidation(new ModuleValidation(), module)) return;

            // 2. Validate that the role name doesnt already exist
            if (_moduleRepository.FindAsync(x => x.Name == module.Name && x.Id != module.Id).Result.Any())
            {
                NotifyWithMessage(MessageHelper.GenericErrors.FieldAlreadyExists("Módulo", "Nome", module.Name));
                return;
            }

            // 3. Run persistence
            await _moduleRepository.UpdateAsync(module);
            await Commit();
        }

        public async Task RemoveAsync(Guid id)
        {
            Module module = await _moduleRepository.GetModuleAndClaimsById(id);

            if (module == null)
            {
                NotifyWithMessage(MessageHelper.GenericErrors.CouldntFindEntity("Módulo", "id", id.ToString()));
                return;
            }
            if (module.Claims.Any())
            {
                NotifyWithMessage(MessageHelper.GenericErrors.CannotDeleteEntity("Módulo"));
                return;
            }

            // 3: Delete(Soft Delete)
            module.Activated = false;
            await _moduleRepository.UpdateAsync(module);
            await Commit();
        }

        public async Task ActivatedAsync(Guid id)
        {
            var module = await _moduleRepository.GetByIdAsync(id);

            if (module == null)
            {
                NotifyWithMessage(MessageHelper.GenericErrors.CouldntFindEntity("Módulo", "id", id.ToString()));
                return;
            }

            if (module.Activated)
            {
                NotifyWithMessage(MessageHelper.GenericErrors.EntityAlreadyActive("Módulo"));
                return;
            }

            module.Activated = true;
            await _moduleRepository.UpdateAsync(module);
            await Commit();
        }

        public void Dispose()
        {
            _moduleRepository?.Dispose();
        }


    }
}
