using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingList.Controllers
{
    public class ShoppingListItemController : ShoppingListController
    {
        // GET: ShoppingListItem
        public ActionResult AddItem()
        {
            return View();
        }
    }
}