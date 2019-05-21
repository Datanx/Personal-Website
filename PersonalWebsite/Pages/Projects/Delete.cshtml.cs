using PersonalWebsite.Authorization;
using PersonalWebsite.Data;
using PersonalWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PersonalWebsite.Pages.Projects
{
  public class DeleteModel : DI_BasePageModel_
  {
    public DeleteModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
    {
    }

    [BindProperty]
    public Project Project { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
      Project = await Context.Projects.FirstOrDefaultAsync(
                                           m => m.ProjectId == id);

      if (Project == null)
      {
        return NotFound();
      }

      var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                               User, Project,
                                               ProjectOperations.Delete);
      if (!isAuthorized.Succeeded)
      {
        return new ChallengeResult();
      }

      return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
      Project = await Context.Projects.FindAsync(id);

      var project = await Context
          .Projects.AsNoTracking()
          .FirstOrDefaultAsync(m => m.ProjectId == id);

      if (project == null)
      {
        return NotFound();
      }

      var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                               User, project,
                                               CommentOperations.Delete);
      if (!isAuthorized.Succeeded)
      {
        return new ChallengeResult();
      }

      Context.Projects.Remove(Project);
      await Context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }
  }
}