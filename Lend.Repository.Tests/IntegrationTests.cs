using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.Entity.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lend.Domain;
using Lend.Repository;

namespace Lend.Repository.Tests
{
    /// <summary>
    /// This class is used for integration testing of the data context
    /// with an actual database. 
    /// </summary>
    /// <remarks>
    /// The purpose of the tests is to validate for mapping correctness. For this
    /// we need an actual database.
    /// <para>
    /// No changes will reflect in the database, inserted and updated data will not take
    /// effect. This is due to the <see cref="TransactionScope"/> impelemented in this class.
    /// Even though a transaction component is used, please avoid testing in a
    /// live database, you should know this already as this is only a reminder.
    /// </para>
    /// </remarks>
    [TestClass]
    public class IntegrationTests
    {
        private const decimal InterestRate = (decimal)0.1;
        private const decimal LoanAmount = 1000;
        private const decimal Interest = LoanAmount * InterestRate;
        private const int InstallmentCount = 2;
        private const decimal TotalPayment = LoanAmount + Interest;
        private const decimal InstallmentPayment = TotalPayment / InstallmentCount;

        private LendContext dbContext;
        private TransactionScope transactionScope;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dbContext = new LendContext();
            this.transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.dbContext.Dispose();
            this.transactionScope.Dispose();
        }

        [TestMethod]
        public void ShouldListBorrowers()
        {
            IEnumerable<Borrower> borrowers = this.dbContext.Borrowers.ToList();
        }

        [TestMethod]
        public void ShouldAddBorrower()
        {
            string firstName = "Test";
            string lastName = "Test";
            DateTime registeredDateTime = DateTime.Now;
            var testBorrower = new Borrower(firstName, lastName);
            this.dbContext.Borrowers.Add(testBorrower);
            this.dbContext.SaveChanges();

            var updatedBorrower = this.dbContext.Borrowers.AsEnumerable().Last();

            Assert.AreEqual(testBorrower, updatedBorrower);
            Assert.AreEqual(firstName, updatedBorrower.LastName);
            Assert.AreEqual(lastName, updatedBorrower.LastName);
            Assert.AreEqual(registeredDateTime, updatedBorrower.RegisteredDateTime);
        }

        [TestMethod]
        public void ShouldAddLoans()
        {
            // Prepare

            // Create and persist existing borrower.
            var testBorrower = new Borrower("Test", "Test");
            this.dbContext.Borrowers.Add(testBorrower);
            this.dbContext.SaveChanges();
            
            // Create test loan.
            var testLoan = new Loan(LoanAmount, InstallmentCount);
            testBorrower.Loans.Add(testLoan);

            // Act
            this.dbContext.Entry(testBorrower).State = System.Data.Entity.EntityState.Modified;
            this.dbContext.SaveChanges();

            // Verify
            Borrower updatedBorrower = this.dbContext.Borrowers.AsEnumerable().Last();
            Loan updatedLoan = updatedBorrower.Loans.First();
            IEnumerable<Installment> installments = updatedBorrower.Loans.First().Installments;

            Assert.AreEqual(testLoan, updatedLoan);
            Assert.AreEqual(InstallmentCount, updatedLoan.Installments.Count);
            Assert.AreEqual(LoanAmount, updatedLoan.AmountBorrowed);
            Assert.AreEqual(TotalPayment, updatedLoan.AmountPayment);
            Assert.AreEqual(testBorrower, updatedLoan.Borrower);
            Assert.AreEqual(InstallmentPayment, updatedLoan.Installments.First().Payment);
        }

        [TestMethod]
        public void ShouldAddExpense()
        {
            // Prepare
            var testExpense = new Expense(1000);

            // Act
            this.dbContext.Expenses.Add(testExpense);
            this.dbContext.SaveChanges();

            // Verify

            Expense updatedExpense = this.dbContext.Expenses.AsEnumerable().Last();
            Assert.AreEqual(testExpense, updatedExpense);
        }

        [TestMethod]
        public void ShouldAddLoanWithExpense()
        {
            // Prepare

            // Add Existing Borrower and Expense

            var testExpense = new Expense(1000);
            this.dbContext.Expenses.Add(testExpense);

            var testBorrower = new Borrower("Test", "Test");
            this.dbContext.Borrowers.Add(testBorrower);
            this.dbContext.SaveChanges();

            var testLoan = new Loan(LoanAmount, InstallmentCount, testExpense);
            testBorrower.Loans.Add(testLoan);

            // Act 
            this.dbContext.Entry(testBorrower).State = System.Data.Entity.EntityState.Modified;
            this.dbContext.SaveChanges();

            // Verify
            Borrower updatedBorrower = this.dbContext.Borrowers.AsEnumerable().Last();
            Loan updatedLoan = updatedBorrower.Loans.First();

            Assert.AreEqual(testLoan, updatedLoan);
            Assert.AreEqual(testBorrower, updatedLoan.Borrower);
            Assert.AreEqual(testExpense, updatedLoan.Expense);
        }
    }
}
