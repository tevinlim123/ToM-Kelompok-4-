using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.IO;

namespace InventoryOffice.Controllers
{
    public class fUserController : Controller
    {
        SqlConnection GenConnString = new SqlConnection(System.Configuration.ConfigurationManager.
        ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand cmd;
        // GET: fForm
        // GET: User
        public ActionResult UserIndex()
        {
            if (Session["UserName"] == null || Session["UserName"].ToString() == "")
            {
                return RedirectToAction("HomePage", "fLogin");
            }
            return View();
        }
        public ActionResult UserData()
        {
            return View();
        }
        public ActionResult UserEdit()
        {
            if (Session["UserName"] == null || Session["UserName"].ToString() == "")
            {
                return RedirectToAction("HomePage", "fLogin");
            }
            return View();
        }
        [HttpGet]
        public ActionResult testingCall()
        {
            try
            {

                DataTable DtResult = new DataTable();
                //SqlConnection MyCon = new SqlConnection(GenConnString);

                string MySql = @" select UserName,Password,Email,Address,Gender,PhoneNumber,FORMAT([CreatedDate],'dd-MM-yyyy HH:mm:ss') as 'CreatedDate',FORMAT([UpdateDate],'dd-MM-yyyy HH:mm:ss') as 'UpdateDate',DateofBirth from [User] ";
                GenConnString.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd = new SqlCommand(MySql, GenConnString);
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            DtResult = dt;
                        }
                    }
                }

                var convertdata = DtResult.AsEnumerable().Select(row => new
                {
                    UserName = row["UserName"],
                    Password = row["Password"],
                    Email = row["Email"],
                    Address = row["Address"],
                    Gender = row["Gender"],
                    PhoneNumber = row["PhoneNumber"],
                    CreatedDate = row["CreatedDate"],
                    UpdateDate = row["UpdateDate"],
                    DateofBirth = row["DateofBirth"],
                });

                return Json(new { success = true, message = "OK", data = convertdata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.ToString(), data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]

        public ActionResult SaveData(string UserName, string Password, string Address, string Email, string Gender, string PhoneNumber, bool Edit, string DateofBirth)
        {
            try
            {
                string validate = validateSubmit(UserName, Edit);
                string msg = "";
                msg = validate;
                bool success = true;
                if (validate == "TRUE")
                {
                    if (Edit == false)
                    {
                        string MySql = @"insert into [User] (UserName,Password,Address,Email,Gender,CreatedDate,DateofBirth,PhoneNumber) values (@UserName,PWDENCRYPT(@Password), @Address, @Email,@Gender,CURRENT_TIMESTAMP,@DateofBirth,@PhoneNumber)";
                        GenConnString.Open();
                        cmd = new SqlCommand(MySql, GenConnString);
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                        cmd.Parameters.AddWithValue("@DateofBirth", DateofBirth);
                        cmd.ExecuteNonQuery();
                        msg = "Data Save Succesfully";
                    }
                    else
                    {
                        string MySql = @"update [User] set UserName=@UserName,Address=@Address,Email=@Email,UpdateDate=CURRENT_TIMESTAMP,PhoneNumber=@PhoneNumber,DateofBirth=@DateofBirth";
                        if (Password != "")
                        {
                            MySql += ",Password=PWDENCRYPT(@Password)";
                        }
                        MySql += " where UserName=@UserName";
                        GenConnString.Open();
                        cmd = new SqlCommand(MySql, GenConnString);
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        if (Password != "")
                        {
                            cmd.Parameters.AddWithValue("@Password", Password);
                        }
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                        cmd.Parameters.AddWithValue("@DateofBirth", DateofBirth);
                        cmd.ExecuteNonQuery();
                        msg = "Data Update Succesfully";
                    }
                }
                else
                {
                    success = false;
                }




                return Json(new { success = success, message = msg }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Getdetail(string UserName)
        {
            try
            {

                DataTable DtResult = new DataTable();
                //SqlConnection MyCon = new SqlConnection(GenConnString);

                string MySql = @" select UserName,Address,Password,Email,DateofBirth,PhoneNumber from [User] where UserName = @UserName";

                GenConnString.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd = new SqlCommand(MySql, GenConnString);
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            DtResult = dt;
                        }
                    }
                }

                var convert = DtResult.AsEnumerable().Select(row => new
                {
                    UserName = row["UserName"],
                    Address = row["Address"],
                    Password = row["Password"],
                    Email = row["Email"],
                    DateofBirth = row["DateofBirth"],
                    PhoneNumber = row["PhoneNumber"],
                });

                return Json(new { success = true, data = convert }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.ToString(), data = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public string validateSubmit(string UserName, bool edit)
        {
            try
            {
                int Count = 0;
                //ArrayList param = new ArrayList();
                //ArrayList paramval = new ArrayList();
                string mySql;
                mySql = "SELECT UserName FROM [User] WHERE UserName= @UserName ";
                //param.Add("@groupid"); paramval.Add(groupid);
                //param.Add("@id"); paramval.Add(id);

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd = new SqlCommand(mySql, GenConnString);
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        Count = dt.Rows.Count;

                    }
                }

                if (Count > 0 && !edit)
                {
                    return "UserName Already Exist!";
                }
                else
                {
                    return "TRUE";
                }

            }
            catch (Exception Ex)
            {
                return "Error Occured, Please Contact Administrator";
            }
        }
    }
  }