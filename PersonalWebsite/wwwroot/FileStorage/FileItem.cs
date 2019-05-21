using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiLect13.Models
{
  public class FileItem
  {
    public long FileItemId { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public long? TopicId { get; set; }
    public Topic topic { get; set; }
  }

  public class Topic
  {
    public long TopicId { get; set; }
    public string Value { get; set; }
  }
}
