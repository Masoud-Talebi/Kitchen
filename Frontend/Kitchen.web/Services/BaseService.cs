using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Kitchen.web.Models.DTOs;
using Kitchen.web.Services.IServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Kitchen.web.SD;
namespace Kitchen.web.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDTO responseDTO { get; set; }
        public IHttpClientFactory httpClientFactory { get; set; }
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.responseDTO = new ResponseDTO();
            this.httpClientFactory = httpClientFactory;
        }


        public async Task<T> Send<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ShopClient");
                HttpRequestMessage message = new HttpRequestMessage();
                var multipartContent = new MultipartFormDataContent();

                message.Headers.Add("Accept", "application/json"); 
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest is not null)
                {
                    if (apiRequest.Image != null || apiRequest.Svg != null)
                    {
                        if (apiRequest.Image != null)
                        {
                            multipartContent.Add(new StreamContent(apiRequest.Image.OpenReadStream()), "Image", apiRequest.Image.FileName);
                        }
                        if (apiRequest.Svg != null)
                        {
                            multipartContent.Add(new StreamContent(apiRequest.Svg.OpenReadStream()), "svg", apiRequest.Svg.FileName);
                        }
                        message.Content = multipartContent;
                    }
                     
                    else
                    {
                        message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                    }
                   
                }
                HttpResponseMessage ApiResponse = null;
                switch (apiRequest.ApiType)
                {
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;

                }
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer" , apiRequest.AccessToken);
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                ApiResponse = await client.SendAsync(message);
                var ApiContent = await ApiResponse.Content.ReadAsStringAsync();
                var ApiContentDto = JsonConvert.DeserializeObject<T>(ApiContent);
                return ApiContentDto;
            }
            catch (Exception ex)
            {
                var dto = new ResponseDTO
                {
                    DisplayMessage = "Error",
                    ErrorMessage = new List<string> { ex.Message }
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiresponsedto = JsonConvert.DeserializeObject<T>(res);
                return apiresponsedto;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}