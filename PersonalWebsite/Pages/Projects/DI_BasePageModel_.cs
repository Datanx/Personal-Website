using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalWebsite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Pages.Projects
{
  public class DI_BasePageModel_ : PageModel
  {
    protected ApplicationDbContext Context { get; }
    protected IAuthorizationService AuthorizationService { get; }
    protected UserManager<IdentityUser> UserManager { get; }

    public DI_BasePageModel_(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager) : base()
    {
      Context = context;
      UserManager = userManager;
      AuthorizationService = authorizationService;
    }
  }
}
