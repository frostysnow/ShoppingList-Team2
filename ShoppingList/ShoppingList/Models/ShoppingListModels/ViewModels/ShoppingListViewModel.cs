using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingList.Models.ShoppingListModels.ViewModels
{
    public class ShoppingListViewModel
    {
        [Key]
        public int ListId { get; set; }
        public string ListName { get; set; }
        public string Color { get; set; }
    }
}