using System;
using System.Collections.Generic;

namespace CRUD.data
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public string CategoriesId { get; set; } = null!;
        public string? CategoriesName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
