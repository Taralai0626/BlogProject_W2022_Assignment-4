using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class Student
    {
        // GET: Students
        public int StudentId { get; set; }

        public string StudentFName { get; set; }

        public string StudentLName { get; set; }

        public string StudentNum { get; set; }

       // public DateTime StudentEnrolDate { get; set; }

    }
}