using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web2.Startup))]
namespace Web2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
