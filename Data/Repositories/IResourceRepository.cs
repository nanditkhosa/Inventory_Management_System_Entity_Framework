using Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IResourceRepository
    {
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void DeleteResource(Resource entity);

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        Resource GetResourceByResourceId(int id);

        /// <summary>
        ///  Gets the modified properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        IDictionary<string, object> GetModifiedProperties(Resource entity);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void InsertResource(Resource entity);

        /// <summary>
        ///  Gets the table.
        /// </summary>
        IQueryable<Resource> ResourceTable { get; }
        IQueryable<Resource> InactiveResourceTable { get; }

        /// <summary>
        /// Gets the table untracked.
        /// </summary>
        IQueryable<Resource> ResourceTableUntracked { get; }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void UpdateResource(Resource entity);

        void InsertNewResourceForExisitingFacility(Resource resource);

        void UpdateExisitingResourceWithFacility(Resource resource);

    }
}
