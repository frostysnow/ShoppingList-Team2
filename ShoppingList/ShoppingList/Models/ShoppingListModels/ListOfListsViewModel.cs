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

        [Key]
        public int ListId { get; set; }
        
        [Required]
        [DisplayName("List Name")]
        public string ListName { get; set; }

        [Required]
        [DisplayName("Content")]
        public string ListContent { get; set; }

        public bool Checked { get; set; }

        public DateTimeOffset CreatedUTC { get; set; }

    }
}