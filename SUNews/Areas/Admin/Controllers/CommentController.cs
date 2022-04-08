using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SUNews.Services.Contracts;

namespace SUNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator, Owner, Manager")]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        private static string StatusMessage { get; set; } = "";

        private static string MessageContent { get; set; } = "";

        public CommentController(ICommentService _commentService)
        {
            this.commentService = _commentService;
        }

        public async Task<IActionResult> AllReportedComment()
        {
            var comments = await commentService.GetAllReportedCommentAsync();
            ViewData[StatusMessage] = MessageContent;

            return View(comments);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                await commentService.DeleteCommentAsync(id);
            }
            catch (Exception)
            {
                StatusMessage = "ErrorMessage";
                MessageContent = "Sorry but something went wrong.";

                return RedirectToAction("AllReportedComment", "Comment");
            }

            StatusMessage = "SuccessMessage";
            MessageContent = "Comment successfully deleted.";

            return RedirectToAction("AllReportedComment", "Comment");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteReport(int id)
        {
            try
            {
                await commentService.DeleteReportAsync(id);
            }
            catch (Exception)
            {
                StatusMessage = "ErrorMessage";
                MessageContent = "Sorry but something went wrong.";

                return RedirectToAction("AllReportedComment", "Comment");
            }

            StatusMessage = "SuccessMessage";
            MessageContent = "Report successfully deleted.";

            return RedirectToAction("AllReportedComment", "Comment");
        }
    }
}
