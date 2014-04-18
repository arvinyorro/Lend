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
                        FirstName = c.String(nullable: false, maxLength: 60),
                        LastName = c.String(nullable: false, maxLength: 60),
                        RegisteredDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => new { t.FirstName, t.LastName }, unique: true, name: "IX_FirstNameLastName");
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AmountBorrowed = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Interest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Paid = c.Boolean(nullable: false),
                        BorrowedDateTime = c.DateTime(nullable: false),
                        Expense_ID = c.Int(),
                        Borrower_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Expenses", t => t.Expense_ID)
                .ForeignKey("dbo.Borrowers", t => t.Borrower_ID, cascadeDelete: true)
                .Index(t => t.Expense_ID)
                .Index(t => t.Borrower_ID);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddedDateTIme = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Installments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Payment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DueDateTime = c.DateTime(nullable: false),
                        PaidDateTime = c.DateTime(),
                        Loan_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Loans", t => t.Loan_ID)
                .Index(t => t.Loan_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "Borrower_ID", "dbo.Borrowers");
            DropForeignKey("dbo.Installments", "Loan_ID", "dbo.Loans");
            DropForeignKey("dbo.Loans", "Expense_ID", "dbo.Expenses");
            DropIndex("dbo.Installments", new[] { "Loan_ID" });
            DropIndex("dbo.Loans", new[] { "Borrower_ID" });
            DropIndex("dbo.Loans", new[] { "Expense_ID" });
            DropIndex("dbo.Borrowers", "IX_FirstNameLastName");
            DropTable("dbo.Installments");
            DropTable("dbo.Expenses");
            DropTable("dbo.Loans");
            DropTable("dbo.Borrowers");
        }
    }
}
