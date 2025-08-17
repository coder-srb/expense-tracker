using Expenses.Api.Data;
using Expenses.Api.Data.Services;
using Expenses.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    [Authorize]
    public class TransactionController(ITransactionsService transactionsService): ControllerBase
    {

        [HttpGet("All")]
        public IActionResult Get()
        {
            var transactions = transactionsService.GetAll();
            return Ok(transactions);
        }

       
        [HttpGet("Details/{id}")]
        public IActionResult GetById(int id)
        {
            var transaction = transactionsService.GetById(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] PostTransactionDto payload)
        {
            var newTransaction = transactionsService.Add(payload);
            return Ok(newTransaction);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] PutTransactionDto payload)
        {
            var updatedTransaction = transactionsService.Update(id, payload);

            if(updatedTransaction == null)
            {
                return NotFound();
            }

            return Ok(updatedTransaction);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            bool success = transactionsService.Delete(id);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
