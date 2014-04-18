namespace Lend.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExpensesAndInstallments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReleasedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Installments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Payment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentDateTime = c.DateTime(nullable: false),
                        PaidDateTime = c.DateTime(nullable: false),
                        Loan_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Loans", t => t.Loan_ID)
                .Index(t => t.Loan_ID);
            
            AddColumn("dbo.Loans", "Expense_ID", c => c.Int());
            CreateIndex("dbo.Loans", "Expense_ID");
            AddForeignKey("dbo.Loans", "Expense_ID", "dbo.Expenses", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Installments", "Loan_ID", "dbo.Loans");
            DropForeignKey("dbo.Loans", "FK_dbo.Loans_dbo.Expenses_ExpenseID");
            DropIndex("dbo.Installments", new[] { "Loan_ID" });
            DropIndex("dbo.Loans", "IX_ExpenseID");
            
            DropTable("dbo.Installments");
            DropColumn("dbo.Loans", "Expense_ID");
            DropTable("dbo.Expenses");
        }
    }
}
