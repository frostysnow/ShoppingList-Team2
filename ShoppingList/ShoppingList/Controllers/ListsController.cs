using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingList.Models;
using ShoppingList.Models.ShoppingListModels;

namespace ShoppingList.Controllers
{
    public class ListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ListOfListsViewModels
        public ActionResult Index()
        {
            return View(db.ListOfListsViewModels.ToList());
        }

        // GET: ListOfListsViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListOfListsViewModel listOfListsViewModel = db.ListOfListsViewModels.Find(id);
            if (listOfListsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(listOfListsViewModel);
        }

        // GET: ListOfListsViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListOfListsViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ListId,ListName,ListContent,Checked,CreatedUTC")] ListOfListsViewModel listOfListsViewModel)
        {
            if (ModelState.IsValid)
            {
                db.ListOfListsViewModels.Add(listOfListsViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(listOfListsViewModel);
        }

        // GET: ListOfListsViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListOfListsViewModel listOfListsViewModel = db.ListOfListsViewModels.Find(id);
            if (listOfListsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(listOfListsViewModel);
        }

        // POST: ListOfListsViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ListId,ListName,ListContent,Checked,CreatedUTC")] ListOfListsViewModel listOfListsViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listOfListsViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(listOfListsViewModel);
        }

        // GET: ListOfListsViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListOfListsViewModel listOfListsViewModel = db.ListOfListsViewModels.Find(id);
            if (listOfListsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(listOfListsViewModel);
        }

        // POST: ListOfListsViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListOfListsViewModel listOfListsViewModel = db.ListOfListsViewModels.Find(id);
            db.ListOfListsViewModels.Remove(listOfListsViewModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
