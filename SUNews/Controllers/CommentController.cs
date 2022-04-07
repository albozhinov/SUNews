using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SUNews.Data.Models;
using SUNews.Models;
using SUNews.Providers;
using SUNews.Services.Contracts;

namespace SUNews.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IUserManager<User> userManager;


        public CommentController(ICommentService _commentService, IUserManager<User> _userManager)
        {
            commentService = _commentService;
            userManager = _userManager;
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateComment(CreateCommentViewModel createCommentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(createCommentViewModel.CurrentAction, createCommentViewModel.CurrentController, new { id = createCommentViewModel.ArticleId });
            }

            var user = await userManager.GetUserAsync(User);
            await commentService.CreateCommentAsync(createCommentViewModel.Text, createCommentViewModel.ArticleId, user.Id);

            return RedirectToAction(createCommentViewModel.CurrentAction, createCommentViewModel.CurrentController, new { id = createCommentViewModel.ArticleId });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteComment(int id, string articleId)
        {
            try
            {
                await commentService.DeleteCommentAsync(id);
            }
            catch (Exception)
            {
                ViewData["ErrorMessage"] = "Sorry but something went wrong.";

                return RedirectToAction("DetailsOfArticle", "Article", new { id = articleId });
            }

            return RedirectToAction("DetailsOfArticle", "Article", new { id = articleId });
        }
    }
}
