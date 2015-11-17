using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FlickrTest.Startup))]
namespace FlickrTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
            //app.MapSignalR();
        }
    }
}
