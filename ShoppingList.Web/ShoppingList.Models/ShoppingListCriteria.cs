using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingList.Models
{
    public class ShoppingListCriteria
    {
        public ShoppingListSortOption SortOption { get; set; }
    }

    public enum ShoppingListSortOption
    {
        NameAsc = 0,
        NameDesc = 1
    }
}