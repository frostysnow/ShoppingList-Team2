using ShoppingList.Models.ShoppingListModels;
using ShoppingList.Models.ShoppingListModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ShoppingList.Models.ShoppingListModels.IdentityModel;

namespace ShoppingList
{
    public class ShoppingListService
    {
        private readonly Guid _userId;

        public ShoppingListService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<ListOfListsViewModel> GetLists()
        {
            using (var ctx = new ShoppingListDbContext())
            {
                return
                    ctx
                        .Lists
                        .Where(o => o.OwnerId ==_userId)
                        .Select(
                            e =>
                                new ListOfListsViewModel
                                {
                                    ListName = e.ListName,
                                })
                        .ToArray();
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
                        Color = vm.Color,
                        CreatedUTC = DateTimeOffset.UtcNow
                    };

                ctx.Lists.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}