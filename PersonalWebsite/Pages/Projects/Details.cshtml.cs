using PersonalWebsite.Authorization;
using PersonalWebsite.Data;
using PersonalWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PersonalWebsite.Pages.Projects
{
  public class DetailsModel : DI_BasePageModel_
  {
    public DetailsModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
    {
    }

    public Project Project { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
      Project = await Context.Projects.FirstOrDefaultAsync(m => m.ProjectId == id);

      if (Project == null)
      {
        return NotFound();
      }

      return Page();
    }
  }
}