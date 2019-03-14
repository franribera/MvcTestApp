using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.FunctionalTests
{
    [TestClass]
    public class ApiFunctionalTests
    {
        private HttpClient _client;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>();
            var server = new TestServer(builder);
            _client = server.CreateClient();
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
    }
}
