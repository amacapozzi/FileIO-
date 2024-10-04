using Newtonsoft.Json.Linq;
using System;

namespace FileIO
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var response = Api.UploadFile("C:\\Users\\amaca\\Downloads\\AnyDesk.exe").GetAwaiter().GetResult();
            JObject jsonContent = JObject.Parse(response);
            Console.WriteLine("File uploaded, to download file you need this key: {0}", jsonContent["key"].ToString());

            Console.ReadKey();
        }
    }
}