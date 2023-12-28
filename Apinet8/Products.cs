using Api;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Apinet8
{
    public class Products
    {

        private readonly ILogger _logger;

        private readonly IProductData productData;

        public Products(ILoggerFactory loggerFactory, IProductData productData)
        {
            this.productData = productData;
            _logger = loggerFactory.CreateLogger<Products>();
        }

        [Function("Products")]
        public async Task<IActionResult> Run( [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest req/*,
            ILogger log = _logger*/)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            //var product = JsonSerializer.Deserialize<Product>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var newProduct = await productData.GetProducts();//.AddProduct(product);
            return new OkObjectResult(newProduct);
        }
    }
}
