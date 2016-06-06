using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingList.Data
{
    public class ShoppingListNoteEntity
    {
        [Key]
        public int NoteId { get; set; }
        [Required]
        public int ShoppingListItemId { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
