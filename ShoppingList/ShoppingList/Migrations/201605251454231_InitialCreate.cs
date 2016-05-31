namespace ShoppingList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingListEntity",
                c => new
                    {
                        ListId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                        Color = c.String(),
                        ListName = c.String(nullable: false),
                        CreatedUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.ListId);
            
            CreateTable(
                "dbo.ShoppingListItem",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ShoppingListId = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        IsChecked = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                        ShoppingListNotes_NoteId = c.Int(),
                        ShoppingListEntity_ListId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.ShoppingListNote", t => t.ShoppingListNotes_NoteId)
                .ForeignKey("dbo.ShoppingListEntity", t => t.ShoppingListEntity_ListId)
                .Index(t => t.ShoppingListNotes_NoteId)
                .Index(t => t.ShoppingListEntity_ListId);
            
            CreateTable(
                "dbo.ShoppingListNote",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        ShoppingListItemId = c.Int(nullable: false),
                        Body = c.String(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.NoteId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ShoppingListUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ShoppingListUser", t => t.ShoppingListUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ShoppingListUser_Id);
            
            CreateTable(
                "dbo.ShoppingListUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ShoppingListUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoppingListUser", t => t.ShoppingListUser_Id)
                .Index(t => t.ShoppingListUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ShoppingListUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ShoppingListUser", t => t.ShoppingListUser_Id)
                .Index(t => t.ShoppingListUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ShoppingListUser_Id", "dbo.ShoppingListUser");
            DropForeignKey("dbo.IdentityUserLogin", "ShoppingListUser_Id", "dbo.ShoppingListUser");
            DropForeignKey("dbo.IdentityUserClaim", "ShoppingListUser_Id", "dbo.ShoppingListUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.ShoppingListItem", "ShoppingListEntity_ListId", "dbo.ShoppingListEntity");
            DropForeignKey("dbo.ShoppingListItem", "ShoppingListNotes_NoteId", "dbo.ShoppingListNote");
            DropIndex("dbo.IdentityUserLogin", new[] { "ShoppingListUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ShoppingListUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ShoppingListUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.ShoppingListItem", new[] { "ShoppingListEntity_ListId" });
            DropIndex("dbo.ShoppingListItem", new[] { "ShoppingListNotes_NoteId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ShoppingListUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.ShoppingListNote");
            DropTable("dbo.ShoppingListItem");
            DropTable("dbo.ShoppingListEntity");
        }
    }
}
