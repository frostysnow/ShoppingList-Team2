using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingList.Data
{
    public class ShoppingListItemEntity
    {
        [Key]
        public int ItemId { get; set; }
        public int ShoppingListId { get; set; }
        public string Content { get; set; }
        public bool IsChecked { get; set; }
        public enum PriorityLevel
        {
            [Display(Name = "It can wait.")]
            ItCanWait = 0,
            [Display(Name = "Need it soon.")]
            Needitsoon = 1,
            [Display(Name = "Grab it now!")]
            Grabitnow = 2
        }
        public PriorityLevel Priority { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}