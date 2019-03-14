using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Net.Http.Headers;

namespace MvcTestApp.FunctionalTests
{
    //https://reasoncodeexample.com/2016/08/29/how-to-keep-things-tidy-when-using-asp-net-core-testserver/
    public class Server
    {
        private readonly TestServer _testServer;

        public Server(TestServer testServer)
        {
            _testServer = testServer;
            Cookies = new CookieContainer();
        }

        public CookieContainer Cookies { get; }

        public async Task<HttpResponseMessage> Get(string relativeUrl)
        {
            return await Get(new Uri(relativeUrl, UriKind.Relative));
        }

        public async Task<HttpResponseMessage> Post(string relativeUrl, IDictionary<string, string> formValues)
        {
            var relativeUrlUri = new Uri(relativeUrl, UriKind.Relative);
            var absoluteUrl = new Uri(_testServer.BaseAddress, relativeUrlUri);

            var requestBuilder = _testServer.CreateRequest(absoluteUrl.ToString());
            AddCookies(requestBuilder, absoluteUrl);

            var response = await requestBuilder.And(message =>
            {
                message.Content = new FormUrlEncodedContent(formValues);
            }).PostAsync();

            UpdateCookies(response, absoluteUrl);

            return response;
        }

        public async Task<HttpResponseMessage> FollowRedirect(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.Moved && response.StatusCode != HttpStatusCode.Found)
            {
                return response;
            }
            var redirectUrl = new Uri(response.Headers.Location.ToString(), UriKind.RelativeOrAbsolute);
            if (redirectUrl.IsAbsoluteUri)
            {
                redirectUrl = new Uri(redirectUrl.PathAndQuery, UriKind.Relative);
            }
            return await Get(redirectUrl);
        }

        private async Task<HttpResponseMessage> Get(Uri relativeUrl)
        {
            var absoluteUrl = new Uri(_testServer.BaseAddress, relativeUrl);
            var requestBuilder = _testServer.CreateRequest(absoluteUrl.ToString());
            AddCookies(requestBuilder, absoluteUrl);
            var response = await requestBuilder.GetAsync();
            UpdateCookies(response, absoluteUrl);
            return response;
        }

        private void AddCookies(RequestBuilder requestBuilder, Uri absoluteUrl)
        {
            var cookieHeader = Cookies.GetCookieHeader(absoluteUrl);
            if (!string.IsNullOrWhiteSpace(cookieHeader))
            {
                requestBuilder.AddHeader(HeaderNames.Cookie, cookieHeader);
            }
        }

        private void UpdateCookies(HttpResponseMessage response, Uri absoluteUrl)
        {
            if (response.Headers.Contains(HeaderNames.SetCookie))
            {
                var cookies = response.Headers.GetValues(HeaderNames.SetCookie);
                foreach (var cookie in cookies)
                {
                    Cookies.SetCookies(absoluteUrl, cookie);
                }
            }
        }
    }
}