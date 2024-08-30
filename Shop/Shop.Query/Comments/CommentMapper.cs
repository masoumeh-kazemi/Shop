using System.ComponentModel;
using Shop.Domain.CommentAgg;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Comments.DTOs;
using Shop.Query.Users;

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
            UserId = comment.UserId,
            
            

        };
    }

    public static string GetUserFullName(this CommentDto commentDto,ShopContext context )
    {
        var user =  context.Users.FirstOrDefault(f => f.Id == commentDto.UserId).Map();
        var userFullName = user.Name +" " + user.Family;
        commentDto.UserFullName = userFullName;
        return commentDto.UserFullName;
    }
}