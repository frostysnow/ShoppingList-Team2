using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingList.Models
{
    public class ShoppingListItemPageViewModel
    {
        public IEnumerable<ShoppingListItemsViewModel> ListItems { get; set; }
        public ShoppingListItemCriteria Criteria { get; set; }
    }
}