using Test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Test.Controllers
{
    public class TestController : Controller
    {
        private readonly TestEntities _context = new TestEntities();
        // GET: Test
        public ActionResult List()
        {
            var list = _context.ContentQuestions.ToList().OrderByDescending(x => x.QuestionID);
            return View(list);
        }
        public ActionResult Create(int id = 0)
        {
            if (id > 0)
            {
                var obj = _context.ContentQuestions.Where(a => a.QuestionID == id).FirstOrDefault();
                TempData["Photo"] = obj.Image;
                return View(obj);
            }

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ContentQuestion contentQuestion, HttpPostedFileBase image)
        {

            if (image != null)
            {
                contentQuestion.Image = UploadPhoto(image);

            }
            else
            {
                contentQuestion.Image = TempData["Photo"].ToString();
            }
            if (contentQuestion.QuestionID > 0)
            {

                _context.Entry(contentQuestion).State = EntityState.Modified;
                _context.SaveChanges();
                ModelState.Clear();
            }
            else
            {
                _context.ContentQuestions.Add(contentQuestion);
                _context.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public ActionResult Index()
        {
            var users = _context.UserContents.ToList();
            return View(users);
        }

        // GET: User/Create
        public ActionResult Createcontent()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Createcontent(UserContent user)
        {
            if (ModelState.IsValid)
            {
                _context.UserContents.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public ActionResult Delete(string username)
        {
            var user = _context.UserContents.Find(username);
            _context.UserContents.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult Delete(string username)
        //{
        //    if (username == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserContent user = _context.UserContents.Find(username);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //// POST: UserContent/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string username)
        //{
        //    UserContent user = _context.UserContents.Find(username);
        //    _context.UserContents.Remove(user);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        private string UploadPhoto(HttpPostedFileBase image)
        {
            string result = string.Empty;
            if (image != null)
            {
                var fileName = GetRandomNumber(10) + image.FileName;
                image.SaveAs(HttpContext.Server.MapPath("~/Uploadedstaff/") + fileName);
                result = fileName;
            }
            return result;
        }
        public static string GetRandomNumber(int length)
        {
            var rnd = new Random();
            const string charPool = "1234567890";
            var rs = new StringBuilder();

            while (length-- > 0)
                rs.Append(charPool[(int)(rnd.NextDouble() * charPool.Length)]);

            return rs.ToString();
        }


    }
}