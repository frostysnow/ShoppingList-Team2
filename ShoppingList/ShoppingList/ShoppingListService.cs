using ShoppingList.Models.ShoppingListModels;
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

        public IEnumerable<ListOfListsViewModel> GetNotes()
        {
            using (var ctx = new ShoppingListDbContext())
            {
                return
                    ctx
                        .Lists
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new ListOfListsViewModel
                                {
                                    ListName = e.ListName,
                                })
                        .ToArray();
            }
        }
    }
}