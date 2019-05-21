using PersonalWebsite.Authorization;
using PersonalWebsite.Data;
using PersonalWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PersonalWebsite.Pages.Comments
{
    public class DetailsModel : DI_BasePageModel
    {
        public DetailsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public Comment Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Comment = await Context.Comment.FirstOrDefaultAsync(m => m.CommentId == id);

            if (Comment == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Commenters.CommentManagersRole) ||
                               User.IsInRole(Commenters.CommentAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized 
                &&  currentUserId != Comment.OwnerID
                && Comment.Status != CommentStatus.Approved) 
            {
                return new ChallengeResult();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, CommentStatus status)
        {
            var comment = await Context.Comment.FirstOrDefaultAsync(
                                                      m => m.CommentId == id);

            if (comment == null)
            {
                return NotFound();
            }

            var commentOperation = (status == CommentStatus.Approved)
                                                       ? CommentOperations.Approve
                                                       : CommentOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, comment,
                                        commentOperation);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            comment.Status = status;
            Context.Comment.Update(comment);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
