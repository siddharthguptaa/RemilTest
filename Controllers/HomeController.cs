using DTestAssign.DAL;
using DTestAssign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTestAssign.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(StudentModel obj)
        {
            if (!ModelState.IsValid)
                return Json(ModelState.Values.FirstOrDefault().Errors.Select(s => s.ErrorMessage.ToString()).FirstOrDefault());
            if (obj.City == "City")
                return Json("Please Select valid City");
            else
            {
                var stateId = DBString.GetStateId(obj.City);
                if (stateId != 0)
                    obj.CityId = stateId;
                else
                    return Json("Some issue occured while inserting data,please try again . ");
            }
            int inserted = DBString.InsertStudentDetails(obj);

            if (inserted != 0)
            {
                obj.StudentId = inserted;
                var value = DBString.InsertStudentMarks(obj);
                if (value)
                    return Json(new { success = true, message = "Data Inserted Successfully." });

                return Json(new { success = false, message = "Some issue occured while inserting data,please try again . " });
            }
            return Json(new { success = false, message = "Some issue occured while inserting data,please try again . " });
        }

        [HttpGet]
        public ActionResult CityNames()
        {
            var resp = DBString.GetCitiesName();
            return Json(new { success = true, result = resp }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult StateAndCountryNamebyCityName(string obj)
        {
            var resp = DBString.GetStateAndCountryNamebyCityName((string)obj);
            return Json(new { success = true, result = resp }, JsonRequestBehavior.AllowGet);
        }
    }
}