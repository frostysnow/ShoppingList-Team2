using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingList.Models.ShoppingListModels
{
    public class StringValueAttribute : System.Attribute
    {

        private string _value;

        public StringValueAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

    }

    public enum Priority
    {
        [StringValue("It can wait.")]
        ItCanWait = 1, 
        [StringValue("Need it soon.")]
        Needitsoon = 2, 
        [StringValue("Grab it now!")]
        Grabitnow = 3
    }

    public class ShoppingListItem
    {
        [Key]
        [Required]
        public int ItemId                       { get; set; }
        [Required]
        public int ShoppingListId               { get; set; }
        [Required]
        public string Content                   { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool IsChecked                   { get; set; }
        [Required]
        public Priority Priority                { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc        { get; set; }
        public DateTimeOffset? ModifiedUtc      { get; set; }
        public virtual ShoppingListNote ShoppingListNotes { get; set; }
    }
}