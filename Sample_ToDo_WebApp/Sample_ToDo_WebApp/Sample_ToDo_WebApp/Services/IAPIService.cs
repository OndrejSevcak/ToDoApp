
namespace Sample_ToDo_WebApp.Services
{
    public interface IAPIService
    {
        Task<string> SendAsync(string url, HttpMethod method);
        Task<string> PostAsync(string url);
    }
}