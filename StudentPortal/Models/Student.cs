using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Configuration;
using System.Data.Common;


namespace StudentPortal.Models
{
    public class Student
    {
        private StudentContext db = new StudentContext();
        [Required]
        public int ID { get; set; }
        [Required]
        public string NAME { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EMAIL { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PASSWORD { get; set; }
    }
    public class StdLoginModel
    {
        [Required]
        [Display(Name = "User name / Email")]
        public string EMAIL { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PASSWORD { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public bool IsValid(string _email, string _pwd)
        {
            string conStr = ConfigurationManager.ConnectionStrings["StudentContext"].ConnectionString;
            string _sql = "Select EMAIL From Students Where EMAIL='" + _email + "' And PASSWORD='" + _pwd + "'";
            SqlCeConnection cn = new SqlCeConnection();
            cn.ConnectionString = conStr;
           
            cn.Open();
            SqlCeCommand cmd = new SqlCeCommand(_sql, cn);
            SqlCeDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
                return true;
            else
                return false;
        }
    }
    
    public class StudentContext : DbContext
    {
        public DbSet<Student> Studentdbs { get; set; }
    }
}