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
    public class MataKuliahController : Controller
    {
        string connectionstring = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = MVCCRUD; Integrated Security = True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblmk = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("select * from matakuliah", sqlcon);
                sqlda.Fill(dtblmk);
                sqlcon.Close();
            }
            return View(dtblmk);
        }

        [HttpGet]
        public ActionResult create()
        {
            return View(new MataKuliahModel());
        }

        // POST: MataKuliah/Create
        [HttpPost]
        public ActionResult Create(MataKuliahModel mataKuliahModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "insert into matakuliah values (@Kode_MK,@Nama_MK,@Sks)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@Kode_MK", mataKuliahModel.Kode_MK);
                sqlCmd.Parameters.AddWithValue("@Nama_MK", mataKuliahModel.Nama_MK);
                sqlCmd.Parameters.AddWithValue("@Sks", mataKuliahModel.Sks);
                sqlCmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            return RedirectToAction("Index");
        }

        // GET: MataKuliah/Edit/5
        public ActionResult Edit(int id)
        {
            MataKuliahModel mataKuliahModel = new MataKuliahModel();
            DataTable dtblMataKuliah = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                string query = "select * from matakuliah where Kode_MK = @Kode_MK";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlCon);
                sqlda.SelectCommand.Parameters.AddWithValue("@Kode_MK", id);
                sqlda.Fill(dtblMataKuliah);
                sqlCon.Close();
            }
            if (dtblMataKuliah.Rows.Count == 1)
            {
                mataKuliahModel.Kode_MK = dtblMataKuliah.Rows[0][0].ToString();
                mataKuliahModel.Nama_MK = dtblMataKuliah.Rows[0][1].ToString();
                mataKuliahModel.Sks = Convert.ToInt32(dtblMataKuliah.Rows[0][2].ToString());
                return View(mataKuliahModel);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: MataKuliah/Edit/5
        [HttpPost]
        public ActionResult Edit(MataKuliahModel mataKuliahModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "update matakuliah set Nama_MK= @Nama_MK, Sks= @Sks where Kode_MK = @Kode_MK";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@Kode_MK", mataKuliahModel.Kode_MK);
                sqlCmd.Parameters.AddWithValue("@Nama_MK", mataKuliahModel.Nama_MK);
                sqlCmd.Parameters.AddWithValue("@Sks", mataKuliahModel.Sks);
                sqlCmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            return RedirectToAction("Index");
        }

        // GET: MataKuliah/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "delete from matakuliah where Kode_MK = @Kode_MK";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@Kode_MK", id);
                sqlCmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult IndexSearch(String limit, String id)
        {
            DataTable dtblMk = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "SELECT TOP " + limit + " * FROM matakuliah WHERE Nama_MK like '%" + id + "%' ";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlcon);
                sqlda.Fill(dtblMk);
                sqlcon.Close();
            }
            return View(dtblMk);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View(new MataKuliahModel());
        }
    }
}