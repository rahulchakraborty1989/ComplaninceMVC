using ComplanMVC.EntityFrameworkModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComplanMVC.Models;

namespace ComplanMVC.repository
{
    public class CityRepository : ICity //CityRepository class, manually inherits ICity interface
    {
        private DBComplianceEntities db;

        public CityRepository(DBComplianceEntities db)
        {
            this.db = db;
        }

        #region ##############################################################IMPLEMENT INTERFACE###################################################################
        /*try right clicking on the interface, then implement interface. that should implement all the necessary methods from the interface*/
        public void InsertCity(CityMaster ob)
        {
            throw new NotImplementedException();
        }

        public List<CommonViewClass> GetCity()
        {
            // return db.CityMasters.ToList();
            var RetCitylist = (from c in db.CityMasters
                               join s in db.StateMasters on c.StateId equals s.StateId
                               orderby c.CityName
                               select new CommonViewClass
                               {
                                   StateName = s.StateName,
                                   StateId = s.StateId,
                                   CityName = c.CityName,
                                   CityId = c.CityId
                               }).ToList();
            return RetCitylist;
        }

        public List<CommonViewClass> GetCity_ByStateID(int StateId)
        {
            var RetCitylist = (from c in db.CityMasters
                               join s in db.StateMasters on c.StateId equals s.StateId
                               where c.StateId == StateId
                               orderby c.CityName
                               select new CommonViewClass
                               {
                                   StateName = s.StateName,
                                   StateId = s.StateId,
                                   CityName = c.CityName,
                                   CityId = c.CityId
                               }).ToList();
            return RetCitylist;
        }

        public void UpdateCity(CityMaster ob)
        {
            throw new NotImplementedException();
        }

        public void DeleteCity(int CityId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}