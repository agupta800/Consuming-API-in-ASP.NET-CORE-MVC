using APi.Data;
using APi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CustomerController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            var data = _context.customers.ToList();

            if (data.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet]
        [Route("GetCustomerId/{id}")]

        public IActionResult GetCustomerId(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var customer = _context.customers.Where(x => x.Id == id).SingleOrDefault();

                if (customer == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(customer);
                }
            }
        }

        [HttpPost]
        [Route("AddCustomer")]

        public IActionResult AddCustomer([FromBody] Customer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var cus = _context.customers.Add(model);
                _context.SaveChanges();
                return Ok("Record inserted Sucessfully!");
            }
        }

        [HttpPut]
        [Route("UpdateCustomer")]

        public IActionResult UpdateCustomer([FromBody] Customer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var cus = _context.customers.Where(x => x.Id == model.Id).SingleOrDefault();
                if (cus == null)
                {
                    return BadRequest();
                }
                else
                {
                    cus.Name = model.Name;
                    cus.Gender = model.Gender;
                    cus.IsActive = model.IsActive;
                    _context.customers.Update(cus);
                    _context.SaveChanges();
                    return Ok("Record Updated SucessFully!KD");
                }
            }
        }

        [HttpDelete]
        [Route("DeleteCustomer/{id}")]

        public IActionResult DeleteCustomer(int id)
        {
            if (id != 0)
            {
                var emp = _context.customers.Where(x => x.Id == id).SingleOrDefault();

                if (emp == null)
                {
                    return BadRequest();
                }
                else
                {
                    _context.customers.Remove(emp);
                    _context.SaveChanges();

                }

            }
            else
            {
                return BadRequest();
            }
            return Ok("Record Has Sucessfuly Deleted from DataBase!");



        }
    }
}

