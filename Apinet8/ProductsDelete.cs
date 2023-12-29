//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
////using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.Azure.Functions.Worker.Http;

using System.Net;
using Apinet8;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class ProductsDelete
    {
        private readonly ILogger _logger;

        //public ProductsDelete(ILoggerFactory loggerFactory)
        //{
        //    _logger = loggerFactory.CreateLogger<ProductsDelete>();
        //}

        private readonly IProductData productData;

        public ProductsDelete(ILoggerFactory loggerFactory, IProductData productData)
        {
            this.productData = productData;
            _logger = loggerFactory.CreateLogger<ProductsDelete>();
        }

        [Function("ProductsDelete")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "products/{productId:int}")] HttpRequest req,
            int productId/*,
            ILogger log*/)
        {
            var result = await productData.DeleteProduct(productId);

            if (result)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}
