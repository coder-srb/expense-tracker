using Expenses.Api.Controllers.Models.Base;

namespace Expenses.Api.Controllers.Models
{
    public class Transaction:BaseEntity
    {
        public string Type { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }

        public int? UserId { get; set; }    // Foreign key for User
        public virtual User? User { get; set; }   // Navigation property for User
    }
}
