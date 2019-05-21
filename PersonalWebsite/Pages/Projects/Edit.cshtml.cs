using PersonalWebsite.Authorization;
using PersonalWebsite.Data;
using PersonalWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Pages.Projects
{
  public class EditModel : DI_BasePageModel_
  {
    public EditModel(
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
                                                ProjectOperations.Update);
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

      // Fetch Project from DB.
      var project = await Context
          .Projects.AsNoTracking()
          .FirstOrDefaultAsync(m => m.ProjectId == id);

      if (project == null)
      {
        return NotFound();
      }

      var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                               User, project,
                                               CommentOperations.Update);
      if (!isAuthorized.Succeeded)
      {
        return new ChallengeResult();
      }

      Context.Attach(Project).State = EntityState.Modified;

      await Context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }

    private bool ProjectExists(int id)
    {
      return Context.Projects.Any(e => e.ProjectId == id);
    }
  }
}