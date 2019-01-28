using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Praxeum.WebApp.Helpers;
using Praxeum.WebApp.Models;

namespace Praxeum.WebApp.Pages.Learners.Errors
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AzureAdB2COptions _azureAdB2COptions;

        public IList<LearnerIndexModel> Learners { get; private set; }

        public IndexModel(
            IOptions<AzureAdB2COptions> azureAdB2COptions)
        {
            _azureAdB2COptions = azureAdB2COptions.Value;
        }

        public async Task OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response =
                    await httpClient.GetAsync($"{_azureAdB2COptions.ApiUrl}/learners?status=failed");

                response.EnsureSuccessStatusCode();

                var content =
                    await response.Content.ReadAsStringAsync();

                this.Learners =
                     JsonConvert.DeserializeObject<List<LearnerIndexModel>>(content);
            }
        }
    }
}