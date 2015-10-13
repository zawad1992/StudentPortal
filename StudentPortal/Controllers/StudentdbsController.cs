using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentPortal.Models;
using System.Web.Security;

namespace StudentPortal.Controllers
{
    public class StudentdbsController : Controller
    {
        private StudentContext db = new StudentContext();

        //
        // GET: /Studentdbs/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Studentdbs.ToList());
        }

        //
        // GET: /Studentdbs/Details/5
        
        public ActionResult Details(int id = 0)
        {
            Student student = db.Studentdbs.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // GET: /Studentdbs/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Studentdbs/Create
        
        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Studentdbs.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(student);
        }

        //
        // GET: /Studentdbs/Edit/5
        
        public ActionResult Edit(int id = 0)
        {
            Student student = db.Studentdbs.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // POST: /Studentdbs/Edit/5
        
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        //
        // GET: /Studentdbs/Delete/5
        
        public ActionResult Delete(int id = 0)
        {
            Student student = db.Studentdbs.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        
        public ActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Login(StdLoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsValid(model.EMAIL, model.PASSWORD))
                {

                    FormsAuthentication.SetAuthCookie(model.EMAIL, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Studentdbs/Delete/5
        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Studentdbs.Find(id);
            db.Studentdbs.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}