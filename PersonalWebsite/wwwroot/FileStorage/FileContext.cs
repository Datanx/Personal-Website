using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApiLect13.Models
{
  public class FileContext : DbContext
  {
    public FileContext(DbContextOptions<FileContext> options)
      : base(options)
    {
    }

    public DbSet<FileItem> FileItems { get; set; }
    public DbSet<Topic> Topics { get; set; }
  }
}
