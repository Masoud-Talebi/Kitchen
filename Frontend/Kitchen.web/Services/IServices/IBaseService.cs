using Kitchen.web.Models.DTOs;

namespace Kitchen.web.Services.IServices
{
    
public interface IBaseService : IDisposable
{
    ResponseDTO responseDTO { get; set; }
    Task<T> Send<T>(ApiRequest apiRequest);
}
}