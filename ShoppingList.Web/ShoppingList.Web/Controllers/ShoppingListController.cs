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

        [HttpGet]
        public ActionResult Index()
        {
            var Lists = _svc.Value.GetLists();
            return View(Lists);
        }

        [HttpGet]
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

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteGet(int id)
        {
            var detail = _svc.Value.GetListById(id);

            return View(detail);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            _svc.Value.DeleteList(id);

            return RedirectToAction("Index");
        }
    }
}