using Arepas.Api.Dtos;
using Arepas.Application.Interfaces;
using Arepas.Domain.Dtos;
using Arepas.Domain.Exceptions;
using Arepas.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Arepas.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(IMapper mapper, ICustomerService customerService)
        {
            _mapper = mapper;
            _customerService = customerService;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParams queryParams)
        {
            if (queryParams.Page == 0 && queryParams.Limit == 0)
            {
                return Ok(_mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(await _customerService.GetAllAsync()));
            }

            var responseData = await _customerService.GetByQueryParamsAsync(queryParams);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(responseData.XPagination));

            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(responseData.Items));
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = _mapper.Map<CustomerDto>(await _customerService.GetByIdAsync(id));
            return Ok(customer);
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto customerDto)
        {
            return Ok(_mapper.Map<CustomerDto>(await _customerService.AddAsync(_mapper.Map<Customer>(customerDto))));
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CustomerDto customerDto)
        {
            return Ok(_mapper.Map<CustomerDto>(await _customerService.UpdateAsync(id, _mapper.Map<Customer>(customerDto))));
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.RemoveAsync(id);
            return Ok();
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetOrdersByCustomerId(int id)
        {
            var customerEmbedded = new CustomerOrder()
            {
                Customer = await _customerService.GetByIdAsync(id),
                Orders = await _customerService.GetOrdersByCustomerIdAsync(id)
            };

            return Ok(_mapper.Map<CustomerOrder, CustomerOrderDto>(customerEmbedded));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn([FromBody] LoginDto loginRequestInfo)
        {
            var customers = await _customerService.GetAllAsync();

            var result = customers.FirstOrDefault(c => c.UserEmail == loginRequestInfo.Email && c.Password == loginRequestInfo.Password);

            if (result == null)
            {
                throw new InternalServerErrorException("El UserEmail o la Contraseña son Invalidos");
            }

            return Ok();
        }

    }
}