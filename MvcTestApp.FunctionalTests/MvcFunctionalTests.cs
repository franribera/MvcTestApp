using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvcTestApp.FunctionalTests
{
    [TestClass]
    public class MvcFunctionalTests
    {
        private Server _server;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>();
            var testServer = new TestServer(builder);
            _server = new Server(testServer);
        }

        [TestMethod]
        public async Task User_Without_Session_Is_Redirected_To_Login_Page()
        {
            // Arrange
            const string destinationPage = "/Page1";
            const string expectedRedirectedPage = "/login/login";

            // Act
            var response = await _server.Get(destinationPage);

            // Assert
            Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
            Assert.AreEqual(destinationPage, response.RequestMessage.RequestUri.AbsolutePath);
            Assert.AreEqual(expectedRedirectedPage, response.Headers.Location.AbsolutePath);
        }

        [TestMethod]
        public async Task User_Login_Failed_No_Redirection_Is_Performed()
        {
            // Arrange
            var postValues = new Dictionary<string, string>
            {
                {"username", "NotExistingUser"},
                {"password", "NotExistingPassword"},
                {"returnurl", "/Page1"}
            };

            // Act
            var response = await _server.Post("/login/login", postValues);

            // Assert
            Assert.AreNotEqual(HttpStatusCode.Redirect, response.StatusCode);
        }

        [TestMethod]
        public async Task User_Logs_In_And_Is_Redirected_To_Original_Requested_Page()
        {
            // Arrange
            const string destinationPage = "/Page1";
            var postValues = new Dictionary<string, string>
            {
                {"username", "User1"},
                {"password", "User1"},
                {"returnurl", destinationPage}
            };

            // Act
            var loginResponse = await _server.Post("/login/login", postValues);
            var response = await _server.FollowRedirect(loginResponse);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(destinationPage, response.RequestMessage.RequestUri.AbsolutePath);
        }

        [DataTestMethod]
        [DataRow("User1", true)]
        [DataRow("User2", false)]
        [DataRow("User3", false)]
        [DataRow("Admin", true)]
        public async Task Page1_Can_Only_Be_Accessed_By_PAGE1_Role(string userName, bool expectedRedirectionPage)
        {
            await TestPageAccess("/Page1", userName, expectedRedirectionPage);
        }

        [DataTestMethod]
        [DataRow("User1", false)]
        [DataRow("User2", true)]
        [DataRow("User3", false)]
        [DataRow("Admin", true)]
        public async Task Page2_Can_Only_Be_Accessed_By_PAGE2_Role(string userName, bool expectedRedirectionPage)
        {
            await TestPageAccess("/Page2", userName, expectedRedirectionPage);
        }

        [DataTestMethod]
        [DataRow("User1", false)]
        [DataRow("User2", false)]
        [DataRow("User3", true)]
        [DataRow("Admin", true)]
        public async Task Page3_Can_Only_Be_Accessed_By_PAGE3_Role(string userName, bool expectedRedirectionPage)
        {
            await TestPageAccess("/Page3", userName, expectedRedirectionPage);
        }

        private async Task TestPageAccess(string intendedDestinationPage, string userName, bool accessGranted)
        {
            // Arrange
            const string unauthorieze = "/login/Unauthorize";

            var postValues = new Dictionary<string, string>
            {
                {"username", userName},
                {"password", userName},
                {"returnurl", intendedDestinationPage}
            };

            // Act
            var loginResponse = await _server.Post("/login/login", postValues);
            var redirectResponse = await _server.FollowRedirect(loginResponse);
            var response = await _server.FollowRedirect(redirectResponse);

            // Assert
            Assert.AreEqual(accessGranted, response.RequestMessage.RequestUri.AbsolutePath != unauthorieze);
        }


    }
}
