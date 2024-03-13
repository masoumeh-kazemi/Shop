using Common.Application;
using Shop.Application.Comments.ChangesStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Query.Comments.DTOs;

namespace Shop.Presentation.Facade.Comments;

public interface ICommentFacade
{
    Task<OperationResult> CreateComment(CreateCommentCommand command);
    Task<OperationResult> EditComment(EditCommentCommand command);
    Task<OperationResult> ChangeStatus(ChangeCommentStatusCommand command);

    Task<CommentDto?> GetCommentById(long id);
    Task<CommentFilterResult?>  GetCommentsByFilter(CommentFilterParams filterParams);

}