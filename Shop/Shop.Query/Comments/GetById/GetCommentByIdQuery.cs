using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.GetById;

public record GetCommentByIdQuery(long Id) : IQuery<CommentDto>;


public class GetCommentByIdQueryHandler : IQueryHandler<GetCommentByIdQuery, CommentDto>
{
    private readonly ShopContext _context;

    public GetCommentByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<CommentDto> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken: cancellationToken);
        if (comment == null)
            return null;

        return comment.Map();
    }
}