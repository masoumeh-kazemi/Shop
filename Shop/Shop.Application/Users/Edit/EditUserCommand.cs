using System.Runtime.InteropServices.ComTypes;
using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Edit;

public class EditUserCommand : IBaseCommand
{
    public EditUserCommand(long userId, IFormFile? avatar, string name, string family, string phoneNumber
        , string email, Gender gender)
    {
        UserId = userId;
        Avatar = avatar;
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }
    public long UserId { get; private set; }
    public IFormFile? Avatar { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public Gender Gender { get; private set; }
}

public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;
    private readonly IFileService _fileService;
    public EditUserCommandHandler(IUserRepository repository, IUserDomainService domainService, IFileService fileService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();
        var oldAvatarName = user.AvatarName;
        user.Edit(request.Name, request.Family, request.PhoneNumber, request.Email, request.Gender, _domainService);
        if (request.Avatar != null)
        {
            var avatarName = await _fileService.SaveFileAndGenerateName(request.Avatar, Directories.UserAvatars);
            user.SetAvatar(avatarName);
        }

        DeleteOldAvatar(request.Avatar, oldAvatarName);
        await _repository.Save();
        return OperationResult.Success();
    }


    private void DeleteOldAvatar(IFormFile? avatarFile, string oldImage)
    {
        if (avatarFile == null || oldImage == "avatar.png")
            return;
        _fileService.DeleteFile(Directories.UserAvatars, oldImage);

    }
}

public class EditUserCommandValidator
{

}