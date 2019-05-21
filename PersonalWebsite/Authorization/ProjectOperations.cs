using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace PersonalWebsite.Authorization
{
  public class ProjectOperations
  {
    public static OperationAuthorizationRequirement Create =
          new OperationAuthorizationRequirement { Name = Developers.CreateOperationName };
    public static OperationAuthorizationRequirement Read =
      new OperationAuthorizationRequirement { Name = Developers.ReadOperationName };
    public static OperationAuthorizationRequirement Update =
      new OperationAuthorizationRequirement { Name = Developers.UpdateOperationName };
    public static OperationAuthorizationRequirement Delete =
      new OperationAuthorizationRequirement { Name = Developers.DeleteOperationName };
  }

  public class Developers
  {
    public static readonly string CreateOperationName = "Create";
    public static readonly string ReadOperationName = "Read";
    public static readonly string UpdateOperationName = "Update";
    public static readonly string DeleteOperationName = "Delete";

    public static readonly string ProjectAdministratorsRole = "CommentAdministrators";
    public static readonly string ProjectManagersRole = "CommentManagers";
  }
}
