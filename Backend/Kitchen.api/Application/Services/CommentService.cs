using AutoMapper;
using Kitchen.api.Application.Constract;
using Kitchen.api.Application.DTOs;
using Kitchen.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.api.Application.Services
{
    public class CommentService : ICommentService
    {
        #region Field
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CommentService(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }
        #endregion
        public async Task<IEnumerable<CommentDTO>> GetAllAccseptComments()
        {
            IEnumerable<Comment> comments =await _context.Comments.Where(p => p.Accepted == true).ToListAsync();
            
            IEnumerable<CommentDTO> commentDto = _mapper.Map<IEnumerable<CommentDTO>>(comments);
            foreach (var item in commentDto)
            {
                var user = await GetUserComment(item.UserId);
                item.User = user;
            }
            return commentDto;
        }
        public async Task<string> AddComment(AddCommentDTO addComment)
        {

            Comment comment = _mapper.Map<Comment>(addComment);
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return "نظر شما با موفقیت ثبت شد";
        }

        public async Task<bool> DeleteComment(int Id)
        {
            var comment = await _context.Comments.FindAsync(Id);
            _context.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CommentDTO>> GetAllComments()
        {
            IEnumerable<Comment> comments = await _context.Comments.ToListAsync();
            IEnumerable<CommentDTO> commentDto = _mapper.Map<IEnumerable<CommentDTO>>(comments);
            foreach (var item in commentDto)
            {
                var user = await GetUserComment(item.UserId);
                item.User = user;
            }
            return commentDto;
        }

        public async Task<CommentDTO> GetCommentById(int Id)
        {
            var comment = await _context.Comments.FindAsync(Id);
            CommentDTO commentDTO = _mapper.Map<CommentDTO>(comment);
            var user = await GetUserComment(commentDTO.UserId);
            commentDTO.User = user;
            return commentDTO;
        }
        public async Task<string> AcceptComment(int Id)
        {
            var comment = await _context.Comments.FindAsync(Id);
            comment.Accepted = true;
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return "نظر با موفقیت تایید شد";
        }
        public async Task<UserDTO> GetUserComment(int Id)
        {
            var user=  await _context.Users.FindAsync(Id);
            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }


    }
}
