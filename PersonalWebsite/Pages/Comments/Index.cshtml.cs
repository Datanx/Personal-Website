using PersonalWebsite.Authorization;
using PersonalWebsite.Data;
using PersonalWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Pages.Comments
{
  public class IndexModel : DI_BasePageModel
  {
    public IndexModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
    {
    }

    public IList<Comment> Comment { get; set; }

    public async Task OnGetAsync()
    {
      var comments = from c in Context.Comment
                     select c;

      var isAuthorized = User.IsInRole(Commenters.CommentManagersRole) ||
                         User.IsInRole(Commenters.CommentAdministratorsRole);

      var currentUserId = UserManager.GetUserId(User);

      // Only approved comments are shown UNLESS you're authorized to see them
      // or you are the owner.
      if (!isAuthorized)
      {
        comments = comments.Where(c => c.Status == CommentStatus.Approved
                                    || c.OwnerID == currentUserId);
      }

      Comment = await comments.ToListAsync();
    }
  }
}