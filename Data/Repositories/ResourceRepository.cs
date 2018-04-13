using Core.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        #region Properties
        private IDbSet<Resource> Entities => _entities ?? (_entities = _context.Set<Resource>());
        private readonly AppContext _context;
        private IDbSet<Resource> _entities;
        #endregion

        #region constructors
        public ResourceRepository()
        {
            _context = new AppContext();
        }
        #endregion

        #region Methods
        
        public IQueryable<Resource> ResourceTable => Entities.Include(r => r.Facility).Where(r => r.IsActive == true);
        public IQueryable<Resource> InactiveResourceTable => Entities.Include(r => r.Facility).Where(r => r.IsActive == false);

        public IQueryable<Resource> ResourceTableUntracked => Entities.AsNoTracking();

        public void DeleteResource(Resource entity)
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

        public Resource GetResourceByResourceId(int id)
        {
            return ResourceTable.Where(u => u.Id == id).FirstOrDefault();
          }

        public IDictionary<string, object> GetModifiedProperties(Resource entity)
        {
            return _context.GetModifiedProperties(entity);
        }

        public void InsertResource(Resource entity)
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


        public void UpdateResource(Resource entity)
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

        public void InsertNewResourceForExisitingFacility(Resource resource)
        {
                var facility = _context.Facility.Find(resource.FacilityId);
                facility.Resources.Add(resource);
                _context.SaveChanges();
        }

        public void UpdateExisitingResourceWithFacility(Resource resource)
        {
           
            if (resource.FacilityId != resource.FacilityId)
            {
                //var NewFacilityList = _context.Facility.Where(f => ListOfFacilityIds.Contains(f.Id));
                //user.Facilities = NewFacilityList.ToList();

                _context.SaveChanges();
            }
            else
            {
                UpdateResource(resource);
            }
            _context.SaveChanges();
        }

        #endregion
    }
}
