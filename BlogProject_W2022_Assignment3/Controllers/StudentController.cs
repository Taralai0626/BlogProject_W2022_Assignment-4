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
        //[Route("Student/Show/{StudentId}")]

        public ActionResult Show(int Id)
        {
            StudentDataController controller = new StudentDataController();
            Student SelectedStudnt = controller.FindStudent(Id);

            //routes the single Student info to show.cshtml
            return View(SelectedStudnt);
        }

        // GET : /Student/DeleteConfirm/{id}
        //[Route("Student/DeleteConfirm/{StudentId}")]
        public ActionResult DeleteConfirm(int Id)
        {
            StudentDataController controller = new StudentDataController();
            Student SelectedStudnt = controller.FindStudent(Id);

            //routes the single teacher info to show.cshtml
            return View(SelectedStudnt);
        }

        //POST: /Student/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            StudentDataController controller = new StudentDataController();
            controller.DeleteStudent(Id);
            return RedirectToAction("List");
        }

        //Get: /Student/New
        public ActionResult New()
        {
            return View();
        }

        //Post: /Student/Create
        [HttpPost]
        public ActionResult Create(string studentfname, string studentlname, string studentnumber)
        {
            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(studentfname);
            Debug.WriteLine(studentlname);
            Debug.WriteLine(studentnumber);

            Student NewStudent = new Student();
            NewStudent.StudentFName = studentfname;
            NewStudent.StudentLName = studentlname;
            NewStudent.StudentNum = studentnumber;

            StudentDataController controller = new StudentDataController();
            controller.AddStudent(NewStudent);
            //we want to do the following;
            //connect to a database
            //insert into Student
            //witht provided values

            //redirects immediately to the list view
            return RedirectToAction("List");
        }

    }
}
