using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Core.Domains;

namespace Data.Repositories
{
    public class FacilityRepository : IFacilityRepository
    {
        #region Properties
        private IDbSet<Facility> Entities => _entities ?? (_entities = _context.Set<Facility>());
        private readonly AppContext _context;
        private IDbSet<Facility> _entities;
        #endregion

        #region constructors
        public FacilityRepository()
        {
            _context = new AppContext();
        }
        #endregion

        #region Methods

        public IQueryable<Facility> FacilityTable => Entities.Include(f => f.Users).Include(f => f.Resources).Where(f => f.IsActive == true);
        public IQueryable<Facility> InactiveFacilityTable => Entities.Include(f => f.Users).Include(f => f.Resources).Where(f => f.IsActive == false);

        public IQueryable<Facility> FacilityTableUntracked => Entities.AsNoTracking();

        public void DeleteFacility(Facility entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.Aggregate(string.Empty, (current1, validationErrors) =>
                    validationErrors.ValidationErrors.Aggregate(current1, (current, validationError) =>
                    current + (Environment.NewLine + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}")));

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public Facility GetFacilityByFacilityId(int id)
        {
            return FacilityTable.Where(f => f.Id == id).FirstOrDefault();
        }

        public void InsertFacility(Facility entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Add(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.Aggregate(string.Empty, (current1, validationErrors) =>
                     validationErrors.ValidationErrors.Aggregate(current1, (current, validationError) =>
                     current + (Environment.NewLine + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}")));

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void UpdateFacility(Facility entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    var alreadyAttached = Entities.Local.FirstOrDefault(x => x.Id == entity.Id);
                    if (alreadyAttached != null)
                    {
                        _context.Entry(alreadyAttached).CurrentValues.SetValues(entity);
                    }
                    else
                    {
                        _context.Entry(entity).State = EntityState.Modified;
                    }
                }

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.Aggregate(string.Empty, (current1, validationErrors) =>
                    validationErrors.ValidationErrors.Aggregate(current1, (current, validationError) =>
                    current + (Environment.NewLine + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}")));

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public IQueryable<Facility> GetFacilitiesWithIds(List<int> FacilityIds)
        {
            return Entities.Where(f => FacilityIds.Contains(f.Id));
        }
        #endregion
    }
}