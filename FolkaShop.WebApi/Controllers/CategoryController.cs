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
    public class CategoryController : ControllerBase
    {
        ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            try
            {
                var data = await _categoryRepository.GetCategory();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var data = await _categoryRepository.GetCategory(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            try
            {
                var result = await _categoryRepository.AddCategory(category);
                return Ok(new { status = "success", result = result, message = "Saving Data Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Save Data : " + ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryRepository.DeleteCategory(id);
                return Ok(new { status = "success", result = result, message = "Deleting Data Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Delete Data : " + ex.Message });
            }
        }
    }
}
