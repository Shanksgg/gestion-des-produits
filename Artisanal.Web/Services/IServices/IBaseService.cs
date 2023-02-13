using Artisanal.Web.Models;

namespace Artisanal.Web.Services.IServices
{
    public interface IBaseService :IDisposable
    {
        public ResponseDto ResponseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}