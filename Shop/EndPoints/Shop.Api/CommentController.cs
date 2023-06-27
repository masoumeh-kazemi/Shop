using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Comments.ChangesStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Presentation.Facade.Comments;
using Shop.Query.Comments.DTOs;

namespace Shop.Api
{
    public class CommentController : ApiController
    {
        private readonly ICommentFacade _commentFacade;

        public CommentController(ICommentFacade commentFacade)
        {
            _commentFacade = commentFacade;
        }

        [HttpPost]
        public async Task<ApiResult> CreateComment(CreateCommentCommand command)
        {
            var result = await _commentFacade.CreateComment(command);
            return CommandResult(result);
        }

        [HttpGet("{commentId}")]
        public async Task<ApiResult<CommentDto?>> GetCommentById(long commentId)
        {
            var result = await _commentFacade.GetCommentById(commentId);
            return QueryResult(result);
        }

        [HttpGet]
        public async Task<ApiResult<CommentFilterResult?>> GetCommentByFilter([FromQuery] CommentFilterParams filterParams)
        {
            var result = await _commentFacade.GetCommentFilterById(filterParams);
            return QueryResult(result);
        }

        [HttpPut]
        public async Task<ApiResult> EditComment(EditCommentCommand command)
        {
            var result = await _commentFacade.EditComment(command);
            return CommandResult(result);
        }

        [HttpPut("ChangeStatus")]
        public async Task<ApiResult> ChangeCommentStatus(ChangeCommentStatusCommand command)
        {
            var result = await _commentFacade.ChangeCommentStatus(command);
            return CommandResult(result);
        }
    }
}
