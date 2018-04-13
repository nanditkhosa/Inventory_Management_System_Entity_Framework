using System.Collections.Generic;
using System.Linq;
using Core.Domains;

namespace Web.ViewModels
{
    public class StandardIndexViewModel
    {
        public IEnumerable<FacilityViewModel> Facilities { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }

        public IEnumerable<ResourceViewModel> Resources { get; set; }

        public StandardIndexViewModel(IEnumerable<Facility> facility)
        {
            Facilities = facility.Select(x => new FacilityViewModel(x));
        }

        public StandardIndexViewModel(IEnumerable<User> userList)
        {
            Users = userList.Select(user => new UserViewModel(user));
        }

        public StandardIndexViewModel(IEnumerable<Resource> resources)
        {
            Resources = resources.Select(x => new ResourceViewModel(x));
        }
    }

}