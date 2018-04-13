using Core.Domains;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public interface IFacilityRepository
    {
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void DeleteFacility(Facility entity);

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        Facility GetFacilityByFacilityId(int id);

        /// <summary>
        ///  Gets the modified properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        //IDictionary<string, object> GetModifiedProperties(Facility entity);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void InsertFacility(Facility entity);

        /// <summary>
        ///  Gets the table.
        /// </summary>
        IQueryable<Facility> FacilityTable { get; }
        IQueryable<Facility> InactiveFacilityTable { get; }

        /// <summary>
        /// Gets the table untracked.
        /// </summary>
        IQueryable<Facility> FacilityTableUntracked { get; }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void UpdateFacility(Facility entity);

        IQueryable<Facility> GetFacilitiesWithIds(List<int> FacilityIds);
    }
}
