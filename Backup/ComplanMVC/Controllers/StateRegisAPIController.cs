using ComplanMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ComplanMVC.Controllers
{
    public class StateRegisAPIController : ApiController
    {
        private static List<StateRegis> proslist;
        // GET api/stateregis
        public List<StateRegis> Get()
        {
            return Getallstates();
        }

        // GET api/stateregis/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/stateregis
        //public void Post([FromBody]string value)
        //{
        //}
        //--------------------------insert part---------------
        public string Post(string statename)
        {
            StateRegis ob = new StateRegis();
            ob.statename = statename;
            return InsertEmployees(ob);
        }

        // PUT api/stateregis/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}
        public string Put(int stateid, string statename)
        {
            string str = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["rahulllll"].ConnectionString);
            SqlCommand cmd = new SqlCommand("update StateMaster set StateName='" + statename + "' where StateId=" + stateid + "", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                str = "1";
            }
            catch (Exception ex)
            {
                str = "0";
            }
            return str;
        }

        // DELETE api/stateregis/5
        //public void Delete(int id)
        //{
        //}
        public string Delete(int stateid)
        {
            string str = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["rahulllll"].ConnectionString);
            SqlCommand cmd = new SqlCommand("delete from StateMaster where StateId='" + stateid + "'", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                str = "1";
            }
            catch (Exception ex)
            {
                str = "0";
            }
            return str;
        }
        //==================================================================================================================

        public static List<StateRegis> Getallstates()
        {
            try
            {
                DataTable dtbramch = new DataTable();
                dtbramch = selectjsondata();
                proslist = dtbramch.AsEnumerable().Select(data => new StateRegis()
                {
                    stateid = Convert.ToInt32(data["StateId"]),
                    statename = Convert.ToString(data["StateName"]),
                    countryid = Convert.ToInt32(data["CountryId"])
                }).ToList();
                return proslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable selectjsondata()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["rahulllll"].ConnectionString);
            DataTable dt = new DataTable();
            try
            {
                string str = @"select * from dbo.StateMaster";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
            finally
            {
                con.Close();
            }
        }

        public static string InsertEmployees(StateRegis prod)
        {
            string str = "";
            try
            {
                str = SaveData(prod.statename);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return str;
        }
        public static string SaveData(string statename)
        {
            string str = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["rahulllll"].ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO StateMaster(StateName,CountryId) VALUES ('" + statename + "',1)", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                str = "1";
            }
            catch (Exception ex)
            {
                str = "0";
            }
            return str;
        }
    }
}
