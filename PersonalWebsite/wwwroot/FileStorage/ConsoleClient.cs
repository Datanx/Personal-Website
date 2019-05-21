///////////////////////////////////////////////////////////////
// ConsoleClient.cs - Client for WebApi FilesController      //
//                                                           //
// Jim Fawcett, CSE686 - Internet Programming, Spring 2019   //
///////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ConsoleClient
{
  class ConsoleClient
  {
    public HttpClient client { get; set; }

    private string baseUrl_;

    ConsoleClient(string url)
    {
      baseUrl_ = url;
      client = new HttpClient();
    }

    public async Task<HttpResponseMessage> SendFile(string fileSpec)
    {
      MultipartFormDataContent multiContent = new MultipartFormDataContent();

      byte[] data = File.ReadAllBytes(fileSpec);
      ByteArrayContent bytes = new ByteArrayContent(data);
      string fileName = Path.GetFileName(fileSpec);
      multiContent.Add(bytes, "files", fileName);

      return await client.PostAsync(baseUrl_, multiContent);
    }

    static void Main(string[] args)
    {
      Console.WriteLine("Press key to start: ");
      Console.ReadKey();

      string url = "https://localhost:44342/api/Files";
      ConsoleClient client = new ConsoleClient(url);
      Task<HttpResponseMessage> t = client.SendFile("../../../ConsoleClient.cs");
      var result = t.Result;
      Console.WriteLine("\n  result = \"{0}\"", result);
      Console.WriteLine("Press Key to exit: ");
      Console.ReadKey();
    }
  }
}
