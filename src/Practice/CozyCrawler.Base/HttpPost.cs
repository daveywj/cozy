﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CozyCrawler.Base
{
    public class HttpPost
    {
        public static HttpResponseMessage Post(string url, HttpContent content, Dictionary<string, string> headers = null)
        {
            Uri uri = new Uri(HttpCommon.Host);
            HttpClientHandler handler = new HttpClientHandler { UseCookies = true };
            CookieContainer CookieContainer = HttpCookie.GetUriCookieContainer(uri);
            if (CookieContainer != null)
            {
                handler.CookieContainer = CookieContainer;
            }
            else
            {
                handler.CookieContainer = new CookieContainer();
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };

            request.Headers.Add("User-Agent", HttpCommon.DefaultUA);
            if(headers != null)
            {
                foreach (var h in headers)
                {
                    request.Headers.Add(h.Key, h.Value);
                }
            }

            HttpClient client = new HttpClient(handler);
            HttpResponseMessage response = client.SendAsync(request).Result;

            var c = handler.CookieContainer.GetCookies(uri);
            foreach (Cookie e in c)
            {
                HttpCookie.InternetSetCookie(HttpCommon.Host, e.Name, e.Value + ";path=/;expires=Sun,22-Feb-2099 00:00:00 GMT");
            }
            return response;
        }
    }
}
