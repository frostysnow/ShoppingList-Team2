using Microsoft.AspNet.Identity;
using ShoppingList.Models;
using ShoppingList.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingList.Web.Controllers
{
    [Authorize]
    public class ShoppingListNoteController : ShoppingListItemController
    {
        private readonly Lazy<ShoppingListNoteService> _svc;

        public ShoppingListNoteController()
        {
            _svc =
                new Lazy<ShoppingListNoteService>(
                    () =>
                    {
                        var userId = Guid.Parse(User.Identity.GetUserId());
                        return new ShoppingListNoteService();
                    });
        }

        public ActionResult Index(int id)
        {
            var Items = _svc.Value.GetNotes(id);
            return View(Items);
        }

        [HttpGet]
        public ActionResult CreateNote()
        {
            try
            {
                var vm = new ShoppingListNoteCreateViewModel();

                return View(vm);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Argument Exception. Redirecting to Shopping List.");
                return RedirectToAction("Index", "ShoppingList", null);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNote(ShoppingListNoteCreateViewModel vm, int id)
        {
            if (!ModelState.IsValid) return View(vm);

            if (!_svc.Value.CreateNote(vm, id))
            {
                ModelState.AddModelError("", " Unable to add note.");
                return View(vm);
            }
            return RedirectToAction("Index", new { id = Url.RequestContext.RouteData.Values["id"] });
        }

        public ActionResult DeleteAllNotes()
        {
            _svc.Value.DeleteAllNotes();
            return RedirectToAction("Index", "ShoppingList");
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteNoteGet(int id, int ShoppingListItemId)
        {
            try
            {
                var detail = _svc.Value.GetNoteById(id, ShoppingListItemId);

                return View(detail);
            }
            catch (ArgumentException e)
            {
                return RedirectToAction("Index", "ShoppingList", null);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNotePost(int id, int ShoppingListItemId)
        {
            _svc.Value.DeleteNote(id, ShoppingListItemId);
            return RedirectToAction("Index", new { id = Url.RequestContext.RouteData.Values["id"] });
        }
    }
}