using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using LoginPageMVC.Models;

namespace LoginPageMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private MySqlConnection scon = new MySqlConnection("Server=localhost;database=productsdb;userid=root;pwd=root");
        List<employee> elist = new List<employee>();
        public ActionResult ShowAllEmp()
        {
            scon.Open();
            MySqlDataAdapter adap = new MySqlDataAdapter("select * from employee", scon);
            DataSet ds = new DataSet();
            adap.Fill(ds, "emp");
            employee em = null;
            foreach(DataRow dr in ds.Tables["emp"].Rows)
            {
                em = new employee();
                em.Sno = int.Parse(dr["Sno"].ToString());
                em.EmpNumber = dr["Empno"].ToString();
                em.EmpName = dr["EmpName"].ToString();
                em.EmpJob = dr["Job"].ToString();
                em.EmpSal = int.Parse(dr["Salary"].ToString());
                em.EmpEmail = dr["Email"].ToString();
                elist.Add(em);
            }
            return View(elist);
        }
        public ActionResult LoginPage()
        {
            return View();
        }
        public ActionResult ShowOneEmp()
        {
            scon.Open();
            string Eno = Session["eno"].ToString();
            MySqlCommand cmd = new MySqlCommand("select * from employee where EmpNo=@eno", scon);
            cmd.Parameters.AddWithValue("@eno", Eno);
            MySqlDataReader dr = cmd.ExecuteReader();
            employee em = new employee();
            if(dr.Read()==true)
            {
                em.Sno = int.Parse(dr["Sno"].ToString());
                em.EmpNumber = dr["EmpNo"].ToString();
                em.EmpName = dr["EmpName"].ToString();
                em.EmpJob = dr["Job"].ToString();
                em.EmpSal = int.Parse(dr["Salary"].ToString());
                em.EmpEmail = dr["Email"].ToString();
            }
            scon.Close();
            return View(em);
            
        }

        [HttpPost]
        public ActionResult LoginPage(Login lo)
        {
            try
            {
                if(lo.username.Equals("admin") && lo.password.Equals("admin"))
                {
                    return RedirectToAction("ShowAllEmp", "Employee");
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("select * from employee where Email=@email and pwd=@pwd", scon);
                    cmd.Parameters.AddWithValue("@email", lo.username);
                    cmd.Parameters.AddWithValue("@pwd", lo.password);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if(dr.Read()==true)
                    {
                        Session["eno"] = dr["EmpNo"].ToString();
                        return RedirectToAction("ShowOneEmp", "Employee");
                        
                    }
                    else
                    {
                        ViewBag.empinfo = "Please check your Username / Password";
                    }
                }
            }
            catch(Exception e)
            {
                ViewBag.loginfo = e.Message;
            }
            finally
            {
                scon.Close();
            }
            return View();
        }
    }
}