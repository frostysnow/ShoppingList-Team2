using Microsoft.AspNet.Identity;
using ShoppingList.Models.ShoppingListModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingList.Controllers
{
    public class ShoppingListItemController : ShoppingListController
    {

        private readonly Lazy<ShoppingListItemService> _svc;

        public ShoppingListItemController()
        {
            _svc =
                new Lazy<ShoppingListItemService>(
                    () =>
                    {
                        var userId = Guid.Parse(User.Identity.GetUserId());
                        return new ShoppingListItemService(userId);
                    });
        }

        [HttpPost]
        public ActionResult CreateItem(ShoppingListItemCreateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (!_svc.Value.CreateItem(vm))
            {
                ModelState.AddModelError("", " Unable to add item.");
                return View(vm);
            }
            return RedirectToAction("Index");
        }
    }
}