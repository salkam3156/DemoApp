using System.Threading.Tasks;
using DemoApp.ApplicationServices.Models;

namespace DemoApp.ApplicationServices.Contracts
{
    public interface INotifier
    {
        Task NotifyClientsAsync(Notification notification);
    }
}
