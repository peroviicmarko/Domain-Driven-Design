using DDD.IoC.Core;

namespace DDD.API.Middleware
{
    public class RequestLogger
    {
        public static void HttpLogger(HttpContext context)
        {
            string requestUrl = context.Request.Path;
            string method = context.Request.Method;

            string remoteIpAddress = context.Connection.RemoteIpAddress.ToString();
            string remotePort = context.Connection.RemotePort.ToString();

            string log = $"{method} {requestUrl} ip:[{remoteIpAddress}] port:[{remotePort}]";
            Logger.Http(log);
        }
    }
}
