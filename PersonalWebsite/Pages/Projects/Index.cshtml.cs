using PersonalWebsite.Authorization;
using PersonalWebsite.Data;
using PersonalWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Pages.Projects
{
  public class IndexModel : DI_BasePageModel_
  {
    public IndexModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
    {
    }

    public IList<Project> Project { get; set; }

    public async Task OnGetAsync()
    {
      var projects = from c in Context.Projects
                     select c;
      Project = await projects.ToListAsync();
    }
  }
}