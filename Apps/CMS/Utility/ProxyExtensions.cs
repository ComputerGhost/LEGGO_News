using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProxyKit;
using System.Net.Http;
using System.Threading.Tasks;

namespace CMS.Utility
{
    public static class ProxyExtensions
    {

        public static IServiceCollection AddMyProxy(this IServiceCollection services)
        {
            return services.AddProxy();
        }


        private static async Task<HttpResponseMessage> HandleProxyTraffic(HttpContext context, string proxyBaseUri)
        {
            var forwardContext = context.ForwardTo(proxyBaseUri);
            var token = await context.GetTokenAsync("access_token");
            forwardContext.UpstreamRequest.SetBearerToken(token);
            return await forwardContext.Send();
        }

        public static IApplicationBuilder UseMyProxy(this IApplicationBuilder app, string proxyBaseUri)
        {
            app.RunProxy(context => HandleProxyTraffic(context, proxyBaseUri));
            return app;
        }

    }
}
