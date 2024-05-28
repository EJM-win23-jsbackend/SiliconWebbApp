using BlazorWebbApp.Entities;
using BlazorWebbApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BlazorWebbApp.Services
{
    public class SubscriberServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IOptions<ConnectionStrings> _options;

        public SubscriberServices(HttpClient httpClient, IConfiguration configuration, IOptions<ConnectionStrings> options)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _options = options;
        }

        public async Task<IActionResult> SubscribeAsync(SubscribeEntity subscribe)
        {
            try 
            {
                var connectionString = _options.Value.SubscribeFunction;
                var result = await _httpClient.PostAsJsonAsync(connectionString, subscribe);

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
                var connectionString = _options.Value.UnsubscribeFunction;
                var result = await _httpClient.PostAsJsonAsync(connectionString, subscribe);

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
                var connectionString = _options.Value.GetASubscriberFunction;
                var result = await _httpClient.PostAsJsonAsync(connectionString, userId);

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
