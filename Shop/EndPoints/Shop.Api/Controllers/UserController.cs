using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Categories.Create;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Presentation.Facade.Users;
using Shop.Query.Categories.DTOs;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers
{

    public class UserController : ApiController
    {

        private readonly IUserFacade _userFacade;

        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        [HttpGet]
        public async Task<ApiResult<UserFilterResult>> GetUsersByFilter([FromQuery] UserFilterParams filterParams)
        {
            var result = await _userFacade.GetUserByFilter(filterParams);
            return QueryResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<UserDto?>> GetCategoryById(long id)
        {
            var result = await _userFacade.GetUserById(id);
            return QueryResult(result);
        }


        [HttpPost]
        public async Task<ApiResult> CreateUser(CreateUserCommand command)
        {
            var result = await _userFacade.CreateUser(command);
            return CommandResult(result);

        }

        [HttpPut]
        public async Task<ApiResult> EditUser([FromForm] EditUserCommand command)
        {
            var result = await _userFacade.EditUser(command);
            return CommandResult(result);
        }
    }
}
