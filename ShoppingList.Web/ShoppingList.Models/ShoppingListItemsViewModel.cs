using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingList.Models
{
    public class ShoppingListItemsViewModel
    {
        [Required]
        public int ItemId { get; set; }
        public string Content { get; set; }
        [DefaultValue(false)]
        public bool IsChecked { get; set; }
        public int ShoppingListId { get; set; }
    }
}