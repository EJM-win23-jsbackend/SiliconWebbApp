using Azure;
using BlazorWebbApp.Entities;
using BlazorWebbApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BlazorWebbApp.Services
{
    public class SubscriberServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SubscriberServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> SubscribeAsync(SubscribeEntity subscribe)
        {
            try 
            {
                var result = await _httpClient.PostAsJsonAsync(_configuration.GetConnectionString("SubscribeFunction"), subscribe);

                var content = await result.Content.ReadAsStringAsync();

                var json = JsonConvert.DeserializeObject<SubscribeResponseMessage>(content);

                if (result.IsSuccessStatusCode)
                {
                    return new OkObjectResult(new { Status = 200, Message = $"{json!.Message}"});
                }
                if(result.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    return new ConflictObjectResult(new { Status = 409, Message = $"{json!.Message}"});
                }

                return new BadRequestObjectResult(new { Status = 400, Message = "Something went wrong" });
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("SubscribeService Subscribe ::" + ex.Message);
                return new BadRequestObjectResult(new { Status = 400, Message = "Something went wrong" });
            }
        }

        public async Task<IActionResult> UnsubscribeAsync(SubscribeEntity subscribe)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync(_configuration.GetConnectionString("UnsubscribeFunction"), subscribe);

                var response = await result.Content.ReadFromJsonAsync<SubscribeResponseMessage>();

                if (result.IsSuccessStatusCode)
                {
                    return new OkObjectResult(new { Status = 200, Message = $"{response.Message}" });
                }
                if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    return new ConflictObjectResult(new { Status = 409, Message = $"{response.Message}" });
                }
                if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new NotFoundObjectResult(new { Status = 404, Message = $"{response.Message}" });
                }
                return new BadRequestObjectResult(new { Status = 400, Message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SubscribeService Subscribe ::" + ex.Message);
                return new BadRequestObjectResult(new { Status = 400, Message = "Something went wrong" });
            }
        }

        public async Task<IActionResult> GetASubscriberAsync(string userId)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync(_configuration.GetConnectionString("GetASubscriberFunction"), userId);

                var content = await result.Content.ReadAsStringAsync();

                var entity = JsonConvert.DeserializeObject<SubscribeEntity>(content);

                if (result.IsSuccessStatusCode)
                {
                        return new OkObjectResult(entity);
                }
                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SubscribeService Subscribe ::" + ex.Message);
                return new BadRequestResult();
            }
        }
    }
}
