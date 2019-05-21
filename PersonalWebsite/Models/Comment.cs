using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalWebsite.Models
{
  public class Comment
  {
    public int CommentId { get; set; }

    // user ID from AspNetUser table.
    public string OwnerID { get; set; }

    public string Name { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime Created { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public CommentStatus Status { get; set; }
  }

  public enum CommentStatus
  {
    Submitted,
    Approved,
    Rejected
  }
}