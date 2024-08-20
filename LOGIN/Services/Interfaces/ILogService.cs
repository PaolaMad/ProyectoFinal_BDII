
namespace LOGIN.Services.Interfaces
{
    public interface ILogService
    {
        Task CreateLogAsync(string action, string state);
    }
}
