using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingList.Models.ShoppingListModels
{
    public class ListOfListsViewModel
    {
        
        [Required]
        [DisplayName("List Name")]
        public string ListName { get; set; }

    }
}