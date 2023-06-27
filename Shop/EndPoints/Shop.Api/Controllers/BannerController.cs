using Common.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Presentation.Facade.SiteEntities.Banner;
using Shop.Query.Categories.DTOs;
using Shop.Query.SiteEntities.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IBannerFacade _bannerFacade;

        public BannerController(IBannerFacade bannerFacade)
        {
            _bannerFacade = bannerFacade;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BannerDto>> GetBannerById(long id)
        {
            var result = await _bannerFacade.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CraeteBanner(CreateBannerCommand command)
        {
            var result = await _bannerFacade.Create(command);
            if (result.Status == OperationResultStatus.Success)
                return Ok();
            else
                return BadRequest(result.Message);
        }
    }



}
