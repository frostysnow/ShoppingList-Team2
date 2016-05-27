using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Models.ShoppingListModels
{
    public class ShoppingListDetailViewModel
    {
        [Required]
        public string ItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool IsChecked { get; set; }
    }
}