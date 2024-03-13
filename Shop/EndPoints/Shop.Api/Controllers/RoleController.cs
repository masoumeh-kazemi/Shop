using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Roles.Create;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Roles;

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
    }
}
