using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingList.Models
{
    public class ListOfListsViewModel
    {
        [Key]
        [Required]
        public int ListId { get; set; }

        [Required]
        [DisplayName("List Name")]
        public string ListName { get; set; }

        [Required]
        public string Color { get; set; }
    }
}