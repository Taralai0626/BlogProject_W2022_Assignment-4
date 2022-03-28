﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        public string TeacherFName { get; set; }

        public string TeacherLName { get; set; }

        public DateTime TeacherHireDate { get; set; }

        public decimal TeacherSalary { get; set; }
    }
}