using ArteDaTerraBrasil.Business.Interfaces.Parameters;
using ArteDaTerraBrasil.Business.Interfaces.Shareds;
using ArteDaTerraBrasil.Business.Models.Parameters;
using ArteDaTerraBrasil.Business.Services.Shareds;
using ArteDaTerraBrasil.Business.Validations.Parameters;
using ArteDaTerraBrasil.Helper.StaticVariables;

namespace ArteDaTerraBrasil.Business.Services.Parameters
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository,
        INotifierService notifier,
                           IUnitOfWork unitOfWork) : base(notifier, unitOfWork)
        {
            _roleRepository = roleRepository;
        }

        public async Task AddAsync(Role role)
        {
            // 1. Run Validation
            if (!RunValidation(new RoleValidation(), role)) return;

            // 2. Validate that the role name doesnt already exist
            if (_roleRepository.FindAsync(x => x.Name == role.Name).Result.Any())
            {
                NotifyWithMessage("O nome que escolheu para a Role já existe");
                return;
            }

            // 3. Run persistence
            await _roleRepository.AddAsync(role);

            await Commit();
        }

        public async Task RemoveAsync(Guid id)
        {

            // 1. Run Validation in the database
            var role = await _roleRepository.GetRoleAndUsersByRoleId(id);

            if (role == null)
            {
                NotifyWithMessage(MessageHelper.GenericErrors.CouldntFindEntity("Role", "id", id.ToString()));
                return;
            }

            if (role.Users.Any())
            {
                NotifyWithMessage(MessageHelper.GenericErrors.CannotDeleteEntity("Role"));
                return;
            }

            // 3: Delete
            role.Activated = false;
            await _roleRepository.UpdateAsync(role);
            await Commit();
        }

        public async Task ActivateAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                NotifyWithMessage(MessageHelper.GenericErrors.CouldntFindEntity("Role", "id", id.ToString()));
                return;
            }

            if (role.Activated)
            {
                NotifyWithMessage(MessageHelper.GenericErrors.EntityAlreadyActive("Role"));
                return;
            }

            // 2: Activate the sprint
            role.Activated = true;
            await _roleRepository.UpdateAsync(role);
            await Commit();

        }

        public async Task UpdateAsync(Role role)
        {
            // 1. Run Validation 
            if (!RunValidation(new RoleValidation(), role)) return;

            // 2. Validate that the role name doesnt already exist
            if (_roleRepository.FindAsync(x => x.Name == role.Name && x.Id != role.Id).Result.Any())
            {
                NotifyWithMessage(MessageHelper.GenericErrors.FieldAlreadyExists("Role", "Nome", role.Name));
                return;
            }

            // 3. Run persistence
            await _roleRepository.UpdateAsync(role);
            await Commit();
        }
        public void Dispose()
        {
            _roleRepository?.Dispose();
        }
    }
}
