namespace ShoppingList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedModelInformation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShoppingListItemEntity", "Content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShoppingListItemEntity", "Content", c => c.String(nullable: false));
        }
    }
}
