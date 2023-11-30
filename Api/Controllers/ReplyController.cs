using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReplyController : ControllerBase
    {
        private readonly IReplyService _replyService;

        public ReplyController(IReplyService replyService)
        {
            _replyService = replyService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Reply>>> PostReplyAsync(AddReplyDto model) {
            var owner = User.Claims.First(x => x.Type.Equals(ClaimTypes.Email)).Value;
            return await _replyService.PostReplyAsync(owner, model);
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Reply>>>> GetPostRepliesAsync(Guid postId) {
            return await _replyService.GetPostReplies(postId);
        }
    }
}