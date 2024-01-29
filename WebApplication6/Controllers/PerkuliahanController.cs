using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class PerkuliahanController : Controller
    {
        string connectionstring = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = MVCCRUD; Integrated Security = True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblperkuliahan = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("select * from perkuliahan", sqlcon);
                sqlda.Fill(dtblperkuliahan);
                sqlcon.Close();
            }
            return View(dtblperkuliahan);
        }

        [HttpGet]
        public ActionResult create()
        {
            return View(new PerkuliahanModel());
        }

        // POST: Perkuliahan/Create
        [HttpPost]
        public ActionResult Create(PerkuliahanModel perkuliahanModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "insert into perkuliahan values (@Nim,@Kode_MK,@Nip,@Nilai)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@Nim", perkuliahanModel.Nim);
                sqlCmd.Parameters.AddWithValue("@Kode_MK", perkuliahanModel.Kode_MK);
                sqlCmd.Parameters.AddWithValue("@Nip", perkuliahanModel.Nip);
                sqlCmd.Parameters.AddWithValue("@Nilai", perkuliahanModel.Nilai);
                sqlCmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            return RedirectToAction("Index");
        }

        // GET: MataKuliah/Edit/5
        public ActionResult Edit(int id)
        {
            PerkuliahanModel perkuliahanModel = new PerkuliahanModel();
            DataTable dtblperkuliahan = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                string query = "select * from perkuliahan where Nim = @Nim";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlCon);
                sqlda.SelectCommand.Parameters.AddWithValue("@Nim", id);
                sqlda.Fill(dtblperkuliahan);
                sqlCon.Close();
            }
            if (dtblperkuliahan.Rows.Count == 1)
            {
                perkuliahanModel.Nim = dtblperkuliahan.Rows[0][0].ToString();
                perkuliahanModel.Kode_MK = dtblperkuliahan.Rows[0][1].ToString();
                perkuliahanModel.Nip = dtblperkuliahan.Rows[0][2].ToString();
                perkuliahanModel.Nilai = Convert.ToInt32(dtblperkuliahan.Rows[0][3].ToString());
                return View(perkuliahanModel);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Perkuliahan/Edit/5
        [HttpPost]
        public ActionResult Edit(PerkuliahanModel perkuliahanModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "update perkuliahan set Kode_MK = @Kode_MK, Nip= @Nip, Nilai=@Nilai where Nim = @Nim";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@Nim", perkuliahanModel.Nim);
                sqlCmd.Parameters.AddWithValue("@Kode_MK", perkuliahanModel.Kode_MK);
                sqlCmd.Parameters.AddWithValue("@Nip", perkuliahanModel.Nip);
                sqlCmd.Parameters.AddWithValue("@Nilai", perkuliahanModel.Nilai);
                sqlCmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            return RedirectToAction("Index");
        }

        // GET: Perkuliahan/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "delete from perkuliahan where Nim = @Nim";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@Nim", id);
                sqlCmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult IndexSearch(String limit, String id)
        {
            DataTable dtblPerkuliahan = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "SELECT TOP " + limit + " * FROM perkuliahan WHERE Nim like '%" + id + "%' ";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlcon);
                sqlda.Fill(dtblPerkuliahan);
                sqlcon.Close();
            }
            return View(dtblPerkuliahan);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View(new PerkuliahanModel());
        }
    }
}
