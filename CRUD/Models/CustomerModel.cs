namespace CRUD.Models
{
    public class CustomerModel
    {
        public List<Customer> Customers { get; set; }

   
    }

    public class Customer
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? Avatar { get; set; }

       

    }
}
