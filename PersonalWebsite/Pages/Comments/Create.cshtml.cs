using PersonalWebsite.Authorization;
using PersonalWebsite.Data;
using PersonalWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;

namespace PersonalWebsite.Pages.Comments
{
  public class CreateModel : DI_BasePageModel
  {
    public CreateModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
    {
    }

    public IActionResult OnGet()
    {
      Comment = new Comment
      {
        Name = "Rick",
        Title = "It's time to make career moves",
        Content = "I know taking your next career step can be stressful, but I'm excited to let you know LinkedIn is here to help.",
        Email = "rick@rick.com"
      };
      return Page();
    }

    [BindProperty]
    public Comment Comment { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      Comment.OwnerID = UserManager.GetUserId(User);
      Comment.Created = DateTime.UtcNow;

      // requires using PersonalWebsite.Authorization;
      var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                  User, Comment,
                                                  CommentOperations.Create);
      if (!isAuthorized.Succeeded)
      {
        return new ChallengeResult();
      }

      Context.Comment.Add(Comment);
      await Context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }
  }
}