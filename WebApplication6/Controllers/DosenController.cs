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
    public class DosenController : Controller
    {
        string connectionstring = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = MVCCRUD; Integrated Security = True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtbldosen = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("select * from dosen", sqlcon);
                sqlda.Fill(dtbldosen);
                sqlcon.Close();
            }
            return View(dtbldosen);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new DosenModel());
        }

        // POST: Dosen/Create
        [HttpPost]
        public ActionResult Create(DosenModel dosenModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "insert into dosen values (@Nip,@Nama_Dosen)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@Nip", dosenModel.Nip);
                sqlCmd.Parameters.AddWithValue("@Nama_Dosen", dosenModel.Nama_Dosen);
                sqlCmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            return RedirectToAction("Index");
        }

        // GET: Dosen/Edit/5
        public ActionResult Edit(int id)
        {
            DosenModel dosenModel = new DosenModel();
            DataTable dtblDosen = new DataTable();
            using(SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                string query = "select * from dosen where Nip = @Nip";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlCon);
                sqlda.SelectCommand.Parameters.AddWithValue("@Nip", id);
                sqlda.Fill(dtblDosen);
                sqlCon.Close();
            }
            if(dtblDosen.Rows.Count ==1)
            {
                dosenModel.Nip = dtblDosen.Rows[0][0].ToString();
                dosenModel.Nama_Dosen = dtblDosen.Rows[0][1].ToString();
                return View(dosenModel);
            }
            else
                return RedirectToAction("Index");
            }

        // POST: Dosen/Edit/5
        [HttpPost]
        public ActionResult Edit(DosenModel dosenModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "update dosen set Nama_Dosen= @Nama_Dosen where Nip = @Nip";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@Nip", dosenModel.Nip);
                sqlCmd.Parameters.AddWithValue("@Nama_Dosen", dosenModel.Nama_Dosen);
                sqlCmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            return RedirectToAction("Index");
        }

        // GET: Dosen/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "delete from dosen where Nip = @Nip";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@Nip", id);
                sqlCmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult IndexSearch(String limit, String id)
        {
            DataTable dtbldosen = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "SELECT TOP "+limit+" * FROM dosen WHERE Nama_Dosen like '%" + id + "%' ";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlcon);
                sqlda.Fill(dtbldosen);
                sqlcon.Close();
            }
            return View(dtbldosen);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View(new DosenModel());
        }


    }
}
