using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Rabbit.Mvc.Models.Dto;

namespace Rabbit.Mvc.Services
{
    public class TransferService : ITransferService
    {
        private HttpClient _apiClient;

        public TransferService(HttpClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public async Task Transfer(TransferDto transferDto)
        {
            var uri = "https://localhost:5001/api/Banking";
            var transferContent = new StringContent(JsonSerializer.Serialize(transferDto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, transferContent);
            response.EnsureSuccessStatusCode();
        }
    }
}