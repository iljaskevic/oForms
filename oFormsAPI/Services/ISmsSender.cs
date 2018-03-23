using System.Threading.Tasks;

namespace oFormsAPI.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
