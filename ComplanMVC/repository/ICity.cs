using ComplanMVC.EntityFrameworkModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComplanMVC.Models;

namespace ComplanMVC.repository
{
    public interface ICity
    {
        void InsertCity(CityMaster ob); // Create
        List<CommonViewClass> GetCity(); // Read
        List<CommonViewClass> GetCity_ByStateID(int StateId);// Read by id
        void UpdateCity(CityMaster ob); //Update
        void DeleteCity(int CityId); //Delete
        void Save();
    }
}