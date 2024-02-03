using static Kitchen.web.SD;

namespace Kitchen.web.Models.DTOs
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; } = "KitchenToken";
    }
}