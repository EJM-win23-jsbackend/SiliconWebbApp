using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebbApp.Services
{
    public class ProfileImageServices
    {
        private readonly HttpClient _httpClient;

        public ProfileImageServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> UploadProfileImageAsync(IBrowserFile model, string userId)
        {
            try
            {
                if (model != null && userId != null)
                {
                    var content = new MultipartFormDataContent();

                    using (var fileStream = model.OpenReadStream())
                    {
                        var streamContent = new StreamContent(fileStream);

                        // Kontrollera om ContentType är null innan du använder det
                        if (!string.IsNullOrEmpty(model.ContentType))
                        {
                            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.ContentType);
                        }

                        content.Add(streamContent, "file", model.Name);

                        var response = await _httpClient.PostAsync($"http://localhost:7181/api/uploadprofilepicture/{userId}", content);

                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            return new OkObjectResult(result);
                        }
                    }
                }

                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image: {ex.Message}");
                return new BadRequestResult();
            }
        }

        public async Task<string> DownloadProfileImageAsync(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:7181/api/getfile/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var imageBytes = await response.Content.ReadAsByteArrayAsync();
                    var base64String = Convert.ToBase64String(imageBytes);
                    var contentType = response.Content.Headers.ContentType.ToString();
                    var imageBase64 = $"data:{contentType};base64,{base64String}";

                    return imageBase64;
                }

                return null!; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching image: {ex.Message}");
                return null!;            
            }
        }
    }
}
