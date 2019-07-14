using ComplanMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ComplanMVC.DataAccessLayer
{
    public class DBdata
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["rahulllll"].ConnectionString);

        public DataTable GetCityByState(int stateid)
        {
            string result = "";
            DataTable ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SP_CITYBIND_BY_STATE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@STATEID", stateid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataTable();
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return ds;
            }
            finally
            {
                con.Close();
            }
        }

        public string InsertClientMigration(ClientRegisClass cm, string strtypeid)
        {
            string str = "";
            string result = "";
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ClientRegistration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClientName", cm.ClientName);
                cmd.Parameters.AddWithValue("@ClientCode", cm.ClientCode);
                cmd.Parameters.AddWithValue("@Clienttype", strtypeid);
                cmd.Parameters.AddWithValue("@mode", 1);
                //cmd.Parameters.AddWithValue("@status", ParameterDirection.Output);

                SqlParameter ReturnStat = cmd.Parameters.Add("@status", SqlDbType.Int);
                ReturnStat.Direction = ParameterDirection.Output;

                con.Open();
                result = Convert.ToString(cmd.ExecuteScalar());
                str = ReturnStat.Value.ToString();
                return str;
            }
            catch
            {
                return str = "";
            }
            finally
            {
                con.Close();
            }
        }
        //SelectAllData
        public DataTable SelectAlClientlData()
        {
            string result = "";
            DataTable ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ClientRegistration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClientName", "");
                cmd.Parameters.AddWithValue("@ClientCode", "");
                cmd.Parameters.AddWithValue("@Clienttype", 0);
                cmd.Parameters.AddWithValue("@mode", 2);
                SqlParameter ReturnStat = cmd.Parameters.Add("@status", SqlDbType.Int);
                ReturnStat.Direction = ParameterDirection.Output;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataTable(); 
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return ds;
            }
            finally
            {
                con.Close();
            }
        }

        //***************************************************Customer Add**********************************************
        public string InsertData(Customer objcust)
        {
            string result = "";
            try
            {
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", 0);
                cmd.Parameters.AddWithValue("@Name", objcust.Name);
                cmd.Parameters.AddWithValue("@Address", objcust.Address);
                cmd.Parameters.AddWithValue("@Mobileno", objcust.Mobileno);
                cmd.Parameters.AddWithValue("@Birthdate", objcust.Birthdate);
                cmd.Parameters.AddWithValue("@EmailID", objcust.EmailID);
                cmd.Parameters.AddWithValue("@Query", 1);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public string UpdateData(Customer objcust)
        {
            string result = "";
            try
            {
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", objcust.CustomerID);
                cmd.Parameters.AddWithValue("@Name", objcust.Name);
                cmd.Parameters.AddWithValue("@Address", objcust.Address);
                cmd.Parameters.AddWithValue("@Mobileno", objcust.Mobileno);
                cmd.Parameters.AddWithValue("@Birthdate", objcust.Birthdate);
                cmd.Parameters.AddWithValue("@EmailID", objcust.EmailID);
                cmd.Parameters.AddWithValue("@Query", 2);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public string DeleteData(Customer objcust)
        {
            string result = "";
            try
            {
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", objcust.CustomerID);
                cmd.Parameters.AddWithValue("@Name", null);
                cmd.Parameters.AddWithValue("@Address", null);
                cmd.Parameters.AddWithValue("@Mobileno", null);
                cmd.Parameters.AddWithValue("@Birthdate", null);
                cmd.Parameters.AddWithValue("@EmailID", null);
                cmd.Parameters.AddWithValue("@Query", 3);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public List<Customer> Selectalldata()
        {
            DataSet ds = null;
            List<Customer> custlist = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", null);
                cmd.Parameters.AddWithValue("@Name", null);
                cmd.Parameters.AddWithValue("@Address", null);
                cmd.Parameters.AddWithValue("@Mobileno", null);
                cmd.Parameters.AddWithValue("@Birthdate", null);
                cmd.Parameters.AddWithValue("@EmailID", null);
                cmd.Parameters.AddWithValue("@Query", 4);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                custlist = new List<Customer>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Customer cobj = new Customer();
                    cobj.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"].ToString());
                    cobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    cobj.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    cobj.Mobileno = ds.Tables[0].Rows[i]["Mobileno"].ToString();
                    cobj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                    cobj.Birthdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Birthdate"].ToString());

                    custlist.Add(cobj);
                }
                return custlist;
            }
            catch
            {
                return custlist;
            }
            finally
            {
                con.Close();
            }
        }

        public Customer SelectDatabyID(string CustomerID)
        {
            DataSet ds = null;
            Customer cobj = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@Name", null);
                cmd.Parameters.AddWithValue("@Address", null);
                cmd.Parameters.AddWithValue("@Mobileno", null);
                cmd.Parameters.AddWithValue("@Birthdate", null);
                cmd.Parameters.AddWithValue("@EmailID", null);
                cmd.Parameters.AddWithValue("@Query", 5);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cobj = new Customer();
                    cobj.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"].ToString());
                    cobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    cobj.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    cobj.Mobileno = ds.Tables[0].Rows[i]["Mobileno"].ToString();
                    cobj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                    cobj.Birthdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Birthdate"].ToString());
                }
                return cobj;
            }
            catch
            {
                return cobj;
            }
            finally
            {
                con.Close();
            }
        }
        //======================================================User Master=========================================

        public string Logincheck(Login obj)
        {
            string result = "";
            try
            {
                SqlCommand cmd = new SqlCommand("SP_USERMASTER", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", obj.UserId);
                cmd.Parameters.AddWithValue("@PASSWORD", obj.Password);
                cmd.Parameters.AddWithValue("@STATUS", 1);
                con.Open();
                result = Convert.ToString(cmd.ExecuteScalar());
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }
    }
}