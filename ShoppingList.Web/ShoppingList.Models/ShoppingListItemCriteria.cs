using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingList.Models
{
    public class ShoppingListItemCriteria
    {
        public ShoppingListItemSortOption SortOption { get; set; }
    }

    public enum ShoppingListItemSortOption
    {
        ItemAsc = 0,
        ItemDesc = 1
    }
}