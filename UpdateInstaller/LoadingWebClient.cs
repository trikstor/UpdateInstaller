using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UpdateInstaller.Models;

namespace UpdateInstaller
{
    public class LoadingWebClient
    {
        private Uri releaseHistoryUrl { get; }

        public LoadingWebClient(Uri releaseHistoryUrl)
        {
            this.releaseHistoryUrl = releaseHistoryUrl;
        }

        public async Task<IEnumerable<ReleaseRecord>> GetReleaseHistoryAsync()
        {
            var webClient = new WebClient();
            var content = await webClient.DownloadStringTaskAsync(releaseHistoryUrl).ConfigureAwait(false);
            var releaseHistory = JsonConvert.DeserializeObject<ReleaseRecord[]>(content);

            return releaseHistory;
        }
    }
}
