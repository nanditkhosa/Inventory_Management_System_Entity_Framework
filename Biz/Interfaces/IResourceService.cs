using System.Collections.Generic;
using System.Linq;
using Core.Domains;

namespace Biz.Interfaces
{
    public interface IResourceService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IQueryable<Resource> GetAll();
        IQueryable<Resource> GetAllInactive();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Resource GetById(int id);

        //User GetByEmailId(string id);

        /// <summary>
        /// Inserts or updates the model.
        /// </summary>
        /// <param name="account">The account.</param>
        void InsertOrUpdate(Resource resource);

        /// <summary>
        /// Delete Asset
        /// </summary>
        /// <param name="asset"></param>
        void Delete(Resource resource);

        void UpdateInventory(Resource resource);
    }
}
