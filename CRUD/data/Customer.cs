using System;
using System.Collections.Generic;

namespace CRUD.data
{
    public partial class Customer
    {
        public string CustomerId { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
    }
}
