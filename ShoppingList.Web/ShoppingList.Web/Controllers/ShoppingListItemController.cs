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
    public class ShoppingListItemController : ShoppingListController
    {
        private readonly Lazy<ShoppingListItemService> _svc;

        private readonly Lazy<ShoppingListService> _svc2;

        public ShoppingListItemController()
        {
            _svc =
                new Lazy<ShoppingListItemService>(
                    () =>
                    {
                        var userId = Guid.Parse(User.Identity.GetUserId());
                        return new ShoppingListItemService();
                    });
        }

        [HttpGet]
        public ActionResult ItemIndex(int? id)
        {
            var Items = _svc.Value.GetItems(id);
            return View(Items);
        }

        [HttpGet]
        public ActionResult NoteToItemIndex(int id, int ShoppingListItemId)
        {
            var Items = _svc2.Value.GetLists(id);
            return View(Items);
        }

        [HttpGet]
        public ActionResult CreateItem()
        {
            try
            {
                var vm = new ShoppingListItemCreateViewModel();

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
        public ActionResult CreateItem(ShoppingListItemCreateViewModel vm, int id)
        {
            if (!ModelState.IsValid) return View(vm);

            if (!_svc.Value.CreateItem(vm, id))
            {
                ModelState.AddModelError("", " Unable to add item.");
                return View(vm);
            }
            return RedirectToAction("ItemIndex", new { id = Url.RequestContext.RouteData.Values["id"] });
        }

        public ActionResult DeleteAllItems()
        {
            _svc.Value.DeleteAllItems();
            return RedirectToAction("Index", "ShoppingList");
        }

        [HttpGet]
        [ActionName("DeleteItem")]
        public ActionResult DeleteGet(int id, int ShoppingListId)
        {
            try
            {
                var detail = _svc.Value.GetItemById(id, ShoppingListId);

                return View(detail);
            }
            catch (ArgumentException e)
            {
                return RedirectToAction("ItemIndex", "ShoppingList", null);
            }
        }

        [HttpPost]
        [ActionName("DeleteItem")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id, int ShoppingListId)
        {
            _svc.Value.DeleteItem(id, ShoppingListId);
            return RedirectToAction("ItemIndex", new { id = Url.RequestContext.RouteData.Values["id"] });
        }
    }
}