using Expenses.Api.Dtos;
using Microsoft.OpenApi.Any;
using System.Transactions;
using Transaction = Expenses.Api.Models.Transaction;

namespace Expenses.Api.Data.Services
{
    public interface ITransactionsService
    {
        List<Transaction> GetAll(int userId);
        Transaction? GetById(int id);
        Transaction Add(PostTransactionDto transaction, int userId);
        Transaction? Update(int id, PutTransactionDto transaction);
        bool Delete(int id);
    }

    public class TransactionsService(AppDbContext context) : ITransactionsService
    {
        public Transaction Add(PostTransactionDto transaction, int userId)
        {
            var newTransaction = new Transaction
            {
                Type = transaction.Type,
                Amount = transaction.Amount,
                Category = transaction.Category,    
                CreatedAt = transaction.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
                UserId = userId
            };

            context.Transactions.Add(newTransaction);
            context.SaveChanges();

            return newTransaction;
        }

        public bool Delete(int id)
        {
            var transaction = context.Transactions.FirstOrDefault(t => t.Id == id);

            if (transaction == null)
            {
                return false;
            }

            context.Transactions.Remove(transaction);
            context.SaveChanges();

            return true;
        }

        public List<Transaction> GetAll(int userId)
        {
            var transactions = context.Transactions.Where(t => t.UserId == userId).ToList();
            return transactions;
        }

        public Transaction? GetById(int id)
        {
            var transaction = context.Transactions.FirstOrDefault(t => t.Id == id);
            return transaction;
        }

        public Transaction? Update(int id, PutTransactionDto transaction)
        {
            var updatedTransaction = context.Transactions.Find(id);
            if (updatedTransaction != null)
            { 
                updatedTransaction.Type = transaction.Type;
                updatedTransaction.Amount = transaction.Amount;
                updatedTransaction.Category = transaction.Category;
                updatedTransaction.UpdatedAt = DateTime.UtcNow;

                context.Transactions.Update(updatedTransaction);
                context.SaveChanges();
            }

            return updatedTransaction;
        }
    }
}
