using AutoMapper;
using ClassLibraryContracts.Models;
using ClassLibraryContracts.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            var config = new MapperConfiguration(
               cfg => {
                   cfg.AddProfile<WebApiMapper>();
               }
           );
            _mapper = config.CreateMapper();
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerAsync([FromRoute] long id)
        {
            var customer = await _customerRepository.GetAsync(id, HttpContext.RequestAborted);
            if(customer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Customer>(customer));
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Int64>> CreateCustomerAsync([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existedCustomer = await _customerRepository.GetAsync(customer.Id, HttpContext.RequestAborted);
            if (existedCustomer != null)
            {
                return Conflict(new { Error = $"Уже есть запись с Id равным {customer.Id}" });
            }
            var result = await _customerRepository.AddAsync(_mapper.Map<CustomerDto>(customer), HttpContext.RequestAborted);

            return Ok(result.Id);
        }
    }
}