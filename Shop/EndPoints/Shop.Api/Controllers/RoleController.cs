using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Roles;
using Shop.Query.Roles.DTOs;

namespace Shop.Api.Controllers
{

    public class RoleController : ApiController
    {
        private readonly IRoleFacade _roleFacade;

        public RoleController(IRoleFacade roleFacade)
        {
            _roleFacade = roleFacade;
        }

        [HttpPost]
        public async Task<ApiResult> CreateRole(CreateRoleCommand command)
        {
            var result = await _roleFacade.CreateRole(command);
            return CommandResult(result);

        }
        [HttpPut]
        public async Task<ApiResult> EditRole(EditRoleCommand command)
        {
            var result = await _roleFacade.EditRole(command);
            return CommandResult(result);

        }

        [HttpGet("{Id}")]
        public async Task<ApiResult<RoleDto?>> GetRole(long id)
        {
            var result = await _roleFacade.GetRoleById(id);
            return QueryResult(result);

        }

        [HttpGet]
        public async Task<ApiResult<List<RoleDto>>> GetRoles()
        {
            var result = await _roleFacade.GetRoles();
            var queryResult = QueryResult(result);
            return queryResult;
        }
    }
}
