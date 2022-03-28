using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;
using System.Diagnostics;

namespace BlogProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // Get: Teacher/List
        // Showing a page of all teachers in the system
        [Route("Teacher/List/{SearchKey}")]
        public ActionResult List(string SearchKey)
        {
            //debugging message to see if we have gethered the key
            Debug.WriteLine("The key is " + SearchKey);
            //connect to our data acceess layer
            //gere our teachers
            //pass the teachers to the view Theacher/List.cshtml
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);

            return View(Teachers);
        }

        // GET : /Teacher/Show/{id}
        //[Route("Teacher/Show/{TeacherId}")]

        public ActionResult Show(int Id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(Id);

            //routes the single teacher info to show.cshtml
            return View(SelectedTeacher);
        }
    }
}