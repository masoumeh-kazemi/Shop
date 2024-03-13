using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.SiteEntities.Banner;
using Shop.Query.Categories.DTOs;
using Shop.Query.SiteEntities.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [PermissionChecker(Permission.CRUD_Banner)]
    public class BannerController : ApiController
    {
        private readonly IBannerFacade _bannerFacade;

        public BannerController(IBannerFacade bannerFacade)
        {
            _bannerFacade = bannerFacade;
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<BannerDto>> GetBannerById(long id)
        {
            var result = await _bannerFacade.GetById(id);
            return QueryResult(result);
        }

        [HttpGet] 
        public async Task<ApiResult<List<BannerDto>>> GetBanner()
        {
            var result = await _bannerFacade.GetBanners();
            return QueryResult(result);
        }

        [HttpPost]
        public async Task<ApiResult> CreateBanner(CreateBannerCommand command)
        {
            var result = await _bannerFacade.Create(command);
            return CommandResult(result);
        }
    }



}
