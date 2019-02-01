﻿using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.WebApi.Features.Contests
{
    public class ContestAdder : IHandler<ContestAdd, ContestAdded>
    {
        private readonly IContestRepository _contestRepository;

        public ContestAdder(
            IContestRepository contestRepository)
        {
            _contestRepository =
                contestRepository;
        }

        public async Task<ContestAdded> ExecuteAsync(
            ContestAdd contestAdd)
        {
            var contest =
                Mapper.Map(contestAdd, new Contest());

            contest = 
                await _contestRepository.AddAsync(
                    contest);

            var contestAdded =
                Mapper.Map(contest, new ContestAdded());

            return contestAdded;
        }
    }
}