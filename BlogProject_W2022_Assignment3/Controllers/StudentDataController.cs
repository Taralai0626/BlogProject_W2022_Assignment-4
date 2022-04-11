using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogProject.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;


namespace BlogProject.Controllers
{
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the students table of our blog database.
        /// <summary>
        /// Returns a list of studdents in the system
        /// </summary>
        /// <param name="SearchKey">search key (optional) of student name</param>
        /// <example>GET api/StudentrData/ListStudnet</example>
        /// <example>GET api/StudentrData/ListStudnet/Austin</example>
        /// <returns>
        /// A list of Student obejects (ID, first names,last names)
        /// </returns>
        [HttpGet]
        [Route("api/studentdata/liststudent/{searchkey?}")]
        public List<Student> ListStudents(string SearchKey = null)
        {
            if (SearchKey != null)
            {
                Debug.WriteLine("The searchkey is " + SearchKey);
            }

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "Select * from students";
            if (SearchKey != null)
            {
                query = query + " where lower(studentfname) = lower(@key)";
                cmd.Parameters.AddWithValue("@key", SearchKey);
                cmd.Prepare();
            }

            Debug.WriteLine("The query is : " + query);
            cmd.CommandText = query;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Student Names
            List<Student> Students = new List<Student> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Student NewStudent = new Student();
                NewStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                NewStudent.StudentFName = ResultSet["studentfname"].ToString();
                NewStudent.StudentLName = ResultSet["studentlname"].ToString();
                NewStudent.StudentNum = ResultSet["studentnumber"].ToString();
                //NewStudent.StudentEnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);

                //Add the Student Name to the List
                Students.Add(NewStudent);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Student names
            return Students;
        }

        [HttpGet]
        [Route("api/studentdata/findstudent/{studentid}")]

        public Student FindStudent(int studentid)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "select * from students where studentid=@id";
            cmd.Parameters.AddWithValue("@id", studentid);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Student Names
            Student SelectedStudent = new Student();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
                while (ResultSet.Read())
                {
                    SelectedStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                    SelectedStudent.StudentFName = (ResultSet["studentfname"]).ToString();
                    SelectedStudent.StudentLName = (ResultSet["studentlname"]).ToString();
                    SelectedStudent.StudentNum = ResultSet["studentnumber"].ToString();
                   // SelectedStudent.StudentEnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);

                }
            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of student names
            return SelectedStudent;
        }

        /// <summary>
        /// Adds a new Student to the system given Student information
        /// <paramref name="NewStudent"/> Student information to add </paramref>
        /// </summary>
        public void AddStudent(Student NewStudent)
        {
            //create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and database
            Conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            string query = "insert into teachers (studentfname, studentlname, studentnumber) values(@studentfname,@studentlname,@studentnumber)";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@studentfname", NewStudent.StudentFName);
            cmd.Parameters.AddWithValue("@studentlname", NewStudent.StudentLName);
            cmd.Parameters.AddWithValue("@studentnumber", NewStudent.StudentNum);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            Conn.Close();
        }
        /// <summary>
        /// delete a Student in the system
        /// </summary>
        /// <param name="StudentId">the primary key StudentId</param>
        public void DeleteStudent(int StudentId)
        {
            //create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and database
            Conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            string query = "delete from students where studentid = @id";
            cmd.Parameters.AddWithValue("@id", StudentId);
            cmd.CommandText = query;
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }



    }

}
