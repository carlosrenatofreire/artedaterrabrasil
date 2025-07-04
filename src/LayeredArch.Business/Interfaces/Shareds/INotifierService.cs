using LayeredArch.Business.Models.Shareds;

namespace LayeredArch.Business.Interfaces.Shareds
{
    public interface INotifierService
    {
        bool HasNotification();
        List<Notification> GetNotification();
        void Handle(Notification notification);
    }
}
