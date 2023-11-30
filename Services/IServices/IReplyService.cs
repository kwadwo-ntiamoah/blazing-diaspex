using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Models;

namespace Services.IServices
{
    public interface IReplyService
    {
        public Task<ApiResponse<Reply>> PostReplyAsync(string owner, AddReplyDto reply);
        public Task<ApiResponse<IEnumerable<Reply>>> GetPostReplies(Guid postId);
    }
}