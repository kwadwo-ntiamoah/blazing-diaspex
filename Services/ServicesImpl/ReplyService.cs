using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Constants;
using Data.DTOs;
using Data.Models;
using Data.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using Services.IServices;

namespace Services.ServicesImpl
{
    public class ReplyService: IReplyService
    {
        private readonly IReplyRepo _replyRepo;
        private readonly ILogger<ReplyService> _logger;

        public ReplyService(IReplyRepo replyRepo, ILogger<ReplyService> logger)
        {
            _replyRepo = replyRepo;
            _logger = logger;
        }

        public async Task<ApiResponse<IEnumerable<Reply>>> GetPostReplies(Guid postId)
        {
            var response = new ApiResponse<IEnumerable<Reply>>();

            try
            {
                var replies = await _replyRepo.GetPostRepliesAsync(postId);

                response.Status = System.Net.HttpStatusCode.OK;
                response.Message = "Replies retrieved successfully";
                response.Data = replies;

                return response;   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at ReplyService::GetPostReplies => message::{message}", ex.Message);

                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }

        public async Task<ApiResponse<Reply>> PostReplyAsync(string owner, AddReplyDto model)
        {
            var response = new ApiResponse<Reply>();

            try
            {
                Reply reply = new()
                {
                    PostId = model.PostId,
                    Content = model.Content,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Owner = owner,
                };

                var addedReply = await _replyRepo.PostReplyAsync(reply);

                if (addedReply != null)
                {
                    response.Message = "Reply posted successfully";
                    response.Status = System.Net.HttpStatusCode.Created;
                    response.Data = addedReply;

                    return response;
                }

                response.Message = "Post could not be added. Try again later";
                response.Status = System.Net.HttpStatusCode.BadRequest;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at ReplyService::PostReplyAsync => message::{message}", ex.Message);

                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Message = StringConstants.SERVER_ERROR;

                return response;
            }
        }
    }
}