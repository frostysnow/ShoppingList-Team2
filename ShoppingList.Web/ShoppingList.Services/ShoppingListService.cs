using ShoppingList.Data;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ShoppingList.Data.IdentityModel;

namespace ShoppingList.Services
{
    public class ShoppingListService
    {
        private ShoppingListItemService svc = new ShoppingListItemService();

        private readonly Guid _userId;

        public ShoppingListService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<ShoppingListViewModel> GetLists(ShoppingListCriteria criteria)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var lists = ctx
                        .Lists
                        .Where(e => e.OwnerId == _userId);

                //switch (criteria.SortOption)
                //{
                //    case ShoppingListSortOption.NameAsc:
                //        lists = lists.OrderBy(l => l.ListName);
                //        break;
                //    case ShoppingListSortOption.NameDesc:
                //        lists = lists.OrderByDescending(l => l.ListName);
                //        break;
                //    default:
                //        lists = lists.OrderBy(l => l.ListName);
                //        break;
                //}

                return lists.Select(
                     e =>
                         new ShoppingListViewModel
                         {
                             ListId = e.ListId,
                             ListName = e.ListName,
                             Color = e.Color,
                         }).ToList();

            }
        }

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
                foreach (ShoppingListItemEntity item in ctx.Items)
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