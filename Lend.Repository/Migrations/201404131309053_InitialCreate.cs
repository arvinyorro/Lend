namespace Lend.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Borrowers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        RegisteredDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AmountBorrowed = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Interest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Paid = c.Boolean(nullable: false),
                        BorrowedDateTime = c.DateTime(nullable: false),
                        BorrowerID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Borrowers", t => t.BorrowerID)
                .Index(t => t.BorrowerID);

            CreateIndex("dbo.Borrowers", new string[] { "FirstName", "LastName" }, true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "Borrower_ID", "dbo.Borrowers");
            DropIndex("dbo.Loans", new[] { "Borrower_ID" });
            DropTable("dbo.Loans");

            DropIndex("dbo.Borrowers", new string[] { "FirstName", "LastName" });
            DropTable("dbo.Borrowers");
        }
    }
}
