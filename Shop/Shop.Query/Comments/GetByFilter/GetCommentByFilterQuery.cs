using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.CommentAgg;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.GetByFilter;

public class GetCommentByFilterQuery : QueryFilter<CommentFilterResult, CommentFilterParams>
{
    public GetCommentByFilterQuery(CommentFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetCommentByFilterQueryHandler : IQueryHandler<GetCommentByFilterQuery, CommentFilterResult>
{
    private readonly ShopContext _context;

    public GetCommentByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
    {
        var @param = request.FilterParams;
        var result = _context.Comments.OrderByDescending(f=>f.Id).AsQueryable();

        if (@param.ProductId != null)
            result = result.Where(f => f.ProductId == @param.ProductId);

        if (@param.CommentStatus != null)
            result = result.Where(f => f.Status == @param.CommentStatus);

        if (@param.UserId != null)
            result = result.Where(f => f.UserId == @param.UserId);

        if (@param.EndDate != null)
            result = result.Where(f => f.CreationDate.Date == @param.EndDate);

        if (@param.StartDate != null)
            result = result.Where(f => f.CreationDate.Date == @param.StartDate);

        var skip = (@param.PageId - 1) * @param.Take;
        var model = new CommentFilterResult()
        {
            Data = await result.Skip(skip).Take(@param.Take)
                .Select(comment => comment.Map()).ToListAsync(cancellationToken: cancellationToken),

            FilterParams = @param,
        };
        model.Data.ForEach(comment=>comment.UserFullName = comment.GetUserFullName(_context));

        model.GeneratePaging(result, @param.Take, @param.PageId);

        return model;
    }
}