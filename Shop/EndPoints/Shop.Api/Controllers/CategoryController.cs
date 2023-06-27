using System;
using System.Net;
using AngleSharp.Dom;
using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Categories.DTOs;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApiController
    {
        private readonly ICategoryFacade _categoryFacade;
        public CategoryController(ICategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        [HttpGet]
        public async Task<ApiResult<List<CategoryDto>>> GetCategories()
        {
            var result = await _categoryFacade.GetCategories();

            return QueryResult(result);
        }


        [HttpGet("{id}")]
        public async Task<ApiResult<CategoryDto>> GetCategoryById(long id)
        {
            var result = await _categoryFacade.GetCategoryById(id);
            return QueryResult(result);
        }

        [HttpGet("getChild/{parentId}")]
        public async Task<ApiResult<List<ChildCategoryDto>>> GetCategoriesByParentId(long parentId)
        {
            var result = await _categoryFacade.GetCategoriesByParentId(parentId);
            return QueryResult(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        //{
        //    var result = await _categoryFacade.CreateCategory(command);
        //    if (result.Status == OperationResultStatus.Success)
        //        return Ok();
        //    else
        //        return BadRequest(result.Message);
        //}


        [HttpPost]
        public async Task<ApiResult<long>> CreateCategory(CreateCategoryCommand command)
        {
            var result = await _categoryFacade.CreateCategory(command);
            var url = Url.Action("GetCategoryById", "Category", new { id = result.Data }, Request.Scheme);

            return CommandResult(result,HttpStatusCode.Created, url);
        }

        [HttpPost("AddChild")]
        public async Task<ApiResult<long>> AddChildCategory(AddChildCategoryCommand command)
        {
            var result = await _categoryFacade.AddChildCategory(command);
            var url = Url.Action("GetCategoryById", "Category", new { id = result.Data }, Request.Scheme);
            return CommandResult(result, HttpStatusCode.Created, url);
        }

        [HttpPut]
        public async Task<ApiResult> EditCategory(EditCategoryCommand command)
        {
            var result = await _categoryFacade.EditCategory(command);
            return CommandResult(result);

        }

        [HttpDelete]
        public async Task<ApiResult> RemoveCategory(long categoryId)
        {
            var result = await _categoryFacade.RemoveCategory(categoryId);
            return CommandResult(result);

        }
    }

}
 