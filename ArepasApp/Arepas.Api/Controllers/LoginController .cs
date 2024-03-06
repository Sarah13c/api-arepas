using Arepas.Api.Dtos;
using Arepas.Application.Interfaces;
using Arepas.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Arepas.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ICustomerService _customerService;

        public LoginController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
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