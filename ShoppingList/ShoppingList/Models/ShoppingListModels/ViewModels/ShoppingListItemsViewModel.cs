using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Models.ShoppingListModels
{
    public class ShoppingListItemsViewModel
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool IsChecked { get; set; }
        public int ShoppingListId { get; set; }
    }
}