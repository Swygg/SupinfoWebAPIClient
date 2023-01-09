using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetWebAPI.Models.DTO;
using ProjetWebAPI.Models.Inputs;
using ProjetWebAPI.Services.Interfaces;

namespace ProjetWebAPI.Controllers
{
    [ApiController]
    [Route("/Customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomersService _customersService;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomersService customersService,
            IMapper mapper,
            ILogger<CustomersController> logger)
        {
            _customersService = customersService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Create")]
        public IActionResult Create(CustomerCreateInput input)
        {
            try
            {
                var customer = _mapper.Map<Customer>(input);
                _customersService.Create(customer);
                _logger.LogInformation("Succès");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Echec");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(CustomerUpdateInput input)
        {
            try
            {
                var customer = _mapper.Map<Customer>(input);
                _customersService.Update(customer);
                _logger.LogInformation("Succès");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Echec");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ReadOne")]
        public IActionResult ReadOne(int id)
        {
            try
            {
                var customer = _customersService.ReadOne(id);
                _logger.LogInformation("Succès");
                return Ok(customer);
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Echec");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ReadAll")]
        public IActionResult ReadAll()
        {
            try
            {
                var customers = _customersService.ReadAll();
                _logger.LogInformation("Succès");
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Echec");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                _customersService.Delete(id);
                _logger.LogInformation("Succès");
                return Ok("Suppression confirmée");
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Succès");
                return BadRequest(ex.Message);
            }
        }
    }
}
