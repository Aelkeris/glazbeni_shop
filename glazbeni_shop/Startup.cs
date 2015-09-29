using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(glazbeni_shop.Startup))]
namespace glazbeni_shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
