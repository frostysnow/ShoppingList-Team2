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
        public ActionResult ItemIndex(string sortOrder, string currentFilter, int id)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ContentsSortOrder = String.IsNullOrEmpty(sortOrder) ? "ContentsDesc" : "";
            ViewBag.PrioritySortOrder = sortOrder == "Priority" ? "PriorityDesc" : "Priority";
            ViewBag.IsCheckedSortOrder = sortOrder == "IsChecked" ? "IsCheckedDesc" : "IsChecked";
            var ShoppingListItems = _svc.Value.GetItems(id);
            var Items = from items
                        in ShoppingListItems
                        select items;
            switch (sortOrder)
            {
                case "ContentsDesc":
                    ShoppingListItems = ShoppingListItems.OrderByDescending(s => s.Content);
                    break;
                case "Priority":
                    ShoppingListItems = ShoppingListItems.OrderBy(s => s.Priority);
                    break;
                case "PriorityDesc":
                    ShoppingListItems = ShoppingListItems.OrderByDescending(s => s.Priority);
                    break;
                case "IsChecked":
                    ShoppingListItems = ShoppingListItems.OrderBy(s => s.IsChecked);
                    break;
                case "IsCheckedDesc":
                    ShoppingListItems = ShoppingListItems.OrderByDescending(s => s.IsChecked);
                    break;
                default:
                    ShoppingListItems = ShoppingListItems.OrderBy(s => s.Content);
                    break;
            }

            return View("ItemIndex", ShoppingListItems);
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

        [HttpGet]
        public ActionResult EditItem(int id, int ShoppingListId)
        {
            var update = _svc.Value.GetItemById(id, ShoppingListId);
            var list =
                new ShoppingListItemEditViewModel
                {
                    ItemId = update.ItemId,
                    ShoppingListId = update.ShoppingListId,
                    Content = update.Content,
                    Priority = (ShoppingListItemEditViewModel.PriorityLevel)update.Priority
                };
            return View(list);
        }

        [HttpPost]
        public ActionResult EditItem(ShoppingListItemEditViewModel vm, int id)
        {
            if (!ModelState.IsValid) return View(vm);
            if (!_svc.Value.EditItem(vm))
            {
                ModelState.AddModelError("", "Unable to update note.");
                return View(vm);
            }
            return RedirectToAction("ItemIndex", new { id = vm.ShoppingListId });
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
            return RedirectToAction("ItemIndex/" + ShoppingListId);
        }

        public ActionResult DeleteChecked(int id, int[] CheckedIds)
        {
            if (CheckedIds != null && CheckedIds.Length > 0)
                _svc.Value.DeleteCheckedIds(CheckedIds);
            return RedirectToAction("ItemIndex/" + id);
        }
    }
}