using Biz.Interfaces;
using Biz.Services;
using Ninject.Modules;

namespace Web.Infrastructure
{
    public class WebModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// Note: please sort modules alphabetically
        /// </summary>
        public override void Load()
        {
            Bind<IFacilityService>().To<FacilityService>();
            Bind<IUserService>().To<UserService>();
            Bind<IResourceService>().To<ResourceService>();
        }
    }
}