using Expenses.Api.Models.Base;

namespace Expenses.Api.Models
{
    public class Transaction:BaseEntity
    {
        public int? UserId {get; set;}
        public virtual User? User { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
    }
}
