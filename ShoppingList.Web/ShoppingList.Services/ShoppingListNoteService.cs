using ShoppingList.Data;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ShoppingList.Data.IdentityModel;

namespace ShoppingList.Services
{
    public class ShoppingListNoteService
    {
        public ShoppingListNoteService()
        {
        }

        public IEnumerable<ShoppingListNoteViewModel> GetNotes(int id)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                return
                    ctx
                    .Notes
                    .Where(e => e.ShoppingListItemId == id)
                    .Select(
                        e =>
                            new ShoppingListNoteViewModel
                            {
                                NoteId = e.NoteId,
                                ShoppingListItemId = e.ShoppingListItemId,
                                Body = e.Body
                            })
                        .ToArray();
            }
        }

        public ShoppingListNoteViewModel GetNoteById(int noteId, int itemId)
        {
            ShoppingListNoteEntity entity;
            using (var ctx = new ShoppingListDbContext())
            {
                entity =
                    ctx
                    .Notes
                    .SingleOrDefault(e => e.NoteId == noteId && e.ShoppingListItemId == itemId);
            }
            return
                new ShoppingListNoteViewModel
                {
                    NoteId = entity.NoteId,
                    ShoppingListItemId = entity.ShoppingListItemId,
                    Body = entity.Body,
                };
        }

        public bool CreateNote(ShoppingListNoteCreateViewModel vm, int id)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    new ShoppingListNoteEntity
                    {
                        NoteId = vm.NoteId,
                        ShoppingListItemId = id,
                        Body = vm.Body
                    };

                ctx.Notes.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int noteId, int itemId)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    ctx
                    .Notes
                    .Single(e => e.NoteId == noteId && e.ShoppingListItemId == itemId);
                ctx.Notes.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAllNotes()
        {
            using (var ctx = new ShoppingListDbContext())
            {
                foreach (ShoppingListNoteEntity note in ctx.Notes)
                {
                    ctx.Notes.Remove(note);
                }
                return ctx.SaveChanges() == 1;
            }
        }
    }
}