using Arepas.Api.Dtos;
using Arepas.Application.Interfaces;
using Arepas.Domain.Dtos;
using Arepas.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Arepas.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IMapper _mapper;

        public OrderDetailsController(IMapper mapper, IOrderDetailService orderDetailService)
        {
            _mapper = mapper;
            _orderDetailService = orderDetailService;
        }

        // GET: api/<OrderDetailsController>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParams queryParams)
        {
            if (queryParams.Page == 0 && queryParams.Limit == 0)
            {
                return Ok(_mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDto>>(await _orderDetailService.GetAllAsync()));
            }

            var responseData = await _orderDetailService.GetByQueryParamsAsync(queryParams);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(responseData.XPagination));

            return Ok(_mapper.Map<IEnumerable<OrderDetailDto>>(responseData.Items));
        }

        // GET api/<OrderDetailsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<OrderDetailDto>(await _orderDetailService.GetByIdAsync(id)));
        }

        // POST api/<OrderDetailsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDetailDto orderDetailDto)
        {
            return Ok(_mapper.Map<OrderDetailDto>(await _orderDetailService.AddAsync(_mapper.Map<OrderDetail>(orderDetailDto))));
        }

        // PUT api/<OrderDetailsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OrderDetailDto orderDetailDto)
        {
            return Ok(_mapper.Map<OrderDetailDto>(await _orderDetailService.UpdateAsync(id, _mapper.Map<OrderDetail>(orderDetailDto))));
        }

        // DELETE api/<OrderDetailsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderDetailService.RemoveAsync(id);
            return Ok();
        }
    }
}