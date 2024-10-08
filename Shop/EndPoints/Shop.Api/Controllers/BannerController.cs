﻿using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Delete;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.SiteEntities.Banner;
using Shop.Query.Categories.DTOs;
using Shop.Query.SiteEntities.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[PermissionChecker(Permission.CRUD_Banner)]
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
        public async Task<ApiResult> CreateBanner([FromForm] CreateBannerCommand command)
        {
            var result = await _bannerFacade.Create(command);
            return CommandResult(result);
        }

        [HttpPut]
        public async Task<ApiResult> EditBanner([FromForm] EditBannerCommand command)
        {
            var result = await _bannerFacade.Edit(command);
            return CommandResult(result);
        }

        [HttpDelete("{bannerId}")]
        public async Task<ApiResult> DeleteBanner(long bannerId)
        {
            var result = await _bannerFacade.Delete(new DeleteBannerCommand(bannerId));
            return CommandResult(result);

        }
    }



}
