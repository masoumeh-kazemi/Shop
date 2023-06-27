using Common.Application;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Enums;
using Shop.Domain.RoleAgg.Repository;

namespace Shop.Application.Roles.Edit;

public record EditRoleCommand(long Id, string Title, List<Permission> Permissions) : IBaseCommand;

public class EditRoleCommandHandler : IBaseCommandHandler<EditRoleCommand>
{
    private readonly IRoleRepository _repository;

    public EditRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _repository.GetTracking(request.Id);
        if (role == null)
            return OperationResult.NotFound();

        role.Edit(request.Title);
        var rolePermissions = new List<RolePermission>();
        request.Permissions.ForEach(permission =>
        {
            rolePermissions.Add(new RolePermission(permission));
        });

        role.SetPermissions(rolePermissions);
        await _repository.Save();
        return OperationResult.Success();
    }
}