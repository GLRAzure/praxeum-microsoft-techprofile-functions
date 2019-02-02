﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.LeaderBoards
{
    public class LeaderBoardUpdate 
    {
        [SwaggerExclude]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}