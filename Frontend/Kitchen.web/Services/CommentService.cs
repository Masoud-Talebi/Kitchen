using Kitchen.web.Services.IServices;

namespace Kitchen.web.Services
{

    public class CommentService : BaseService, ICommentService
    {
        private readonly IHttpClientFactory httpClient;
        public CommentService(IHttpClientFactory httpClient) : base(httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<T> AcceptComment<T>(int Id, string AccesToken)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Url = SD.KitchenApiBase + "/api/Comment/AcceptComment?Id=" + Id,
                AccessToken = AccesToken
            });
        }

        public async Task<T> AddComment<T>(AddCommentDTO addComment, string AccesToken)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data = addComment,
                Url = SD.KitchenApiBase + "/api/Comment",
                AccessToken = AccesToken
            });
        }

        public async Task<T> DeleteComment<T>(int Id, string AccesToken)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.KitchenApiBase + "/api/Comment?Id=" + Id,
                AccessToken = AccesToken
            });
        }

        public async Task<T> GetAllAccseptComments<T>()
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.KitchenApiBase + "/api/Comment"
            });
        }

        public async Task<T> GetAllComments<T>(string AccesToken)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.KitchenApiBase + "/api/Comment/GetAppCommentForAdmin",
                AccessToken = AccesToken
            });
        }

        public async Task<T> GetCommentById<T>(int Id)
        {
            return await this.Send<T>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.KitchenApiBase + "/api/Comment/GetCommentById?Id=" + Id,
            });
        }

    }
}