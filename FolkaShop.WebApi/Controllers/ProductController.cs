using FolkaShop.WebApi.Data.Repository.Interface;
using FolkaShop.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FolkaShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            try
            {
                var data = await _productRepository.GetProduct();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        [HttpGet("{categoryId}")]
        public IActionResult GetProduct(int categoryId)
        {
            try
            {
                var data = _productRepository.GetProduct(categoryId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTO productDTO)
        {
            try
            {
                int categoryId = productDTO.CategoryId;
                var product = new Product();
                product.SKU = int.Parse(GetNumberSKU(categoryId));
                product.CategoryId = categoryId;
                product.Name = productDTO.Name;
                product.Description = productDTO.Description;
                product.Price = productDTO.Price;
                var result = await _productRepository.AddProduct(product);
                return Ok(new { status = "success", result = result, message = "Saving Data Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Save Data : " + ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int sku)
        {
            try
            {
                var result = await _productRepository.DeleteProduct(sku);
                return Ok(new { status = "success", result = result, message = "Deleting Data Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Delete Data : " + ex.Message });
            }
        }

        private string GetNumberSKU(int categoryId)
        {
            int counter;
            string number;

            var result = _productRepository.GetProduct().Result.Where(p => p.SKU.ToString().StartsWith(categoryId.ToString()));    
            if (result.Count() > 0)
            {
                var data = result.Select(x => x.SKU).Max();
                counter = int.Parse(data.ToString().Substring(1, 4)) + 1;
                string joinstr = "0000" + counter;
                number = string.Concat(categoryId, joinstr.Substring(joinstr.Length - 4, 4));
            }
            else
            {
                number = string.Concat(categoryId, "0001");
            }

            return number;
        }
    }
}
