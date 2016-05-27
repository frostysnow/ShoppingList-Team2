using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingList.Models.ShoppingListModels
{
    public class ShoppingListItemCreateViewModel
    {
        public int ItemId { get; set; }
        public string Content { get; set; }
        public Priority Priority { get; set; }
        public ShoppingListEntity ShoppingList { get; set; }
    }
}