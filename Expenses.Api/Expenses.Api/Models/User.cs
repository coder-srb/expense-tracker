using Expenses.Api.Models.Base;

namespace Expenses.Api.Models
{
    public class User:BaseEntity
    {
        public List<Transaction> Transactions { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
