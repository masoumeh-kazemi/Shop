using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Categories.Create;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Users;
using Shop.Query.Categories.DTOs;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers
{

    [Authorize]
    public class UserController : ApiController
    {

        private readonly IUserFacade _userFacade;

        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [PermissionChecker(Permission.User_Management)]
        [HttpGet]
        public async Task<ApiResult<UserFilterResult>> GetUsers([FromQuery] UserFilterParams filterParams)
        {
            var result = await _userFacade.GetUserByFilter(filterParams);
            return QueryResult(result);
        }

        [PermissionChecker(Permission.User_Management)]
        [HttpGet("{id}")]
        public async Task<ApiResult<UserDto?>> GetCategoryById(long id)
        {
            var result = await _userFacade.GetUserById(id);
            return QueryResult(result);
        }

        [PermissionChecker(Permission.User_Management)]
        [HttpPost]
        public async Task<ApiResult> CreateUser(CreateUserCommand command)
        {
            var result = await _userFacade.CreateUser(command);
            return CommandResult(result);

        }

        [PermissionChecker(Permission.User_Management)]
        [HttpPut]
        public async Task<ApiResult> EditUser([FromForm] EditUserCommand command)
        {
            var result = await _userFacade.EditUser(command);
            return CommandResult(result);
        }

        [HttpGet("currentN")]
        public async Task<ApiResult<UserDto>> GetCurrentUser()
        {
            var result = await _userFacade.GetUserById(User.GetUserId());
            return QueryResult(result);
        }
    }
}
