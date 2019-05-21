using PersonalWebsite.Authorization;
using PersonalWebsite.Data;
using PersonalWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Pages.Comments
{
  public class EditModel : DI_BasePageModel
  {
    public EditModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
    {
    }

    [BindProperty]
    public Comment Comment { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
      Comment = await Context.Comment.FirstOrDefaultAsync(
                                                 m => m.CommentId == id);

      if (Comment == null)
      {
        return NotFound();
      }

      var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                User, Comment,
                                                CommentOperations.Update);
      if (!isAuthorized.Succeeded)
      {
        return new ChallengeResult();
      }

      return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      // Fetch Comment from DB to get OwnerID.
      var comment = await Context
          .Comment.AsNoTracking()
          .FirstOrDefaultAsync(m => m.CommentId == id);

      if (comment == null)
      {
        return NotFound();
      }

      var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                               User, comment,
                                               CommentOperations.Update);
      if (!isAuthorized.Succeeded)
      {
        return new ChallengeResult();
      }

      Comment.OwnerID = comment.OwnerID;

      Context.Attach(Comment).State = EntityState.Modified;

      if (comment.Status == CommentStatus.Approved)
      {
        // If the comment is updated after approval, 
        // and the user cannot approve,
        // set the status back to submitted so the update can be
        // checked and approved.
        var canApprove = await AuthorizationService.AuthorizeAsync(User,
                                comment,
                                CommentOperations.Approve);

        if (!canApprove.Succeeded)
        {
          comment.Status = CommentStatus.Submitted;
        }
      }

      await Context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }

    private bool CommentExists(int id)
    {
      return Context.Comment.Any(e => e.CommentId == id);
    }
  }
}
