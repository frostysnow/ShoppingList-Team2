using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingList.Models.ShoppingListModels
{
    public class ShoppingListItemCreateViewModel
    {
        public int ItemId { get; set; }
        public int ShoppingListId { get; set; }
        public string Content { get; set; }
        public enum PriorityLevel
        {
            [Display(Name = "It can wait")]
            ItCanWait = 0,
            [Display(Name = "Need it soon")]
            NeedItSoon,
            [Display(Name = "Grab it now")]
            GrabItNow
        }
        public PriorityLevel Priority { get; set; }
    }
}