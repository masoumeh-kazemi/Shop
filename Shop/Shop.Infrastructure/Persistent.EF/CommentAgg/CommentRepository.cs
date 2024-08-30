using System.Linq.Expressions;
using Common.Domain.Repository;
using Shop.Domain.CommentAgg;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.EF.CommentAgg;

public class CommentRepository : BaseRepository<Comment> , ICommentRepository
{
    public CommentRepository(ShopContext context) : base(context)
    {
    }

    public void Delete(Comment comment)
    {
        Context.Comments.Remove(comment);
    }
}

