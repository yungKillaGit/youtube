﻿using System;
using System.Collections.Generic;
using System.Text;
using Youtube.Api.Core.Dto;
using Youtube.Api.Core.Dto.Entities;
using Youtube.Api.Core.Dto.UseCaseRequests;
using Youtube.Api.Core.Dto.UseCaseResponses;
using Youtube.Api.Core.Interfaces;
using Youtube.Api.Core.Interfaces.Gateways.Repositories;
using Youtube.Api.Core.Interfaces.UseCases;

namespace Youtube.Api.Core.UseCases
{
    public class NewCommentUseCase : INewCommentUseCase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVideoRepository _videoRepository;

        public NewCommentUseCase(ICommentRepository commentRepository, IUserRepository userRepository, IVideoRepository videoRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _videoRepository = videoRepository;
        }

        public bool Handle(NewCommentRequest useCaseRequest, IOutputPort<NewCommentResponse> outputPort)
        {
            if (_userRepository.FindById(useCaseRequest.UserId) == null)
            {
                outputPort.Handle(new NewCommentResponse(new[] { new Error(404, "user not found") }));
                return false;
            }
            if (_videoRepository.FindById(useCaseRequest.VideoId) == null)
            {
                outputPort.Handle(new NewCommentResponse(new[] { new Error(404, "video not found") }));
                return false;
            }
            var commentInfo = new CommentDto()
            {
                PostingDate = useCaseRequest.PostingDate,
                Text = useCaseRequest.Text,
                UserId = useCaseRequest.UserId,
                VideoId = useCaseRequest.VideoId,
            };
            int commentId = _commentRepository.Create(commentInfo);
            outputPort.Handle(new NewCommentResponse(commentId));

            return true;
        }
    }
}
