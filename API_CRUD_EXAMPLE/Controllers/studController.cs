﻿using API_CRUD_EXAMPLE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace API_CRUD_EXAMPLE.Controllers
{
    public class studController : Controller
    {
        // GET: stud
        public ActionResult Index()
        {
            IEnumerable<Student> students = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54842/");
                //HTTP GET
                var responseTask = client.GetAsync("list");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Student>>();
                    readTask.Wait();

                    students = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<Student>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(students);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54842/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("add", student);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(student);
        }

        public ActionResult Details(int id)
        {
            Student student = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54842/");
                //HTTP GET
                var responseTask = client.GetAsync("by_id?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();

                    student = readTask.Result;
                }
            }

            return View(student);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Student student = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54842/api/");
                //HTTP GET
                var responseTask = client.GetAsync("student?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();

                    student = readTask.Result;
                }
            }

            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54842/api/");

                //HTTP POST
                var putTask = client.PostAsJsonAsync("student", student);
                putTask.Wait();


                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(student);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54842/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("delete/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

    }
}