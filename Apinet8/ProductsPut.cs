using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;
using Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api
{
    public class ProductsPut
    {
        private readonly ILogger _logger;

        //public ProductsPut(ILoggerFactory loggerFactory)
        //{
        //    _logger = loggerFactory.CreateLogger<ProductsPut>();
        //}

        private readonly IProductData productData;

        public ProductsPut(ILoggerFactory loggerFactory, IProductData productData)
        {
            this.productData = productData;
            _logger = loggerFactory.CreateLogger<ProductsPost>();
        }

        [Function("ProductsPut")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "products")] HttpRequest req/*,
            ILogger log*/)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var product = JsonSerializer.Deserialize<Product>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var updatedProduct = await productData.UpdateProduct(product);
            return new OkObjectResult(updatedProduct);
        }
    }
}
