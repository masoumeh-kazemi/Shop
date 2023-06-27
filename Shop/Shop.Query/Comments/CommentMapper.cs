using System.ComponentModel;
using Shop.Domain.CommentAgg;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments;

public static class CommentMapper
{
    public static CommentDto Map(this Comment comment)
    {
        return new CommentDto()
        {
            CreationDate = comment.CreationDate,
            Id = comment.Id,
            ProductId = comment.ProductId,
            ProductTitle = "",
            Text = comment.Text,
            UserId = comment.UserId
        };
    }
}