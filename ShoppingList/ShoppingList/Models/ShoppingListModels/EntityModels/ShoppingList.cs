using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingList.Models.ShoppingListModels
{
    public class ShoppingList
    {

        [Key]
        [Required]
        public int ListId                       { get; set; }
        [Required]
        public int UserId                       { get; set; }
        public string Color                     { get; set; }
        [Required]
        [DisplayName("List Name")]
        public string ListName                  { get; set; }
        public DateTimeOffset CreatedUTC        { get; set; }
        public DateTimeOffset? ModifiedUTC      { get; set; }

    }
}