using System.Web.Http;

namespace FlickrTest.App_Start
{
    /// <summary>
    /// Handles web api configuration
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Maps the web api routes
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "DefaultApi", 
                "api/{controller}/{id}", 
                new { id = RouteParameter.Optional }
            );
        }
    }
}
