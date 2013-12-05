using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tavis;

namespace GitHubLib
{
    public class ActionResponseHandler : DelegatingResponseHandler
    {
        private readonly Func<Link,HttpResponseMessage,Task> _action;

        public ActionResponseHandler(Func<Link,HttpResponseMessage,Task> action)
        {
            _action = action;
        }

        public override async Task<HttpResponseMessage> HandleAsync(Link link, HttpResponseMessage responseMessage)
        {
            await _action(link,responseMessage);

            var response = await base.HandleAsync(link, responseMessage);

            return response;
        }
    }
}