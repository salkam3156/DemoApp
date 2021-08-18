using System.Threading.Tasks;
using DemoApp.ApplicationServices.Models;

namespace DemoApp.ApplicationServices.MessagingContracts
{
    public interface INotifier
    {
        Task NotifyClientsAsync(Notification notification);
    }
}
