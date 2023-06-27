﻿using Common.Application;
using Shop.Domain.CommentAgg;

namespace Shop.Application.Comments.ChangesStatus;

public record ChangeCommentStatusCommand(long Id, CommentStatus Status) : IBaseCommand;


public class ChangeCommentStatusCommandHandler : IBaseCommandHandler<ChangeCommentStatusCommand>
{
    private readonly ICommentRepository _repository;

    public ChangeCommentStatusCommandHandler(ICommentRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(ChangeCommentStatusCommand request, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetTracking(request.Id);
        if (comment == null)
            return OperationResult.NotFound();

        comment.ChangeStatus(request.Status);
        await _repository.Save();
        return OperationResult.Success();
    }
}