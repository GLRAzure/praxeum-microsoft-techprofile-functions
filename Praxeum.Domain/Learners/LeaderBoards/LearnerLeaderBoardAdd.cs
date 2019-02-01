﻿using System;
using System.ComponentModel.DataAnnotations;
using Praxeum.WebApi.Helpers;

namespace Praxeum.Domain.Learners.LeaderBoards
{
    public class LearnerLeaderBoardAdd 
    {
        [Required]
        public Guid LearnerId { get; set; }

        [SwaggerExclude]
        public Guid LeaderBoardId { get; set; }
    }
}