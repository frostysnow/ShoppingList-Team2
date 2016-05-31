using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingList.Data
{
    public class ShoppingListEntity
    {
        [Key]
        [Required]
        public int ListId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        [DisplayName("List Name")]
        public string ListName { get; set; }
        public DateTimeOffset CreatedUTC { get; set; }
        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}