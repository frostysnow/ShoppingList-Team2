using Microsoft.AspNet.Identity;
using ShoppingList.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingList.Web.Controllers
{
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
    }
}