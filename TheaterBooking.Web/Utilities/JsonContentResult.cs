using System.Collections.Generic;
using System.Data.HashFunction;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JetBrains.Annotations;

namespace TheaterBooking.Web.Utilities
{
    /// <summary>
    ///     Represents a JSON serialized object that is the result of an action method
    /// </summary>
    internal class JsonContentResult : ContentResult
    {
        /// <summary>
        ///     Instantiates a new JSON content result with the specified object as its value
        /// </summary>
        /// <param name="value">The object to serialize into JSON format</param>
        internal JsonContentResult(object value)
        {
            Content = JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                // Must match the React serializer configuration
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            });
            ContentEncoding = Encoding.UTF8;
            ContentType = "application/json";
        }

        /// <summary>
        ///     Enables processing of the result of an action method by a custom type that inherits from the
        ///     <see cref="T:System.Web.Mvc.ActionResult" /> class.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> parameter is null.</exception>
        public override void ExecuteResult([NotNull] ControllerContext context)
        {
            // used a non cryptographic hash, for speed!
            var httpContext = context.HttpContext;
            var cachedEtags = new HashSet<string>(httpContext.Request.Headers.GetValues("If-None-Match") ?? new string[0]);
            var etag = $"\"{Base64.Encode(new xxHash(64).ComputeHash(Encoding.Unicode.GetBytes(Content)))}\"";

            SuppressHttpCachePolicy(httpContext.Response);
            httpContext.Response.Headers["Cache-Control"] = "private, no-cache, must-revalidate";
            httpContext.Response.Headers["ETag"] = etag;

            if (cachedEtags.Contains(etag))
            {
                httpContext.Response.StatusCode = (int) HttpStatusCode.NotModified;
                httpContext.Response.ContentType = ContentType;
                httpContext.Response.ContentEncoding = ContentEncoding;
            }
            else
            {
                base.ExecuteResult(context);
            }
        }

        /// <summary>
        ///     Suppresses the HTTP Cache Policy on the specified response
        /// </summary>
        internal static void SuppressHttpCachePolicy([NotNull] HttpResponseBase response)
        {
            // I blame Microsoft for this hack. (HttpCachePolicy overwrites my headers, and its API only allows you to get stricter)
            var cache = response.Cache;
            cache.SetCacheability(HttpCacheability.NoCache);

            var policyField = cache.GetType().GetField("_httpCachePolicy", BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Assert(policyField != null, "policyField != null");

            var cachedHeadersField = policyField.GetValue(cache)
                .GetType()
                .GetField("_useCachedHeaders", BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Assert(cachedHeadersField != null, "cachedHeadersField != null");
            cachedHeadersField.SetValue(policyField.GetValue(cache), true);

            response.SuppressDefaultCacheControlHeader = true;
        }
    }
}
