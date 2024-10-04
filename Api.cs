using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FileIO
{
    internal class Api
    {
        public static readonly string API_URL = "https://file.io";

        public static HttpClient httpClient = new HttpClient()
        {
            DefaultRequestHeaders =
            {
                { "Authorization", $"Bearer {Config.FILE_IO_APILEY}" }
            }
        };

        public static async Task<string> UploadFile(string filePath, bool isPrivate = true, bool autoDelete = true)
        {
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                var fileStreamContent = new StreamContent(File.OpenRead(filePath));
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

                multipartFormContent.Add(fileStreamContent, name: "file", fileName: Path.GetFileName(filePath));

                var response = await httpClient.PostAsync(API_URL, multipartFormContent);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task GetFileByKey(string key)
        {
            if (string.IsNullOrEmpty(key)) return;

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{API_URL}/{key}"))
            {
                var response = await httpClient.SendAsync(request);
                Console.WriteLine(request.Content.ReadAsStringAsync()) ;
            }
        }
    }
}