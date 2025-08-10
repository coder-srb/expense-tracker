namespace Expenses.Api.Controllers.Models.Base
{
    public class BaseEntity // Base class for entities(User & Transaction) in the application
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
