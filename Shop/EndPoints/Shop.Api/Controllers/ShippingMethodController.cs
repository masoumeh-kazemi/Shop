using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.SiteEntities.ShippingMethods.Create;
using Shop.Application.SiteEntities.ShippingMethods.Edit;
using Shop.Presentation.Facade.SiteEntities.ShippingMethods;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Api.Controllers
{


    [Authorize]
    public class ShippingMethodController : ApiController
    {
        private readonly IShippingMethodFacade _shippingMethodFacade;

        public ShippingMethodController(IShippingMethodFacade shippingMethodFacade)
        {
            _shippingMethodFacade = shippingMethodFacade;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<ShippingMethodDto>>> GetList()
        {
            var result = await _shippingMethodFacade.GetList();
            return QueryResult(result);
        }
        [HttpGet("{id}")]
        public async Task<ApiResult<ShippingMethodDto>> GetById(long id)
        {
            var result = await _shippingMethodFacade.GetById(id);
            return QueryResult(result);
        }


        [HttpPost]
        public async Task<ApiResult> Create(CreateShippingMethodCommand command)
        {
            var result = await _shippingMethodFacade.Create(command);
            return CommandResult(result);
        }

        [HttpPut]
        public async Task<ApiResult> Edit(EditShippingMethodCommand command)
        {
            var result = await _shippingMethodFacade.Edit(command);
            return CommandResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            var result = await _shippingMethodFacade.Delete(id);
            return CommandResult(result);
        }


    }
}
