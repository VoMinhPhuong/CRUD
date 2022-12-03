using CRUD.data;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CRUDContext _context;
        private IWebHostEnvironment _hostingEnvironment;

        public HomeController(ILogger<HomeController> logger, CRUDContext context, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            CustomerModel customerModel = new CustomerModel();
            customerModel.Customers = new List<Models.Customer>();

            var data = _context.Customers.ToList();

            foreach (var customer in data)
            {
                customerModel.Customers.Add(new Models.Customer
                {
                    Id = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Avatar = customer.Avatar
                });
            }
            return View(customerModel);
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            Models.Customer customer = new Models.Customer();

            return View(customer);
        }

        [HttpPost]
        public IActionResult AddCustomer(Models.Customer customer, IFormFile? file)
        {
            try
            {
                string fileName = string.Empty;
                string upDir = Path.Combine(_hostingEnvironment.WebRootPath, "Avatar");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string path = Path.Combine(upDir, fileName);

                if (customer.Avatar != null)
                {
                    var oldImage = Path.Combine(_hostingEnvironment.WebRootPath,
                        customer.Avatar.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                }

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                customer.Avatar = @"\Avatar\" + fileName;

                var newStudent = new data.Customer()
                {
                    CustomerId = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Avatar = customer.Avatar
                };

                _context.Customers.Add(newStudent);
                _context.SaveChanges();
                TempData["AddStatus"] = 1;
            }
            catch (Exception e)
            {
                TempData["AddStatus"] = 0;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateCustomer(string id)
        {
            Models.Customer customer = new Models.Customer();
            var customerById = _context.Customers.Where(x => x.CustomerId.Equals(id)).FirstOrDefault();
            if (customerById != null)
            {
                customer.FirstName = customerById.FirstName;
                customer.LastName = customerById.LastName;
                customer.Avatar = customerById.Avatar;
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult UpdateCustomer(Models.Customer customer, IFormFile? file)
        {
            try
            {
                var customerById = _context.Customers.Where(x => x.CustomerId.Equals(customer.Id)).FirstOrDefault();
                customerById.FirstName = customer.FirstName;
                customerById.LastName = customer.LastName;
                customerById.Avatar = customer.Avatar;

                _context.SaveChanges();
                TempData["UpdateStatus"] = 1;
            }
            catch (Exception e)
            {
                TempData["UpdateStatus"] = 0;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteCustomer(string id)
        {
            try
            {
                var customerById = _context.Customers.Where(x => x.CustomerId.Equals(id)).FirstOrDefault();
                if (customerById != null)
                {
                    _context.Customers.Remove(customerById);
                    _context.SaveChanges();
                }
                TempData["DeleteStatus"] = 1;
            }
            catch
            {
                TempData["DeleteStatus"] = 0;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public string GetValues(string value)
        {
            value += DateTime.Now.ToLongTimeString();
            return value;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}