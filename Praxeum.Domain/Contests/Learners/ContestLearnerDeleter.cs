﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerDeleter : IHandler<ContestLearnerDelete, ContestLearnerDeleted>
    {
       private readonly IMapper _mapper;
        private readonly IContestLearnerRepository _contestLearnerRepository;

        public ContestLearnerDeleter(
            IMapper mapper,
            IContestLearnerRepository contestLearnerRepository)
        {
            _mapper =
                mapper;
            _contestLearnerRepository =
                contestLearnerRepository;
        }

        public async Task<ContestLearnerDeleted> ExecuteAsync(
            ContestLearnerDelete contestLearnerDelete)
        {
            var contestLearner =
                await _contestLearnerRepository.FetchByIdAsync(
                    contestLearnerDelete.ContestId,
                    contestLearnerDelete.Id);

            if (contestLearner == null)
            {
                throw new ArgumentOutOfRangeException($"Contest learner {contestLearnerDelete.Id} not found.");
            }

            contestLearner =
                await _contestLearnerRepository.DeleteByIdAsync(
                    contestLearnerDelete.ContestId,
                    contestLearnerDelete.Id);

            var contestLearnerDeleted =
                _mapper.Map(contestLearner, new ContestLearnerDeleted());

            return contestLearnerDeleted;
        }
    }
}