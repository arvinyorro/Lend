namespace Lend.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInstallment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Installments", "DueDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Installments", "PaidDateTime", c => c.DateTime());
            DropColumn("dbo.Installments", "PaymentDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Installments", "PaymentDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Installments", "PaidDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Installments", "DueDateTime");
        }
    }
}
