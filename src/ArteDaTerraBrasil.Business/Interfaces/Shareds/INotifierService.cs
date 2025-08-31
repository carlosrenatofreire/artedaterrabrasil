using ArteDaTerraBrasil.Business.Models.Shareds;

namespace ArteDaTerraBrasil.Business.Interfaces.Shareds
{
    public interface INotifierService
    {
        bool HasNotification();
        List<Notification> GetNotification();
        void Handle(Notification notification);
    }
}
