using PruebaTecnicaRossmon.DTOs;
using System.Net.Http.Headers;

namespace PruebaTecnicaRossmon.Services
{
    public class ProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient("Api");

            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public async Task<List<GetProductDto>> GetProducts() 
        {
            var client = CreateClient();

            var response = await client.GetAsync("Product");

            if (!response.IsSuccessStatusCode)
                return new List<GetProductDto>();

            var data = await response.Content.ReadFromJsonAsync<List<GetProductDto>>();

            return data ?? new List<GetProductDto>();
        }

        public async Task<bool> CreateProduct(CreateProductDto dto)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("Product", dto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProduct(UpdateProductDto dto)
        {
            var client = CreateClient();

            var response = await client.PutAsJsonAsync($"Product/{dto.Id}", dto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var client = CreateClient();

            var response = await client.DeleteAsync($"Product/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
