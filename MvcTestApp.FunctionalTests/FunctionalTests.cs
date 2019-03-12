using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvcTestApp.FunctionalTests
{
    [TestClass]
    public class FunctionalTests
    {
        private HttpClient _testClient;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>();
            var testServer = new TestServer(builder);
            _testClient = testServer.CreateClient();
        }

        [TestMethod]
        public async Task User_Without_Session_Is_Redirected_To_Login_Page()
        {
            // Arrange
            const string destinationPage = "/Page1";
            const HttpStatusCode expectedHttpCode = HttpStatusCode.Redirect;
            const string expectedRedirectedPage = "/login/login";

            // Act
            var response = await _testClient.GetAsync(destinationPage);

            // Assert
            Assert.AreEqual(expectedHttpCode, response.StatusCode);
            Assert.AreEqual(destinationPage, response.RequestMessage.RequestUri.AbsolutePath);
            Assert.AreEqual(expectedRedirectedPage, response.Headers.Location.AbsolutePath);
        }

        [TestMethod]
        public async Task User_Login_Failed_No_Redirection_Is_Performed()
        {
            // Arrange
            const string userName = "NotExistingUser";
            const string password = "NotExistingPassword";
            const HttpStatusCode expectedHttpCode = HttpStatusCode.OK;
            const string destinationPage = "/Page1";

            var content = new StringContent($"username={userName}&password={password}&returnurl={destinationPage}",
                Encoding.UTF8,
                "application/x-www-form-urlencoded");

            // Act
            var response = await _testClient.PostAsync("/login/login", content);

            // Assert
            Assert.AreEqual(expectedHttpCode, response.StatusCode);
            Assert.IsNull(response.Headers.Location);
        }

        [TestMethod]
        public async Task User_Logs_In_And_Is_Redirected_To_Original_Requested_Page()
        {
            // Arrange
            const string userName = "User1";
            const string password = "User1";
            const HttpStatusCode expectedHttpCode = HttpStatusCode.Redirect;
            const string originalRequestedPage = "Page1";
            var content = new StringContent($"username={userName}&password={password}&returnurl={originalRequestedPage}",
                Encoding.UTF8,
                "application/x-www-form-urlencoded");

            // Act
            var response = await _testClient.PostAsync("/login/login", content);

            // Assert
            Assert.AreEqual(expectedHttpCode, response.StatusCode);
            Assert.AreEqual(originalRequestedPage, response.Headers.Location.ToString());
        }
    }
}
