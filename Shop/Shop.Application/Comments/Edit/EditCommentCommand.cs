﻿using Common.Application;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.CommentAgg;

namespace Shop.Application.Comments.Edit;

public record EditCommentCommand(long Id, string Text) : IBaseCommand;

public class EditCommentCommandHandler : IBaseCommandHandler<EditCommentCommand>
{
    private readonly ICommentRepository _repository;

    public EditCommentCommandHandler(ICommentRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetTracking(request.Id);
        if (comment == null)
            return OperationResult.NotFound();

        comment.Edit(request.Text);
        await _repository.Save();
        return OperationResult.Success();
    }
}


public class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
{
    public EditCommentCommandValidator()
    {
        RuleFor(r => r.Text)
            .NotNull()
            .MinimumLength(5).WithMessage(ValidationMessages.minLength("متن نظر", 5));
    }
}

   