using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Test.Models;

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

            //if (image != null)
            //{
            //    contentQuestion.Image = UploadPhoto(image).ToString();

            //}
            //else
            //{
            //    contentQuestion.Image = TempData["Photo"].ToString();
            //}

            if (contentQuestion.QuestionID > 0)
            {
                _context.Entry(contentQuestion).State = EntityState.Modified;
                _context.SaveChanges();
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

        // GET: UserContent/Details/5
        public ActionResult Details(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserContent user = _context.UserContents.Find(username);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        // GET: UserContent/Edit/5
        public ActionResult Edit(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserContent user = _context.UserContents.Find(username);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: UserContent/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserContent user)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: UserContent/Delete/5
        public ActionResult Delete(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserContent user = _context.UserContents.Find(username);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: UserContent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string username)
        {
            UserContent user = _context.UserContents.Find(username);
            _context.UserContents.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        //private byte[] UploadPhoto(HttpPostedFileBase image)
        //{
        //    throw new NotImplementedException();
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