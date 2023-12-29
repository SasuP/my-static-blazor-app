using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// Microsoft.Azure.WebJobs;
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
    public class ProductsPost
    {
        private readonly ILogger _logger;

        //public ProductsPost(ILoggerFactory loggerFactory)
        //{
        //    _logger = loggerFactory.CreateLogger<ProductsPost>();
        //}

        private readonly IProductData productData;

        public ProductsPost(ILoggerFactory loggerFactory, IProductData productData)
        {
            this.productData = productData;
            _logger = loggerFactory.CreateLogger<ProductsPost>();
        }

        [Function("ProductsPost")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products")] HttpRequest req/*,
            ILogger log = _logger*/)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var product = JsonSerializer.Deserialize<Product>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var newProduct = await productData.AddProduct(product);
            return new OkObjectResult(newProduct);
        }
    }
}
