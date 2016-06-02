namespace ShoppingList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDbBackingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingListNoteEntity",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        ShoppingListItemId = c.Int(nullable: false),
                        Body = c.String(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.NoteId);
            
            AddColumn("dbo.ShoppingListItemEntity", "CreatedUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.ShoppingListItemEntity", "ModifiedUtc", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingListItemEntity", "ModifiedUtc");
            DropColumn("dbo.ShoppingListItemEntity", "CreatedUtc");
            DropTable("dbo.ShoppingListNoteEntity");
        }
    }
}
