using ShoppingList.Models.ShoppingListModels;
using ShoppingList.Models.ShoppingListModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ShoppingList.Models.ShoppingListModels.IdentityModel;

namespace ShoppingList
{
    public class ShoppingListService
    {
        private ShoppingListItemService svc = new ShoppingListItemService();

        private readonly Guid _userId;

        public ShoppingListService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<ShoppingListEntity> GetLists()
        {
            using (var ctx = new ShoppingListDbContext())
            {
                return
                    ctx
                        .Lists
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new ShoppingListEntity
                                {
                                    ListId = e.ListId,
                                    ListName = e.ListName,
                                    Color = e.Color
                                })
                        .ToArray();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool CreateList(ShoppingListCreateViewModel vm)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    new ShoppingListEntity
                    {
                        OwnerId = _userId,
                        ListName = vm.ListName,
                        Color = vm.Color,
                        CreatedUTC = DateTimeOffset.UtcNow
                    };

                ctx.Lists.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public ShoppingListViewModel GetListById(int id)
        {
            ShoppingListEntity entity;
            using (var ctx = new ShoppingListDbContext())
            {
                entity =
                    ctx
                        .Lists
                        .SingleOrDefault(e => e.OwnerId == _userId && e.ListId == id);
            }


            return
                new ShoppingListViewModel
                {
                    ListId = entity.ListId,
                    ListName = entity.ListName
                };
        }

        public bool DeleteList(int id)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    ctx
                        .Lists
                        .SingleOrDefault(e => e.OwnerId == _userId && e.ListId == id);

                foreach(ShoppingListItemEntity item in ctx.Items)
                {
                    if (item.ShoppingListId == entity.ListId)
                        ctx.Items.Remove(item);
                }

                ctx.Lists.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}