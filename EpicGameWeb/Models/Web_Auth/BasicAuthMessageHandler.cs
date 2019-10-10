using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace EpicGameWeb.Models.Web_Auth
{
    public class BasicAuthMessageHandler : DelegatingHandler
    {
        private const string BasicAuthResponseHeader = "WWW-Authenticate";
        private const string BasicAuthResponseHeaderValue = "Basic";

        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue authValue = request.Headers.Authorization;
            if (authValue != null && 
                authValue.Parameter != "undefined" &&
                !String.IsNullOrWhiteSpace(authValue.Parameter))
            {
                string nickname = authValue.Parameter;
                if (HttpContext.Current.Session == null ||
                    HttpContext.Current.Session["userToken"] == null ||
                    string.IsNullOrEmpty(HttpContext.Current.Session["userToken"].ToString()))
                {
                    HttpContext.Current.Session["userToken"] = nickname;
                }
                else
                {
                    nickname = HttpContext.Current.Session["userToken"].ToString();
                }

                if (!string.IsNullOrEmpty(nickname))
                {
                    if (!string.IsNullOrEmpty(nickname))
                    {
                        var identity = new GenericIdentity(nickname);
                        IPrincipal principalObj = new GenericPrincipal(identity, new[] { "Admin" });
                        Thread.CurrentPrincipal = principalObj;
                        HttpContext.Current.User = principalObj;
                    }

                }
            }

            try
            {
                var res = base.SendAsync(request, cancellationToken)
                   .ContinueWith(task =>
                   {
                       var response = task.Result;
                       if (response.StatusCode == HttpStatusCode.Unauthorized
                           && !response.Headers.Contains(BasicAuthResponseHeader))
                       {
                           response.Headers.Add(BasicAuthResponseHeader, BasicAuthResponseHeaderValue);
                       }
                       return response;
                   });
                return res; 
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}