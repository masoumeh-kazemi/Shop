using AutoMapper;
using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Api.ViewModels.Users;
using Shop.Application.Categories.Create;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Users;
using Shop.Query.Categories.DTOs;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers
{

    //[Authorize]
    public class UserController : ApiController
    {

        private readonly IUserFacade _userFacade;
        private readonly IMapper _mapper;
        public UserController(IUserFacade userFacade, IMapper mapper)
        {
            _userFacade = userFacade;
            _mapper = mapper;
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

        //[PermissionChecker(Permission.User_Management)]
        [HttpPost]
        public async Task<ApiResult> CreateUser(CreateUserCommand command)
        {
            var result = await _userFacade.CreateUser(command);
            return CommandResult(result);

        }

        [PermissionChecker(Permission.User_Management)]
        [HttpPut("current")]
        public async Task<ApiResult> EditUser([FromForm] EditUserViewModel command)
        {
            var commandModel = new EditUserCommand(User.GetUserId(), command.Avatar, command.Name
                , command.Family, command.PhoneNumber, command.Email, command.Gender);

            var result = await _userFacade.EditUser(commandModel);
            return CommandResult(result);
        }

        [PermissionChecker(Permission.User_Management)]
        [HttpPut]
        public async Task<ApiResult> Edit([FromForm] EditUserCommand command)
        {
            var result = await _userFacade.EditUser(command);
            return CommandResult(result);
        }

        [HttpPut("ChangePassword")]
        public async Task<ApiResult> ChangePassword(ChangePasswordViewModel command)
        {
            var changePasswordModel = _mapper.Map<ChangeUserPasswordCommand>(command);
            changePasswordModel.UserId = User.GetUserId();
            
            var result = await _userFacade.ChangePassword(changePasswordModel);
            return CommandResult(result);

        }

        [HttpGet("current")]
        public async Task<ApiResult<UserDto>> GetCurrentUser()
        {
            var userId = User.GetUserId();
            var result = await _userFacade.GetUserById(User.GetUserId());
            return QueryResult(result);
        }

        
    }
}
