using Microsoft.AspNet.Identity;
using ShoppingList.Models.ShoppingListModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingList.Controllers
{
    public class ShoppingListController : Controller
    {
        private readonly Lazy<ShoppingListService> _svc;

        public ShoppingListController()
        {
            _svc =
                new Lazy<ShoppingListService>(
                    () =>
                    {
                        var userId = Guid.Parse(User.Identity.GetUserId());
                        return new ShoppingListService(userId);
                    });
        }

        // GET: ShoppingList
        public ActionResult Index()
        {
            var lists = _svc.Value.GetLists();
            return View(lists);
        }

        public ActionResult Create()
        {
            var vm = new ShoppingListCreateViewModel();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShoppingListCreateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (!_svc.Value.CreateList(vm))
            {
                ModelState.AddModelError("", "Unable to create List");
                return View(vm);
            }

            return RedirectToAction("Index");
        }

    }
}