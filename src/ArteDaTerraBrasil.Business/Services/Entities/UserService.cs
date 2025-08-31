using ArteDaTerraBrasil.Business.Interfaces.Entities;
using ArteDaTerraBrasil.Business.Interfaces.Shareds;
using ArteDaTerraBrasil.Business.Models.Entities;
using ArteDaTerraBrasil.Business.Services.Shareds;
using ArteDaTerraBrasil.Helper.StaticVariables;

namespace ArteDaTerraBrasil.Business.Services.Entities
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository,
                           INotifierService notifier,
                           IUnitOfWork unitOfWork) : base(notifier, unitOfWork)
        {
            _userRepository = userRepository;
        }

        public async Task AddAsync(User user)
        {
            await _userRepository.AddAsync(user);

            await Commit();
        }

        public async Task UpdateAsync(User user)
        {

            await _userRepository.UpdateAsync(user);

            await Commit();
        }

        public async Task ActivatedAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                NotifyWithMessage(MessageHelper.GenericErrors.CouldntFindEntity("utilizador", "id", id.ToString()));
                return;
            }

            if (user.Activated)
            {
                NotifyWithMessage(MessageHelper.GenericErrors.EntityAlreadyActive("utilizador"));
                return;
            }

            // 2: Activate the sprint
            user.Activated = true;
            await _userRepository.UpdateAsync(user);
            await Commit();

        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            // 3: Delete the sprint - Soft Delete
            user.Activated = false;
            await _userRepository.UpdateAsync(user);
            await Commit();
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}
