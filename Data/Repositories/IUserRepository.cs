using Core.Domains;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void DeleteUser(User entity);

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        User GetUserByUserId(int id);

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        User GetUserByUserName(string username);

        /// <summary>
        ///  Gets the modified properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        IDictionary<string, object> GetModifiedProperties(User entity);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void InsertUser(User entity);

        /// <summary>
        ///  Gets the table.
        /// </summary>
        IQueryable<User> UserTable { get; }
        IQueryable<User> InactiveUserTable { get; }

        /// <summary>
        /// Gets the table untracked.
        /// </summary>
        IQueryable<User> UserTableUntracked { get; }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void UpdateUser(User entity);

        void InsertNewUserForExisitingFacility(User user, List<int> ListOfFacilityIds);

        void UpdateExisitingUserWithFacility(User user, List<int> ListOfFacilityIds);
    }
}
