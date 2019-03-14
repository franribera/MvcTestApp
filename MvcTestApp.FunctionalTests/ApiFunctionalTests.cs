using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.FunctionalTests
{
    [TestClass]
    public class ApiFunctionalTests
    {
        private TestServer _testServer;
        private HttpClient _client;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>();
            _testServer = new TestServer(builder);
            _client = _testServer.CreateClient();
        }

        [TestMethod]
        public async Task GetById_Without_Basic_Authentication_Header_Returns_401_Unathorized()
        {
            // Act
            var response = await _client.GetAsync($"api/users/{User.User1.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public async Task GetAll_Without_Basic_Authentication_Header_Returns_401_Unathorized()
        {
            // Act
            var response = await _client.GetAsync("api/users");

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public async Task Post_Without_Basic_Authentication_Header_Returns_401_Unathorized()
        {
            // Act
            var response = await _client.PostAsync("api/users", new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()));

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_Without_Basic_Authentication_Header_Returns_401_Unathorized()
        {
            // Act
            var response = await _client.PutAsync($"api/users/{ User.User1.Id}", new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()));

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public async Task Delete_Without_Basic_Authentication_Header_Returns_401_Unathorized()
        {
            // Act
            var response = await _client.DeleteAsync($"api/users/{User.User1.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [DataTestMethod]
        [DataRow("User1:User1")]
        [DataRow("User2:User2")]
        [DataRow("User3:User3")]
        [DataRow("Admin:Admin")]
        public async Task GetId_Could_Be_Accessed_By_Any_Role(string basicAuthentication)
        {
            await TestApiEndpoint(
                $"api/users/{User.User1.Id}",
                basicAuthentication,
                HttpStatusCode.OK,
                builder => builder.GetAsync());
        }

        [DataTestMethod]
        [DataRow("User1:User1")]
        [DataRow("User2:User2")]
        [DataRow("User3:User3")]
        [DataRow("Admin:Admin")]
        public async Task GetAll_Could_Be_Accessed_By_Any_Role(string basicAuthentication)
        {
            await TestApiEndpoint(
                "api/users",
                basicAuthentication,
                HttpStatusCode.OK,
                builder => builder.GetAsync());
        }

        [DataTestMethod]
        [DataRow("User1:User1", 403)]
        [DataRow("User2:User2", 403)]
        [DataRow("User3:User3", 403)]
        [DataRow("Admin:Admin", 204)]
        public async Task Delete_Can_Only_Be_Accessed_By_Admin_Role(string basicAuthentication, int expectedCode)
        {
            await TestApiEndpoint(
                $"api/users/{User.User1.Id}",
                basicAuthentication,
                HttpStatusCode.OK,
                builder => builder.SendAsync("DELETE"));
        }

        private async Task TestApiEndpoint(string relativeUrl, string basicAuthentication, HttpStatusCode expectedResult, Func<RequestBuilder, Task<HttpResponseMessage>> httpFunc)
        {
            // Arrange
            var basicAuthenticationBytes = Encoding.UTF8.GetBytes(basicAuthentication);
            var basicAuthentication64 = Convert.ToBase64String(basicAuthenticationBytes);

            var absoluteUrl = new Uri(_testServer.BaseAddress, relativeUrl);
            var requestBuilder = _testServer.CreateRequest(absoluteUrl.ToString());

            requestBuilder.AddHeader(HeaderNames.Authorization, $"Basic {basicAuthentication64}");

            // Act
            var response = await httpFunc(requestBuilder);

            // Assert
            Assert.AreEqual(expectedResult, response.StatusCode);
        }
    }
}
