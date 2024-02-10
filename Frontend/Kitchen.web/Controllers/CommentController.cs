using Kitchen.web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyApp.Namespace
{
    public class CommentController : Controller
    {
        private readonly ICommentService _service;
        public CommentController(ICommentService service)
        {
            _service = service;
        }
        // GET: CommentController
        public async Task<IActionResult> Index()
        {

            return View();
        }
        public async Task<PartialViewResult> GetAllComments()
        {
            IEnumerable<CommentDTO> comments = null;
            var response = await _service.GetAllAccseptComments<ResponseDTO>(); ;
            if (response is not null || response.IsSuccess)
            {
                comments = JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(Convert.ToString(response.Result));
            }
            return PartialView("_Comments", comments);
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentDTO commentDTO)
        {
            var res = await _service.AddComment<ResponseDTO>(commentDTO, HttpContext.Request.Cookies["token"].ToString());
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> GetAllCommentsAdmin()
        {
            IEnumerable<CommentDTO> comments = null;
            var response = await _service.GetAllComments<ResponseDTO>(HttpContext.Request.Cookies["token"].ToString());
            if (response is not null || response.IsSuccess)
            {
                comments = JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(Convert.ToString(response.Result));
            }
            return PartialView("_CommentsAdmin", comments);
        }
        public async Task<IActionResult> AcceptComment(int Id)
        {
            var res = await _service.AcceptComment<ResponseDTO>(Id, HttpContext.Request.Cookies["token"].ToString());
            return RedirectToAction("Comments", "Admin");
        }
        public async Task<IActionResult> DeleteComment(int Id)
        {
            var res = await _service.DeleteComment<ResponseDTO>(Id, HttpContext.Request.Cookies["token"].ToString());
            return RedirectToAction("Comments", "Admin");
        }

    }
}
