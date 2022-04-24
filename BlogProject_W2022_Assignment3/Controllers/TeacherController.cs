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
        public ActionResult List(string SearchKey = null)
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

        // GET : /Teacher/DeleteConfirm/{id}
        //[Route("Teacher/DeleteConfirm/{TeacherId}")]
        public ActionResult DeleteConfirm(int Id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(Id);

            //routes the single teacher info to show.cshtml
            return View(SelectedTeacher);
        }

        //POST: /Teacher/Delete/{id}
        [HttpPost]
           public ActionResult Delete(int Id)
            { 
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(Id);
            return RedirectToAction("List");
        }

        //Get: /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //Post: /Teacher/Create
        [HttpPost]
        public ActionResult Create(string teacherfname, string teacherlname, string employeenumber)
        {
            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(teacherfname);
            Debug.WriteLine(teacherlname);
            Debug.WriteLine(employeenumber);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFName = teacherfname;
            NewTeacher.TeacherLName = teacherlname;
            NewTeacher.EmployeeNumber = employeenumber;
            
            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);
            //we want to do the following;
            //connect to a database
            //insert into teachers
            //witht provided values
            
            //redirects immediately to the list view
            return RedirectToAction("List");
        }
        
        //Get:/Teahcer/Edit/{id}
        /// <summary>
        /// return a webpage of the teacher
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Edit(int Id)
        {
            // I need to pass teacher info to the view to show that to the user
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(Id);


            return View(SelectedTeacher);
        }
        /// <summary>
        /// This method actually updates the theacher
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //POST:/Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int Id, string teacherfname, string teacherlname, string employeenumber)
        {
            Debug.WriteLine("The teacher name is " + teacherfname);
            Debug.WriteLine("The ID is " + Id);

            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFName = teacherfname;
            TeacherInfo.TeacherLName = teacherlname;
            TeacherInfo.EmployeeNumber = employeenumber;
            TeacherInfo.TeacherId = Id;

            //update the teahccer information
            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(Id, TeacherInfo);



            //return to the teacher that i just changed 
            return RedirectToAction("/Show/" + Id);
        }
    }
}