using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FileIO
{
    internal class Api
    {
        public static readonly string API_URL = "https://file.io/";

        public static HttpClient httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(30),
            DefaultRequestHeaders =
            {
                { "Authorization", $"Bearer {Config.FILE_IO_APILEY}" }
            }
        };

        public static async Task<string> UploadFile(string filePath)
        {
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                var fileStreamContent = new StreamContent(File.OpenRead(filePath));
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

                multipartFormContent.Add(fileStreamContent, name: "file", fileName: Path.GetFileName(filePath));

                var response = await httpClient.PostAsync(API_URL, multipartFormContent);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}