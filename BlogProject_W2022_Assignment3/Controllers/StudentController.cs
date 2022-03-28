using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;
using System.Diagnostics;


namespace BlogProject.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        // Get: Student/List
        // Showing a page of all teachers in the system
        [Route("Student/List/{SearchKey}")]
        public ActionResult List(string SearchKey)
        {
            //debugging message to see if we have gethered the key
            Debug.WriteLine("The key is " + SearchKey);
            //connect to our data acceess layer
            //gere our student
            //pass the student to the view Student/List.cshtml
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents(SearchKey);

            return View(Students);
        }

        // GET : /Student/Show/{id}
        //[Route("Student/Show/{TeacherId}")]

        public ActionResult Show(int Id)
        {
            StudentDataController controller = new StudentDataController();
            Student SelectedStudnt = controller.FindStudent(Id);

            //routes the single teacher info to show.cshtml
            return View(SelectedStudnt);
        }
    }
}
