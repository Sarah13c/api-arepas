using Arepas.Api.Dtos;
using Arepas.Application.Interfaces;
using Arepas.Application.Services;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;

        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParams queryParams)
        {
            if (queryParams.Page == 0 && queryParams.Limit == 0)
            {
                return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(await _productService.GetAllAsync()));
            }

            var responseData = await _productService.GetByQueryParamsAsync(queryParams);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(responseData.XPagination));

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(responseData.Items));
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<ProductDto>(await _productService.GetByIdAsync(id)));
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            return Ok(_mapper.Map<ProductDto>(await _productService.AddAsync(_mapper.Map<Product>(productDto))));
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto productDto)
        {
            return Ok(_mapper.Map<ProductDto>(await _productService.UpdateAsync(id, _mapper.Map<Product>(productDto))));
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.RemoveAsync(id);
            return Ok();
        }
    }
}