using ShoppingList.Data;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ShoppingList.Data.IdentityModel;

namespace ShoppingList.Services
{
    public class ShoppingListItemService
    {
        public ShoppingListItemService()
        { }

        public IEnumerable<ShoppingListItemsViewModel> GetItems(int? id)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                return
                    ctx
                    .Items
                    .Where(e => e.ShoppingListId == id)
                    .Select(
                        e =>
                            new ShoppingListItemsViewModel
                            {
                                ItemId = e.ItemId,
                                ShoppingListId = e.ShoppingListId,
                                Content = e.Content,
                                IsChecked = e.IsChecked,
                            })
                        .ToArray();
            }
        }

        public ShoppingListItemsViewModel GetItemById(int itemId, int listId)
        {
            ShoppingListItemEntity entity;
            using (var ctx = new ShoppingListDbContext())
            {
                entity =
                    ctx
                    .Items
                    .SingleOrDefault(e => e.ItemId == itemId && e.ShoppingListId == listId);
            }
            return
                new ShoppingListItemsViewModel
                {
                    ItemId = entity.ItemId,
                    ShoppingListId = entity.ShoppingListId,
                    Content = entity.Content,
                    IsChecked = entity.IsChecked,
                };
        }

        public bool CreateItem(ShoppingListItemCreateViewModel vm, int id)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    new ShoppingListItemEntity
                    {
                        ShoppingListId = id,
                        Content = vm.Content,
                        Priority = (ShoppingListItemEntity.PriorityLevel)vm.Priority,
                        CreatedUtc = DateTimeOffset.Now,
                    };

                ctx.Items.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteItem(int? itemId, int? listId)
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
                foreach (ShoppingListItemEntity item in ctx.Items)
                {
                    ctx.Items.Remove(item);
                }
                return ctx.SaveChanges() == 1;
            }
        }
    }
}