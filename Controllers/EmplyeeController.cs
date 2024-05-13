using EF_CRUD.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF_CRUD.Controllers
{

    public class EmplyeeController : Controller
    {
        TestingDBEntities dbObj = new TestingDBEntities();

        [HttpGet]
        public ActionResult Employee(ETable em)
        {
            if (em != null)
            {
                return View( em);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddEmployee(ETable em)
        {           
               if (ModelState.IsValid)
               {
                    ETable model = new ETable();
                    model.EId = em.EId;
                    model.EName = em.EName;
                    model.EAddress = em.EAddress;
                    model.Eage = em.Eage;
                    model.Esallery = em.Esallery;

                   if (em.EId == 0)
                   {
                     dbObj.ETables.Add(model);
                     dbObj.SaveChanges();
                   }
                   else
                   {
                     dbObj.Entry(model).State = System.Data.Entity.EntityState.Modified;
                     dbObj.SaveChanges();
                   }
               }
               ModelState.Clear();

            
            return RedirectToAction("EmployeeList");
            
        }


        public ActionResult EmployeeList()
        {
            var response = dbObj.ETables.ToList();
            return View(response);
        }
        
        public ActionResult Delete(int Eid)
        {
            var res = dbObj.ETables.Where(m => m.EId == Eid).FirstOrDefault();
            if (res != null) 
            { 
              dbObj.ETables.Remove(res);
              dbObj.SaveChanges();
            }
            var list = dbObj.ETables.ToList();
            return View("EmployeeList", list);
        }

        public ActionResult Details(int id)
        {
            var res = dbObj.ETables.Where(m => m.EId == id).FirstOrDefault();

            return View(res);
        }
    }
}