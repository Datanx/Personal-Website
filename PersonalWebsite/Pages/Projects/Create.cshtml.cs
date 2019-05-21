using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalWebsite.Authorization;
using PersonalWebsite.Data;
using PersonalWebsite.Models;

namespace PersonalWebsite.Pages.Projects
{
  public class CreateModel : DI_BasePageModel_
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
      Project = new Project
      {
        Title = "My LeetCode Solutions",
        StartDate = DateTime.UtcNow,
        Description = "I will put my solutions to Leetcode Problems in this repo. Every problem will be solved in C++.",
        URL = "https://github.com/Datanx/LeetCode"
      };
      return Page();
    }

    [BindProperty]
    public Project Project { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      // requires using PersonalWebsite.Authorization;
      var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                  User, Project,
                                                  ProjectOperations.Create);
      if (!isAuthorized.Succeeded)
      {
        return new ChallengeResult();
      }

      Context.Projects.Add(Project);
      await Context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }
  }
}