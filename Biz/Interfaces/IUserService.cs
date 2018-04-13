using Core.Domains;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Interfaces
{
    public interface IUserService
    {

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IQueryable<User> GetAll();

        IQueryable<User> GetAllInactive();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        User GetById(int id);

        User GetByUserName(string email);

        /// <summary>
        /// Inserts or updates the model.
        /// </summary>
        /// <param name="account">The account.</param>
        void Insert(User user, List<int> ListOfFacilityIds);


        void Update(User user, List<int> ListOfFacilityIds);
    }
}
