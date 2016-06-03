using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingList.Models
{
    public class ShoppingListPageViewModel
    {
        public IEnumerable<ShoppingListViewModel> Lists {get; set;}
        public ShoppingListCriteria Criteria { get; set; }
    }
}