using System.Threading.Tasks;

namespace StudioBMS.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}