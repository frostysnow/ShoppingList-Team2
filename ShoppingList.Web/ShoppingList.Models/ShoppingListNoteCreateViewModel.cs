using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingList.Models
{
    public class ShoppingListNoteCreateViewModel
    {
        [Key]
        public int NoteId { get; set; }
        public int ShoppingListItemId { get; set; }
        public string Body { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}