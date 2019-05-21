using PersonalWebsite.Authorization;
using PersonalWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

// dotnet aspnet-codegenerator razorpage -m Contact -dc ApplicationDbContext -outDir Pages\Contacts --referenceScriptLibraries
namespace PersonalWebsite.Data
{
  public static class SeedData
  {
    public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
    {
      using (var context = new ApplicationDbContext(
          serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
      {
        // For sample purposes seed both with the same password.
        // Password is set with the following:
        // dotnet user-secrets set SeedUserPW <pw>
        // The admin user can do anything

        var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@syr.edu");
        await EnsureRole(serviceProvider, adminID, Commenters.CommentAdministratorsRole);

        // allowed user can create and edit contacts that they create
        var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@syr.edu");
        await EnsureRole(serviceProvider, managerID, Commenters.CommentManagersRole);

        SeedDB(context, adminID);
      }
    }

    private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                string testUserPw, string UserName)
    {
      var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

      var user = await userManager.FindByNameAsync(UserName);
      if (user == null)
      {
        user = new IdentityUser { UserName = UserName };
        await userManager.CreateAsync(user, testUserPw);
      }

      return user.Id;
    }

    private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                  string uid, string role)
    {
      IdentityResult IR = null;
      var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

      if (roleManager == null)
      {
        throw new Exception("roleManager null");
      }

      if (!await roleManager.RoleExistsAsync(role))
      {
        IR = await roleManager.CreateAsync(new IdentityRole(role));
      }

      var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

      var user = await userManager.FindByIdAsync(uid);

      IR = await userManager.AddToRoleAsync(user, role);

      return IR;
    }
    
    public static void SeedDB(ApplicationDbContext context, string adminID)
    {
      if (context.Comment.Any())
      {
        return;   // DB has been seeded
      }

      context.Projects.AddRange(
        new Project
        {
          Title = "Remote Code Repository",
          StartDate = DateTime.UtcNow,
          Description = "A Code Repository is a Program responsible for managing source code resources, e.g., files and documents.",
          URL = "https://github.com/Datanx/Remote-Code-Repository"
        },
        new Project
        {
          Title = "Nucleus Message Passing",
          StartDate = DateTime.UtcNow,
          Description = "Implement four system calls as described in the papers, to be called by user programs (i.e. C programs)",
          URL = "https://github.com/Datanx/Nucleus-Message-Passing"
        },
        new Project
        {
          Title = "Online Shopping Website",
          StartDate = DateTime.UtcNow,
          Description = "The entire mall backend is implemented by Node.js, and the backend REST interface is implemented through the Express framework, and is output in the form of json.",
          URL = "https://github.com/Datanx/Online-Shopping-Website"
        });

      context.Comment.AddRange(
                new Comment
                {
                  Name = "Debra Garcia",
                  Title = "Audible, an Amazon company",
                  Content = "I was checking out your background with great interest and I was hoping to connect to learn more about you and discuss positions at Audible.",
                  Created = DateTime.UtcNow,
                  Email = "debra@example.com",
                  Status = CommentStatus.Approved,
                  OwnerID = adminID
                },
                new Comment
                {
                  Name = "Thorsten Weinrich",
                  Title = "Software Engineer role at F5 Networks",
                  Content = "I am one of the in-house recruiters at F5 Networks working with our Product Development Team.",
                  Created = DateTime.UtcNow,
                  Email = "thorsten@example.com",
                  Status = CommentStatus.Submitted,
                  OwnerID = adminID
                },
       new Comment
       {
         Name = "Yuhong Li",
         Title = "Full-time Software Developer - Antra Inc.",
         Content = "Antra is hiring for Software Developer. I think your experience and skills are a good match for our position.",
         Created = DateTime.UtcNow,
         Email = "yuhong@example.com",
         Status = CommentStatus.Rejected,
         OwnerID = adminID
       },
       new Comment
       {
         Name = "Jon Orton",
         Title = "Hiring OPTs/CPTs for Full-Time Opportunities",
         Content = "How are you? Let me know if you are looking for a Full-Time Job ?",
         Created = DateTime.UtcNow,
         Email = "jon@example.com",
         Status = CommentStatus.Submitted,
         OwnerID = adminID
       },
       new Comment
       {
         Name = "Will VanDerKloot",
         Title = "Software Engineering opportunity available!",
         Content = "I'm not sure of your current situation but wanted to reach out and start a conversation to determine your wants and needs in terms of a career move.",
         Created = DateTime.UtcNow,
         Email = "willvan@example.com",
         Status = CommentStatus.Submitted,
         OwnerID = adminID
       }
       );
      context.SaveChanges();
    }
  }
}