using ArteDaTerraBrasil.Business.Interfaces.Shareds;
using ArteDaTerraBrasil.Business.Models.Shareds;

namespace ArteDaTerraBrasil.Business.Services.Shareds
{
    public class NotifierService : INotifierService
    {
        private readonly List<Notification> _notifications;

        public NotifierService()
        {
            _notifications = new List<Notification>();
        }
        public List<Notification> GetNotification()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}
