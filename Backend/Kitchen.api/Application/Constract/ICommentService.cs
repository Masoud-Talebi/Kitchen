using Kitchen.api.Application.DTOs;

namespace Kitchen.api.Application.Constract
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetAllAccseptComments();
        Task<IEnumerable<CommentDTO>> GetAllComments();
        Task<string> AddComment(AddCommentDTO addComment);
        Task<CommentDTO> GetCommentById(int Id);
        Task<bool> DeleteComment(int Id);
        Task<string> AcceptComment(int Id);
    }
}