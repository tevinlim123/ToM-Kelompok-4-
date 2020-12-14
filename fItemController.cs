using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.IO;

namespace ToM.Controllers
{
    public class fItemController : Controller
    {
        SqlConnection GenConnString = new SqlConnection(System.Configuration.ConfigurationManager.
       ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand cmd;
        // GET: fItem
        public ActionResult DataItem()
        {
            if (Session["UserName"] == null || Session["UserName"].ToString() == "")
            {
                return RedirectToAction("HomePage", "fLogin");
            }
            return View();
        }
        public ActionResult Indexitem()
        {
            if (Session["UserName"] == null || Session["UserName"].ToString() == "")
            {
                return RedirectToAction("HomePage", "fLogin");
            }
            return View();
        }
        public ActionResult Indexitem2()
        {
            if (Session["UserName"] == null || Session["UserName"].ToString() == "")
            {
                return RedirectToAction("HomePage", "fLogin");
            }
            return View();
        }
        public ActionResult EditItem()
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

                string MySql = @" select Id,UserName,TipeProperti,Lokasi,Address,TipePenawaran,FORMAT([CreatedDate],'dd-MM-yyyy HH:mm:ss') as 'CreatedDate',FORMAT([UpdateDate],'dd-MM-yyyy HH:mm:ss') as 'UpdateDate',Harga from [Item] where TipePenawaran = 'For Sale' ";
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
                    Id = row["Id"],
                    UserName = row["UserName"],
                    TipeProperti = row["TipeProperti"],
                    Lokasi = row["Lokasi"],
                    Alamat = row["Address"],
                    TipePenawaran = row["TipePenawaran"],
                    CreatedDate = row["CreatedDate"],
                    UpdateDate = row["UpdateDate"],
                    Harga = row["Harga"],
                });

                return Json(new { success = true, message = "OK", data = convertdata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.ToString(), data = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult IndexCall()
        {
            try
            {

                DataTable DtResult = new DataTable();
                //SqlConnection MyCon = new SqlConnection(GenConnString);

                string MySql = @" select Id,UserName,TipeProperti,Lokasi,Address,TipePenawaran,FORMAT([CreatedDate],'dd-MM-yyyy HH:mm:ss') as 'CreatedDate',FORMAT([UpdateDate],'dd-MM-yyyy HH:mm:ss') as 'UpdateDate',Harga from [Item] where TipePenawaran = 'For Rent' ";
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
                    Id = row["Id"],
                    UserName = row["UserName"],
                    TipeProperti = row["TipeProperti"],
                    Lokasi = row["Lokasi"],
                    Alamat = row["Address"],
                    TipePenawaran = row["TipePenawaran"],
                    CreatedDate = row["CreatedDate"],
                    UpdateDate = row["UpdateDate"],
                    Harga = row["Harga"],
                });

                return Json(new { success = true, message = "OK", data = convertdata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.ToString(), data = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult EditCall(string UserName)
        {
            try
            {

                DataTable DtResult = new DataTable();
                //SqlConnection MyCon = new SqlConnection(GenConnString);

                string MySql = @" select Id,Image,UserName,TipeProperti,Lokasi,Address,TipePenawaran,FORMAT([CreatedDate],'dd-MM-yyyy HH:mm:ss') as 'CreatedDate',FORMAT([UpdateDate],'dd-MM-yyyy HH:mm:ss') as 'UpdateDate',Harga from [Item] where UserName = @UserName ";
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

                var convertdata = DtResult.AsEnumerable().Select(row => new
                {
                    Id = row["Id"],
                    Image = row["Image"],
                    UserName = row["UserName"],
                    TipeProperti = row["TipeProperti"],
                    Lokasi = row["Lokasi"],
                    Alamat = row["Address"],
                    TipePenawaran = row["TipePenawaran"],
                    CreatedDate = row["CreatedDate"],
                    UpdateDate = row["UpdateDate"],
                    Harga = row["Harga"],
                });

                return Json(new { success = true, message = "OK", data = convertdata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.ToString(), data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]

        public ActionResult SaveData(string Id,string UserName,string TipeProperti, string Lokasi, string TipePenawaran, string Harga, string PhoneNumber,string PanjangRumah, bool Edit, string DateofBirth, string LebarRumah, string LuasRumah, string Address, string Deskripsi, string Image)
        {
            try
            {
                string validate = validateSubmit(Address,TipePenawaran,Edit);
                string msg = "";
                msg = validate;
                bool success = true;
                if (validate == "TRUE")
                {
                    if (Edit == false)
                    {
                        string MySql = @"insert into [Item] (UserName,TipeProperti,Lokasi,TipePenawaran,Harga,CreatedDate,PanjangRumah,PhoneNumber,LebarRumah,LuasRumah,Address,Deskripsi,Image) 
                        values (@UserName,@TipeProperti, @Lokasi, @TipePenawaran,@Harga,CURRENT_TIMESTAMP,@PanjangRumah,@PhoneNumber,@LebarRumah,@LuasRumah,@Address,@Deskripsi,@Image)";
                        GenConnString.Open();
                        cmd = new SqlCommand(MySql, GenConnString);
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@TipeProperti", TipeProperti);
                        cmd.Parameters.AddWithValue("@Lokasi", Lokasi);
                        cmd.Parameters.AddWithValue("@TipePenawaran", TipePenawaran);
                        cmd.Parameters.AddWithValue("@Harga", Harga);
                        cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                        cmd.Parameters.AddWithValue("@PanjangRumah", PanjangRumah);
                        cmd.Parameters.AddWithValue("@LebarRumah", LebarRumah);
                        cmd.Parameters.AddWithValue("@LuasRumah", LuasRumah);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@Deskripsi", Deskripsi);
                        cmd.Parameters.AddWithValue("@Image", Image);
                        cmd.ExecuteNonQuery();
                        msg = "Data Save Succesfully";
                    }
                    else
                    {
                        string MySql = @"update [Item] set UserName=@UserName,TipeProperti=@TipeProperti,Lokasi=@Lokasi,UpdateDate=CURRENT_TIMESTAMP,PhoneNumber=@PhoneNumber,TipePenawaran=@TipePenawaran
                        ,Harga=@Harga, PanjangRumah=@PanjangRumah, LebarRumah=@LebarRumah, LuasRumah=@LuasRumah, Address=@Address, Deskripsi=@Deskripsi, Image=@Image";
                        MySql += " where Id=@Id";
                        GenConnString.Open();
                        cmd = new SqlCommand(MySql, GenConnString);
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@TipeProperti", TipeProperti);
                        cmd.Parameters.AddWithValue("@Lokasi", Lokasi);
                        cmd.Parameters.AddWithValue("@TipePenawaran", TipePenawaran);
                        cmd.Parameters.AddWithValue("@Harga", Harga);
                        cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                        cmd.Parameters.AddWithValue("@PanjangRumah", PanjangRumah);
                        cmd.Parameters.AddWithValue("@LebarRumah", LebarRumah);
                        cmd.Parameters.AddWithValue("@LuasRumah", LuasRumah);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@Deskripsi", Deskripsi);
                        cmd.Parameters.AddWithValue("@Image", Image);
                        cmd.ExecuteNonQuery();
                        msg = "Data Update Succesfully";
                    }
                }
                else
                {
                    success = false;
                }
                return Json(new { success = success, message = msg}, JsonRequestBehavior.AllowGet);
            }


            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(string Id)
        {
            string MySql = (@"Delete [Item] where  Id =  '" + Id + "'");

            TempData["SuccessMessage"] = "Deleted Successfully";
            GenConnString.Open();
            cmd = new SqlCommand(MySql, GenConnString);
            //cmd.Parameters.AddWithValue("@GroupID", GroupID);
            cmd.ExecuteNonQuery();

            return RedirectToAction("EditItem");
        }

        [HttpGet]
        public ActionResult DeleteImage(string FilePath)
        {
            try
            {
                string path = Server.MapPath(FilePath);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.ToString(), data = "" }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public ActionResult InputImage(HttpPostedFileBase dir, string Address, string TipePenawaran)
        {
            HttpFileCollectionBase files = Request.Files;
            string physicalPath = "";
            string returnpath = "";
            string mappath = "~/Image/";
            //int b = files.Count;

            for (int i = 0; i < files.Count; i++)
            {
                dir = files[i];
            }
            if (dir != null)
            {
                string path = Path.Combine(Server.MapPath(mappath), Path.GetFileName(dir.FileName));
                physicalPath = Server.MapPath(mappath + Address + TipePenawaran + Path.GetExtension(dir.FileName));
                returnpath = mappath + Address + TipePenawaran + Path.GetExtension(dir.FileName);
                dir.SaveAs(physicalPath);

            }

            return Json(returnpath, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Getdetail(string Id)
        {
            try
            {

                DataTable DtResult = new DataTable();
                //SqlConnection MyCon = new SqlConnection(GenConnString);

                string MySql = @" select Id,UserName,TipeProperti,Lokasi,PhoneNumber,TipePenawaran,Harga,PanjangRumah,LebarRumah,LuasRumah,Address,Deskripsi,Image from [Item] where Id = @Id";

                GenConnString.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd = new SqlCommand(MySql, GenConnString);
                    cmd.Parameters.AddWithValue("@Id", Id);
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
                    Id = row["Id"],
                    UserName = row["UserName"],
                    TipeProperti = row["TipeProperti"],
                    Lokasi = row["Lokasi"],
                    Address = row["Address"],
                    TipePenawaran = row["TipePenawaran"],
                    PanjangRumah = row["PanjangRumah"],
                    LebarRumah = row["LebarRumah"],
                    LuasRumah = row["LuasRumah"],
                    PhoneNumber = row["PhoneNumber"],
                    Deskripsi = row["Deskripsi"],
                    Image = row["Image"],
                    Harga = row["Harga"],
                });

                return Json(new { success = true, data = convert }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.ToString(), data = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public string validateSubmit(string Address, string TipePenawaran, bool edit)
        {
            try
            {
                int Count = 0;
                //ArrayList param = new ArrayList();
                //ArrayList paramval = new ArrayList();
                string mySql;
                mySql = "SELECT Address,TipePenawaran FROM [Item] WHERE Address like '%' + LTRIM(RTRIM(@Address))  and TipePenawaran= @TipePenawaran ";
                //param.Add("@groupid"); paramval.Add(groupid);
                //param.Add("@id"); paramval.Add(id);

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd = new SqlCommand(mySql, GenConnString);
                    cmd.Parameters.AddWithValue("@TipePenawaran", TipePenawaran);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        Count = dt.Rows.Count;

                    }
                }

                if (Count > 0 && !edit)
                {
                    return "You cannot spam your same information!";
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