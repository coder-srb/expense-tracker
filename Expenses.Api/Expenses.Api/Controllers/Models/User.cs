﻿using Expenses.Api.Controllers.Models.Base;

namespace Expenses.Api.Controllers.Models
{
    public class User:BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Transaction> Transactions { get; set; }   // Navigation property for Transactions
    }
}
