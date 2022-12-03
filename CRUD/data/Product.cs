using System;
using System.Collections.Generic;

namespace CRUD.data
{
    public partial class Product
    {
        public string ProductId { get; set; } = null!;
        public string? Name { get; set; }
        public string CategoriesId { get; set; } = null!;
        public decimal Price { get; set; }
        public string Image { get; set; } = null!;

        public virtual Category Categories { get; set; } = null!;
    }
}
