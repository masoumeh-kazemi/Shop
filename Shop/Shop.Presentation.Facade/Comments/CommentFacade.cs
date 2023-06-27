using Common.Application;
using MediatR;
using Shop.Application.Comments.ChangesStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Query.Comments.DTOs;
using Shop.Query.Comments.GetByFilter;
using Shop.Query.Comments.GetById;

namespace Shop.Presentation.Facade.Comments;

public class CommentFacade : ICommentFacade
{
    private readonly Mediator _mediator;

    public CommentFacade(Mediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> CreateComment(CreateCommentCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditComment(EditCommentCommand command)
    {
        return await _mediator.Send(command);

    }

    public async Task<OperationResult> ChangeCommentStatus(ChangeCommentStatusCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<CommentDto?> GetCommentById(long id)
    {
        return await _mediator.Send(new GetCommentByIdQuery(id));
    }

    public async Task<CommentFilterResult?> GetCommentFilterById(CommentFilterParams filterParams)
    {
        return await _mediator.Send(new GetCommentByFilterQuery(filterParams));
    }
}