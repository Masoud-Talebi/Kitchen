using Kitchen.api;
using Kitchen.api.Application.Constract;
using Kitchen.api.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ResponseDTO response;
        private readonly ApplicationDbContext _context;
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService, ApplicationDbContext context)
        {
            response = new ResponseDTO();
            _commentService = commentService;
            _context = context;
        }
        [HttpGet]
        public async Task<object> GetAllComment()
        {

            try
            {
                var Comments = await _commentService.GetAllAccseptComments();
                response.Result = Comments;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAppCommentForAdmin")]
        public async Task<object> GetAppCommentForAdmin()
        {
            try
            {
                var Comments = await _commentService.GetAllComments();
                response.Result = Comments;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }

        [HttpGet("GetCommentById")]
        public async Task<object> GetCommentById(int Id)
        {
            try
            {
                var Comments = await _commentService.GetCommentById(Id);
                response.Result = Comments;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [Authorize]
        [HttpPost]
        public async Task<object> AddComment(AddCommentDTO addComment)
        {

            try
            {
                var Setting = await _context.Settings.FirstOrDefaultAsync();
                var User = await _context.Users.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == int.Parse(HttpContext.User.Identity.Name));
                if (User.Comments.Count() >= Setting.UserComment)
                {
                    response.Result = $"شما تا بحال {User.Comments.Count()} نظر ثبت کرده ایید و دیگر نمیتوانید نظری ثبت کنید";
                    response.StatusCode = 404;

                }
                else
                {
                    addComment.UserId = int.Parse(HttpContext.User.Identity.Name);
                    var res = await _commentService.AddComment(addComment);
                    response.Result = res;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AcceptComment")]
        public async Task<object> AcceptComment(int Id)
        {

            try
            {
                var res = await _commentService.AcceptComment(Id);
                response.Result = res;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<object> DeleteComment(int Id)
        {
            try
            {
                var res = await _commentService.DeleteComment(Id);
                response.Result = res;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }
            return response;
        }
    }
}
