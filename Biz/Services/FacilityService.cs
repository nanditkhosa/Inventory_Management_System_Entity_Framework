using System;
using System.Collections.Generic;
using System.Linq;
using Biz.Interfaces;
using Core.Domains;
using Data.Repositories;

namespace Biz.Services
{
    public class FacilityService : IFacilityService
    {
        #region Properties
        private readonly IFacilityRepository _facilityRepo;

        #endregion

        #region Constructor

        public FacilityService()
        {
            _facilityRepo = new FacilityRepository();
        }

        public FacilityService(IFacilityRepository facilityRepo)
        {
            _facilityRepo = facilityRepo;
        }
        #endregion

        #region Methods
        public IQueryable<Facility> GetAll()
        {
            return _facilityRepo.FacilityTable;
        }

        public IQueryable<Facility> GetAllInactive()
        {
            return _facilityRepo.InactiveFacilityTable;
        }

        public void Delete(Facility facility)
        {
            _facilityRepo.DeleteFacility(facility);
        }
        public Facility GetById(int id)
        {
            return _facilityRepo.GetFacilityByFacilityId(id);
        }

        public IEnumerable<Facility> GetAllDataTable(string sortOrder, string search, bool? activeFilter = null)
        {
            var queryTable = _facilityRepo.FacilityTable;


            if (activeFilter != null)
            {
                queryTable = queryTable.Where(x => x.IsActive == activeFilter);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchValue = search.ToLower();

                queryTable = queryTable.Where(x => x.Name.ToLower().Contains(searchValue) || x.Landmark.ToLower().Contains(searchValue) || x.Address.ToLower().Contains(searchValue)
                                                   || x.Address2.ToLower().Contains(searchValue) || x.City.ToLower().Contains(searchValue) || x.State.ToLower().Contains(searchValue) ||
                                                   x.ZipCode.ToString().ToLower().Contains(searchValue));
            }
            switch (sortOrder)
            {
                case "Name":
                    return queryTable.OrderBy(x => x.Name);
                case "Name DESC":
                    return queryTable.OrderByDescending(x => x.Name);
                case "Landmark":
                    return queryTable.OrderBy(x => x.Landmark);
                case "Landmark DESC":
                    return queryTable.OrderByDescending(x => x.Landmark);
            }
            return queryTable;


        }

        public void InsertOrUpdate(Facility facility)
        {
            if (facility.Id == 0)
            {
                DateTime currentdateTime = new DateTime();
                facility.LastModifiedTimeStamp = currentdateTime;
                _facilityRepo.InsertFacility(facility);
            }
            else
            {
                DateTime currentdateTime = new DateTime();
                facility.LastModifiedTimeStamp = currentdateTime;
                _facilityRepo.UpdateFacility(facility);
            }
        }

        public IQueryable<Facility> GetFacilitiesWithIds(List<int> FacilityIds)
        {
            return _facilityRepo.GetFacilitiesWithIds(FacilityIds);
        }
        #endregion

    }
}
