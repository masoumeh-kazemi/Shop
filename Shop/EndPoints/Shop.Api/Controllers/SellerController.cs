using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Application.Sellers.EditInventory;
using Shop.Presentation.Facade.Sellers;
using Shop.Query.Sellers.DTOs;

namespace Shop.Api.Controllers
{

    public class SellerController : ApiController
    {
        private readonly ISellerFacade _sellerFacade;

        public SellerController(ISellerFacade sellerFacade)
        {
            _sellerFacade = sellerFacade;
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<SellerDto>> GetSellerById(long id)
        {
            var result = await _sellerFacade.GetSellerById(id);
            return QueryResult(result);
        }
        [HttpGet]
        public async Task<ApiResult<SellerFilterResult>> GetSellerList(SellerFilterParams filterParams)
        {
            var result = await _sellerFacade.GetSellerByFilter(filterParams);
            return QueryResult(result);
        }

        [HttpPost]
        public async Task<ApiResult> CreateSeller(CreateSellerCommand command)
        {
            var result = await _sellerFacade.CreateSeller(command);
            return CommandResult(result);
        }

        [HttpPut]

        public async Task<ApiResult> EditSeller(EditSellerCommand command)
        {
            var result = await _sellerFacade.EditSeller(command);
            return CommandResult(result);
        }

        [HttpPost("Inventory")]
        public async Task<ApiResult> CreateSellerInventory(AddSellerInventoryCommand command)
        {
            var result = await _sellerFacade.AddSellerInventory(command);
            return CommandResult(result);
        }

        [HttpPut("Inventory")]
        public async Task<ApiResult> EditSellerInventory(EditSellerInventoryCommand command)
        {
            var result = await _sellerFacade.EditSellerInventory(command);
            return CommandResult(result);
        }

    }
}
