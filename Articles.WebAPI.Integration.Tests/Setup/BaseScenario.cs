using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Articles.WebAPI.Integration.Tests
{
    public class BaseScenario
    {
        public TestServer CreateServer()
        {
            var hostBuilder = new WebHostBuilder()
                .UseStartup<TestStartUp>();

            var testServer = new TestServer(hostBuilder);

            return testServer;
        }
    }
}
