using Expenses.Api.Data;
using Expenses.Api.Dtos;
using Expenses.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController(AppDbContext context) : ControllerBase
    {
        //private readonly AppDbContext _context;
        //public TransactionController(AppDbContext context)
        //{
        //    _context = context;
        //}

        [HttpGet("All")]
        public IActionResult Get()
        {
            var transactions = context.Transactions.ToList();
            if(transactions == null)
            {
                return NoContent();
            }

            return Ok(transactions);
        }

        //[HttpGet("Details")]    // .../api/Transaction/Details?id=1
        [HttpGet("Details/{id}")]   // .../api/Transaction/Details/1
        public IActionResult GetById(int id)
        {
            var transaction = context.Transactions.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] PostTransactionDto payload)
        {
            var newTransactionDb = new Transaction
            {
                Type = payload.Type,
                Amount = payload.Amount,
                Category = payload.Category,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Transactions.Add(newTransactionDb);
            context.SaveChanges();

            return Ok(newTransactionDb);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] PutTransactionDto payload)
        {
            var updatedTransaction = context.Transactions.Find(id);
            if(updatedTransaction == null)
            {
                return NotFound();
            }

            updatedTransaction.Type = payload.Type;
            updatedTransaction.Amount = payload.Amount;
            updatedTransaction.Category = payload.Category;
            updatedTransaction.UpdatedAt = DateTime.UtcNow;

            context.Transactions.Update(updatedTransaction);
            context.SaveChanges();

            return Ok(updatedTransaction);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var transaction = context.Transactions.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            context.Transactions.Remove(transaction);
            context.SaveChanges();

            return Ok();
        }
    }
}
