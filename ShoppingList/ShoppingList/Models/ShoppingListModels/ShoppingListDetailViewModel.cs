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
        public int ListId { get; set; }
        [Required]
        public string ListName { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool IsChecked { get; set; }
    }
}