using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace PersonalWebsite.Authorization
{
  public static class CommentOperations
  {
    public static OperationAuthorizationRequirement Create =
      new OperationAuthorizationRequirement { Name = Commenters.CreateOperationName };
    public static OperationAuthorizationRequirement Read =
      new OperationAuthorizationRequirement { Name = Commenters.ReadOperationName };
    public static OperationAuthorizationRequirement Update =
      new OperationAuthorizationRequirement { Name = Commenters.UpdateOperationName };
    public static OperationAuthorizationRequirement Delete =
      new OperationAuthorizationRequirement { Name = Commenters.DeleteOperationName };
    public static OperationAuthorizationRequirement Approve =
      new OperationAuthorizationRequirement { Name = Commenters.ApproveOperationName };
    public static OperationAuthorizationRequirement Reject =
      new OperationAuthorizationRequirement { Name = Commenters.RejectOperationName };
  }

  public class Commenters
  {
    public static readonly string CreateOperationName = "Create";
    public static readonly string ReadOperationName = "Read";
    public static readonly string UpdateOperationName = "Update";
    public static readonly string DeleteOperationName = "Delete";
    public static readonly string ApproveOperationName = "Approve";
    public static readonly string RejectOperationName = "Reject";

    public static readonly string CommentAdministratorsRole = "CommentAdministrators";
    public static readonly string CommentManagersRole = "CommentManagers";
  }
}