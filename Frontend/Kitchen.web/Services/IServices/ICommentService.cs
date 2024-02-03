namespace Kitchen.web.Services.IServices
{
    public interface ICommentService
    {
        Task<T> GetAllAccseptComments<T>();
        Task<T> GetAllComments<T>(string AccesToken);
        Task<T> AddComment<T>(AddCommentDTO addComment,string AccesToken);
        Task<T> GetCommentById<T>(int Id);
        Task<T> DeleteComment<T>(int Id,string AccesToken);
        Task<T> AcceptComment<T>(int Id,string AccesToken);
    }
}