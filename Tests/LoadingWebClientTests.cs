using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using UpdateInstaller;
using UpdateInstaller.Models;

namespace Tests
{
    public class Tests
    {
        private LoadingWebClient loadingWebClient { get; set; }

        [SetUp]
        public void Setup()
        {
            var historyUrl = new Uri("https://raw.githubusercontent.com/trikstor/UpdateInstaller/master/Tests/Data/ReleaseHistory.json");
            loadingWebClient = new LoadingWebClient(historyUrl);
        }

        [Test]
        public async Task Test1()
        {
            var expectedHistory = new[]
            {
                new ReleaseRecord
                {
                    Version = "0.0.1",
                    Description = "Test release 1",
                    PackageUrl = new Uri("https://test.com/package1.zip")
                },
                new ReleaseRecord
                {
                    Version = "0.0.2",
                    Description = "Test release 2",
                    PackageUrl = new Uri("https://test.com/package2.zip")
                },
                new ReleaseRecord
                {
                    Version = "0.0.3",
                    Description = "Test release 3",
                    PackageUrl = new Uri("https://github.com/trikstor/UpdateInstaller/raw/master/Tests/Data/TestPackage.zip")
                }
            };

            var actualHistoryCollection = await loadingWebClient.GetReleaseHistoryAsync().ConfigureAwait(false);
            var actualHistory = actualHistoryCollection.ToArray();

            Assert.That(actualHistory, Is.Not.Null);
            Assert.That(actualHistory.Length, Is.EqualTo(expectedHistory.Length));
            foreach (var actualRecord in actualHistory)
            {
                Assert.That(expectedHistory.Select(r => r.Version).Contains(actualRecord.Version));
                Assert.That(expectedHistory.Select(r => r.Description).Contains(actualRecord.Description));
                Assert.That(expectedHistory.Select(r => r.PackageUrl).Contains(actualRecord.PackageUrl));
            }
        }
    }
}