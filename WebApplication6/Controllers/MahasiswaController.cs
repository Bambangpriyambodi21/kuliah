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
    public class MahasiswaController : Controller
    {
        string connectionstring = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = MVCCRUD; Integrated Security = True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblmahasiswa = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("select * from mahasiswa", sqlcon);
                sqlda.Fill(dtblmahasiswa);
                sqlcon.Close();
            }
            return View(dtblmahasiswa);
        }

        [HttpGet]
        public ActionResult create()
        {
            return View(new MahasiswaModel());
        }

        // POST: Mahasiswa/Create
        [HttpPost]
        public ActionResult Create(MahasiswaModel mahasiswaModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "insert into mahasiswa values (@Nim,@Nama_Mhs,@Tgl_Lahir,@Alamat,@Jenis_Kelamin)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@Nim", mahasiswaModel.Nim);
                sqlCmd.Parameters.AddWithValue("@Nama_Mhs", mahasiswaModel.Nama_Mhs);
                sqlCmd.Parameters.AddWithValue("@Tgl_Lahir", mahasiswaModel.Tgl_Lahir);
                sqlCmd.Parameters.AddWithValue("@Alamat", mahasiswaModel.Alamat);
                sqlCmd.Parameters.AddWithValue("@Jenis_Kelamin", mahasiswaModel.Jenis_Kelamin);
                sqlCmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            return RedirectToAction("Index");
        }

        // GET: Mahasiswa/Edit/5
        public ActionResult Edit(int id)
        {
            MahasiswaModel mahasiswaModel = new MahasiswaModel();
            DataTable dtblmahasiswa = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                string query = "select * from mahasiswa where Nim = @Nim";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlCon);
                sqlda.SelectCommand.Parameters.AddWithValue("@Nim", id);
                sqlda.Fill(dtblmahasiswa);
                sqlCon.Close();
            }
            if (dtblmahasiswa.Rows.Count == 1)
            {
                mahasiswaModel.Nim = dtblmahasiswa.Rows[0][0].ToString();
                mahasiswaModel.Nama_Mhs = dtblmahasiswa.Rows[0][1].ToString();
                mahasiswaModel.Tgl_Lahir = dtblmahasiswa.Rows[0][2].ToString();
                mahasiswaModel.Alamat = dtblmahasiswa.Rows[0][3].ToString();
                mahasiswaModel.Jenis_Kelamin = dtblmahasiswa.Rows[0][4].ToString();
                return View(mahasiswaModel);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Perkuliahan/Edit/5
        [HttpPost]
        public ActionResult Edit(MahasiswaModel mahasiswaModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "update mahasiswa set Nama_Mhs = @Nama_Mhs, Tgl_Lahir= @Tgl_Lahir, Alamat=@Alamat, Jenis_Kelamin=@Jenis_Kelamin where Nim = @Nim";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@Nim", mahasiswaModel.Nim);
                sqlCmd.Parameters.AddWithValue("@Nama_Mhs", mahasiswaModel.Nama_Mhs);
                sqlCmd.Parameters.AddWithValue("@Tgl_Lahir", mahasiswaModel.Tgl_Lahir);
                sqlCmd.Parameters.AddWithValue("@Alamat", mahasiswaModel.Alamat);
                sqlCmd.Parameters.AddWithValue("@Jenis_Kelamin", mahasiswaModel.Jenis_Kelamin);
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
                string query = "delete from mahasiswa where Nim = @Nim";
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
            DataTable dtblmhs = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "SELECT TOP " + limit + " * FROM mahasiswa WHERE Nama_Mhs like '%" + id + "%' ";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlcon);
                sqlda.Fill(dtblmhs);
                sqlcon.Close();
            }
            return View(dtblmhs);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View(new MahasiswaModel());
        }
    }
}