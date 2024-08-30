using Common.Domain.Repository;

namespace Shop.Domain.CommentAgg;

public interface ICommentRepository : IBaseRepository<Comment>
{
    void Delete(Comment comment);
}