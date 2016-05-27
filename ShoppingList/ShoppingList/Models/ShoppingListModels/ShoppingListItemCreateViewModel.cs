using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingList.Models.ShoppingListModels
{
    public class ShoppingListItemCreateViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public Priority Priority { get; set; }
    }
}