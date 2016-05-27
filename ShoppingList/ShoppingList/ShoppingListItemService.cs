using ShoppingList.Models.ShoppingListModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ShoppingList.Models.ShoppingListModels.IdentityModel;

namespace ShoppingList
{
    public class ShoppingListItemService
    {

        private readonly Guid _userId;

        public ShoppingListItemService(Guid userId)
        {
            _userId = userId;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool CreateItem(ShoppingListItemCreateViewModel vm)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    new ShoppingListItem
                    {
                        Content = vm.Content,
                        Priority = vm.Priority,
                    };

                ctx.Items.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}