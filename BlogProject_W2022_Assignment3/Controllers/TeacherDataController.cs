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
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the teachers table of our blog database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <param name="SearchKey">search key (optional) of teacher name</param>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <example>GET api/TeacherData/ListTeachers/alexander</example>
        /// <returns>
        /// A list of Tecaher obejects (ID, first names,last names)
        /// </returns>
        [HttpGet]
        [Route("api/teacherdata/listteachers/{searchkey?}")]
        public List<Teacher> ListTeachers(string SearchKey = null)
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
            string query = "Select * from teachers";

            if (SearchKey != null)
            {
                query = query + " where lower(teacherfname) = lower(@key)";
                cmd.Parameters.AddWithValue("@key", SearchKey);
                cmd.Prepare();
            }

            Debug.WriteLine("The query is : " + query);
            cmd.CommandText = query;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                NewTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();

                //NewTeacher.TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                //NewTeacher.TeacherSalary = Convert.ToDecimal(ResultSet["salary"]);

                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Teacher names
            return Teachers;
        }

        [HttpGet]
        [Route("api/teacherdata/findteacher/{teacherid}")]

        public Teacher FindTeacher(int teacherid)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "select * from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", teacherid);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            Teacher SelectedTeacher = new Teacher();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.TeacherFName = (ResultSet["teacherfname"]).ToString();
                SelectedTeacher.TeacherLName = (ResultSet["teacherlname"]).ToString();
                SelectedTeacher.EmployeeNumber = (ResultSet["employeenumber"]).ToString();
                //SelectedTeacher.TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                //SelectedTeacher.TeacherSalary = Convert.ToDecimal(ResultSet["salary"]);
            }
            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Teacher names
            return SelectedTeacher;
        }
        /// <summary>
        /// Adds a new teacher to the system given teacher information
        /// <paramref name="NewTeacher"/> Teacher information to add </paramref>
        /// </summary>
        public void AddTeacher(Teacher NewTeacher)
        {
            //create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and database
            Conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            string query = "insert into teachers (teacherfname, teacherlname, employeenumber) values(@teacherfname,@teacherlname,@employeenumber)";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.EmployeeNumber);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            Conn.Close();
        }
        /// <summary>
        /// delete a teacher in the system
        /// </summary>
        /// <param name="TeacherId">the primary key teacherid</param>
        
        public void DeleteTeacher(int TeacherId)
        {
            //create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and database
            Conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            string query = "delete from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", TeacherId);
            cmd.CommandText = query;
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }
        



    }

}
