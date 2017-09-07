using API_CRUD_EXAMPLE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_CRUD_EXAMPLE.Controllers
{
    public class studentController : ApiController
    {
        [Route("list")]
        [HttpGet]
        public IHttpActionResult display_students()
        {
            List<Student> s = new List<Student>();
            advancedEntities dc = new advancedEntities();
            s = dc.Students.ToList();
            return Ok(s);
        }
        [HttpGet]
        [Route("by_id")]
        public IHttpActionResult get_student(int id)
        {
            Student s = new Student();
            advancedEntities dc = new advancedEntities();
            s = (from st in dc.Students
                 where st.Id == id
                 select st).FirstOrDefault();
            if (s == null)
                return Ok("no student found");
            else
                return Ok(s);
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult add_student(Student st)
        {

            advancedEntities dc = new advancedEntities();
            Student s = new Student();

            s.Name = st.Name;
            s.Batch = st.Batch;
            s.Year = st.Year;
            dc.Students.Add(s);
            dc.SaveChanges();
            return Ok("Added new student");

        }
        [Route("delete")]

        [HttpGet]
        public IHttpActionResult delete_student(int id)
        {
            advancedEntities dc = new advancedEntities();
            Student s = new Student();
            s = (from st in dc.Students
                 where st.Id == id
                 select st).FirstOrDefault();
            return Ok(s);
        }
        [Route("delete")]
        [HttpPost]
        public IHttpActionResult delete_student(Student s)
        {
            advancedEntities dc = new advancedEntities();
             s = new Student();
            s = (from st in dc.Students
                 where st.Id == s.Id
                 select st).FirstOrDefault();
            if (s == null)
                return Ok(" Student NotFound");
            else
            {
                dc.Entry(s).State = System.Data.Entity.EntityState.Deleted;
                dc.SaveChanges();
            }
            return Ok();
        }

       

        [HttpGet]
        public IHttpActionResult update_student(int id)
        {
            advancedEntities dc = new advancedEntities();
            Student s = new Student();
            s = (from st in dc.Students
                 where st.Id == id
                 select st).FirstOrDefault();
            return Ok(s);
        }
        [HttpPost]
        public IHttpActionResult update_student(Student s)
        {
            advancedEntities dc = new advancedEntities();

            var old_student = (from st in dc.Students
                               where st.Id == s.Id
                               select st).FirstOrDefault();
            if (old_student == null)
                return Ok("no student found");
            else
            {
                old_student.Name = s.Name;
                old_student.Batch = s.Batch;
                old_student.Year = s.Year;
                dc.Entry(old_student).CurrentValues.SetValues(s);
                dc.SaveChanges();
            }
            return Ok("Edited Successfully");
        }


    }
}

