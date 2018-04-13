using Core.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Properties
        private IDbSet<User> Entities => _entities ?? (_entities = _context.Set<User>());
        private readonly AppContext _context;
        private IDbSet<User> _entities;
        #endregion

        #region constructors
        public UserRepository()
        {
            _context = new AppContext();
        }
        #endregion

        #region Methods

        public IQueryable<User> UserTable => Entities.Include(u => u.Facilities).Where(u => u.IsActive == true);
        public IQueryable<User> InactiveUserTable => Entities.Include(u => u.Facilities).Where(u => u.IsActive == false);

        public IQueryable<User> UserTableUntracked => Entities.AsNoTracking();

        public void DeleteUser(User entity)
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

        public User GetUserByUserId(int id)
        {
            return UserTable.Where(u => u.Id== id).FirstOrDefault();
        }

        public User GetUserByUserName(string username)
        {
            return UserTable.Where(u => u.UserName == username).FirstOrDefault();
        }

        public IDictionary<string, object> GetModifiedProperties(User entity)
        {
            return _context.GetModifiedProperties(entity);
        }

        public void InsertUser(User entity)
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



        public void UpdateUser(User entity)
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

        public void InsertNewUserForExisitingFacility(User user, List<int> ListOfFacilityIds)
        {
            foreach(int id in ListOfFacilityIds)
            {
                var facility = _context.Facility.Find(id);
                facility.Users.Add(user);
            }
            
            _context.SaveChanges();
        }

        public void UpdateExisitingUserWithFacility(User user, List<int> ListOfFacilityIds)
        {
            var NewFacilityList = _context.Facility.Where(f => ListOfFacilityIds.Contains(f.Id));
            user.Facilities = NewFacilityList.ToList();

            _context.SaveChanges();
        }


        #endregion
    }
}
