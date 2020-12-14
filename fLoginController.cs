using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ToM.Controllers
{
    public class fLoginController : Controller
    {
        SqlConnection GenConnString = new SqlConnection(System.Configuration.ConfigurationManager.
        ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand cmd;
        // GET: fLogin
        public ActionResult Login()
        {
            Session["UserName"] = null;
            Session["Address"] = null;
            Session["Email"] = null;
            return View();
        }
        public ActionResult HomePage()
        {
            Session["UserName"] = null;
            Session["Address"] = null;
            Session["Email"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Verify(string UserName, string Password)
        {
            try
            {
                DataTable DtResult = new DataTable();
                bool success = false;
                string Mysql = @" select UserName,Address,Email from [User] where UserName = @UserName and PWDCOMPARE(@Password,Password)= 1";
                GenConnString.Open();
                cmd = new SqlCommand(Mysql, GenConnString);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {

                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            success = true;
                            DtResult = dt;
                        }
                    }
                }

                var convertdata = DtResult.AsEnumerable().Select(row => new
                {
                    UserName = row["UserName"],
                    Address = row["Address"],
                    Email = row["Email"],
                });


                return Json(new { success = success, message = "ok", data = convertdata }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString(), data = "" }, JsonRequestBehavior.AllowGet);
            }


        }
        [HttpPost]
        public string setSession(string UserName, string Address, string Email, string GroupID)
        {
            try
            {
                Session["UserName"] = UserName;
                Session["Address"] = Address;
                Session["Email"] = Email;

                return "Login Success";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}