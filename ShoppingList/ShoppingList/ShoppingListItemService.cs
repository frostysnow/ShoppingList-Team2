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

        public IEnumerable<ShoppingListItem> GetItems(int id)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                return
                    ctx
                    .Items
                    .Where(e => e.ShoppingListId == id)
                    .Select(
                        e =>
                            new ShoppingListItem
                            {
                                ItemId = e.ItemId,
                                ShoppingListId = e.ShoppingListId,
                                Content = e.Content,
                                IsChecked = e.IsChecked,
                                Priority = e.Priority
                            })
                        .ToArray();
            }
        }

        public ShoppingListItem GetItemById(int itemId, int listId)
        {
            ShoppingListItem entity;
            using (var ctx = new ShoppingListDbContext())
            {
                entity =
                    ctx
                    .Items
                    .SingleOrDefault(e => e.ItemId == itemId && e.ShoppingListId == listId);
            }
            return
                new ShoppingListItem
                {
                    ItemId = entity.ItemId,
                    ShoppingListId = entity.ShoppingListId,
                    Content = entity.Content,
                    IsChecked = entity.IsChecked,
                    Priority = entity.Priority
                };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool CreateItem(ShoppingListItemCreateViewModel vm, int id)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    new ShoppingListItem
                    {
                        ShoppingListId = id,
                        Content = vm.Content,
                        Priority = (ShoppingListItem.PriorityLevel)vm.Priority,
                    };

                ctx.Items.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteItem(int itemId, int listId)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    ctx
                    .Items
                    .Single(e => e.ItemId == itemId && e.ShoppingListId == listId);
                ctx.Items.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAllItems()
        {
            using (var ctx = new ShoppingListDbContext())
            {
                foreach (ShoppingListItem item in ctx.Items)
                {
                    ctx.Items.Remove(item);
                }
                return ctx.SaveChanges() == 1;
            }
        }
    }
}