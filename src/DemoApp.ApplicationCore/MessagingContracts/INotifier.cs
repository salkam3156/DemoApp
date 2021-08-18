using DemoApp.ApplicationCore.Models;
using System.Threading.Tasks;

namespace DemoApp.ApplicationCore.MessagingContracts
{
    public interface INotifier
    {
        Task NotifyClientsAsync(Notification notification);
    }
}
