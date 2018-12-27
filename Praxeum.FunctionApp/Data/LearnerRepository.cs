﻿using Microsoft.Azure.Cosmos;
using Praxeum.FunctionApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Data
{
    public class LearnerRepository : AzureCosmosDbRepository
    {
        public LearnerRepository() : base(new AzureCosmosDbOptions())
        {
        }

        public LearnerRepository(
            AzureCosmosDbOptions azureCosmosDbOptions) : base(azureCosmosDbOptions)
        {
        }

        public async Task<Learner> AddAsync(
            Learner learner)
        {
            var learnerContainer =
               _cosmosDatabase.Containers["learners"];

            var learnerDocument =
                await learnerContainer.Items.CreateItemAsync<Learner>(
                    learner.Id.ToString(),
                    learner);

            return learnerDocument.Resource;
        }

        public async Task<Learner> FetchAsync(
            string userName)
        {
            var learnerContainer =
                _cosmosDatabase.Containers["learners"];

            var query =
                $"SELECT * FROM l WHERE l.userName = @userName";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            queryDefinition.UseParameter(
                "@userName", userName);

            var learners =
                learnerContainer.Items.CreateItemQuery<Learner>(queryDefinition, maxConcurrency: 2);

            var learnerList = 
                new List<Learner>();

            while (learners.HasMoreResults)
            {
                learnerList.AddRange(
                    await learners.FetchNextSetAsync());
            };

            return learnerList.FirstOrDefault();
        }

        public async Task<Learner> AddOrUpdateAsync(
            Learner learner)
        {
            var learnerContainer =
               _cosmosDatabase.Containers["learners"];

            var learnerDocument =
                await learnerContainer.Items.UpsertItemAsync<Learner>(
                    learner.Id.ToString(),
                    learner);

            return learnerDocument.Resource;
        }

        public async Task<Learner> UpdateAsync(
            Learner learner)
        {
            var learnerContainer =
               _cosmosDatabase.Containers["learners"];

            var learnerDocument =
                await learnerContainer.Items.ReplaceItemAsync<Learner>(
                    learner.Id.ToString(),
                    learner.Id.ToString(),
                    learner);

            return learnerDocument.Resource;
        }
    }
}
