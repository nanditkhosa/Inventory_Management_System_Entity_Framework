using Core.Domains;
using Data;
using Data.Repositories;
using Ninject.Modules;

namespace Biz.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// Please add modules in alphabetical order
        /// </summary>
        public override void Load()
        {
            Bind<IFacilityRepository>().To<FacilityRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IResourceRepository>().To<ResourceRepository>();
        }
    }
}
