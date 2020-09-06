using Microsoft.AspNetCore.Mvc;
using EntityFrameworkInMemoryAPI.Data.Interfaces;
using System.Threading.Tasks;
using EntityFrameworkInMemoryAPI.Data.Entities;
using System;
using EntityFrameworkInMemoryAPI.ViewModel;
using EntityFrameworkInMemoryAPI.Data.Validate;

namespace EntityFrameworkInMemoryAPI.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ResultViewModel errorResultViewModel = new ResultViewModel("Ocorreu algum erro interno na aplicação, por favor tente novamente", false, null);

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        #region POST

        [HttpPost]
        [Route("api/v1/customer")]
        public async Task<IActionResult> PostV1([FromBody] Customer customer)
        {
            try
            {
                //gera um id que não existe no db
                int id = new Random().Next(0, 99999);
                var idExists = await _customerRepository.Get(id);

                do
                {
                    id = new Random().Next(0, 99999);
                } while (await _customerRepository.Get(id) != null);


                //valida a entidade
                var validator = new CustomerValidator();
                validator.Validate(customer);

                if (validator.Errors.Count > 0)
                    return BadRequest(new ResultViewModel
                    {
                        Message = "Dados inválidos! Corriga os erros e tente novamente.",
                        Success = false,
                        Data = validator.Errors
                    });


                //insere o usuário no db local
                customer.Id = id;
                var customerInserted = await _customerRepository.Insert(customer);

                return Ok(customerInserted);
            }
            catch (Exception)
            {
                return StatusCode(500, errorResultViewModel);
            }
        }

        [HttpPost]
        [Route("api/v2/customer")]
        public async Task<IActionResult> PostV2([FromBody] Customer customer)
        {
            try
            {
                //gera um id que não existe no db
                int id = new Random().Next(0, 99999);
                var idExists = await _customerRepository.Get(id);

                do
                {
                    id = new Random().Next(0, 99999);
                } while (await _customerRepository.Get(id) != null);


                //valida a entidade
                var validator = new CustomerValidator();
                validator.Validate(customer);

                if (validator.Errors.Count > 0)
                    return BadRequest(new ResultViewModel
                    {
                        Message = "Dados inválidos! Corriga os erros e tente novamente.",
                        Success = false,
                        Data = validator.Errors
                    });


                //insere o usuário no db local
                customer.Id = id;
                var customerInserted = await _customerRepository.Insert(customer);

                return Ok(new ResultViewModel
                {
                    Message = "Cliente inserido com sucesso!",
                    Success = true,
                    Data = customerInserted
                });
            }
            catch (Exception)
            {
                return StatusCode(500, errorResultViewModel);
            }
        }

        #endregion

        #region PUT

        [HttpPut]
        [Route("api/v1/customer")]
        public async Task<IActionResult> UpdateV1([FromBody] Customer customer)
        {
            try
            {
                //valida a entidade
                var validator = new CustomerValidator();
                validator.Validate(customer);

                if (validator.Errors.Count > 0)
                    return BadRequest(new ResultViewModel
                    {
                        Message = "Dados inválidos! Corriga os erros e tente novamente.",
                        Success = false,
                        Data = validator.Errors
                    });

                var customerUpdated = await _customerRepository.Update(customer);

                return Ok(customerUpdated);
            }
            catch (Exception)
            {
                return StatusCode(500, errorResultViewModel);
            }
        }

        [HttpPut]
        [Route("api/v2/customer")]
        public async Task<IActionResult> UpdateV2([FromBody] Customer customer)
        {
            try
            {
                //valida a entidade
                var validator = new CustomerValidator();
                validator.Validate(customer);

                if (validator.Errors.Count > 0)
                    return BadRequest(new ResultViewModel
                    {
                        Message = "Dados inválidos! Corriga os erros e tente novamente.",
                        Success = false,
                        Data = validator.Errors
                    });

                var customerUpdated = await _customerRepository.Update(customer);

                return Ok(new ResultViewModel
                {
                    Message = "Cliente atualizado com sucesso!",
                    Success = true,
                    Data = customerUpdated
                });
            }
            catch (Exception)
            {
                return StatusCode(500, errorResultViewModel);
            }
        }

        #endregion

        #region DELETE

        [HttpDelete]
        [Route("api/v1/customer/{id}")]
        public async Task<IActionResult> DeleteV1(int id)
        {
            try
            {
                await _customerRepository.Delete(id);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, errorResultViewModel);
            }
        }

        [HttpDelete]
        [Route("api/v2/customer/{id}")]
        public async Task<IActionResult> DeleteV2(int id)
        {
            try
            {
                await _customerRepository.Delete(id);

                return Ok(new ResultViewModel
                {
                    Message = "Cliente deletado com sucesso!",
                    Success = true,
                    Data = null
                    
                });
            }
            catch (Exception)
            {
                return StatusCode(500, errorResultViewModel);
            }
        }

        #endregion

        #region GET

        [HttpGet]
        [Route("api/v1/customer/{id}")]
        public async Task<IActionResult> GetV1(int id)
        {
            try
            {
                var customer = await _customerRepository.Get(id);

                return Ok(customer);

            }
            catch (Exception)
            {
                return StatusCode(500, errorResultViewModel);
            }
        }

        [HttpGet]
        [Route("api/v2/customer/{id}")]
        public async Task<IActionResult> GetV2(int id)
        {
            try
            {
                var customer = await _customerRepository.Get(id);

                return Ok(new ResultViewModel
                {
                    Message = "Cliente encontrado com sucesso!",
                    Success = true,
                    Data = customer
                });

            }
            catch (Exception)
            {
                return StatusCode(500, errorResultViewModel);
            }
        }

        #endregion
        
        #region GET-ALL
        
        [HttpGet]
        [Route("api/v1/customer")]
        public async Task<IActionResult> GetV1()
        {
            try
            {
                var allCustomers = await _customerRepository.Get();

                return Ok(allCustomers);
            }
            catch (Exception)
            {
                return StatusCode(500, errorResultViewModel);
            }
        }

        [HttpGet]
        [Route("api/v2/customer")]
        public async Task<IActionResult> GetV2()
        {
            try
            {
                var allCustomers = await _customerRepository.Get();

                return Ok(new ResultViewModel
                {
                    Message = "Todos os clientes foram encontrados com sucesso!",
                    Success = true,
                    Data = allCustomers
                });
            }
            catch (Exception)
            {
                return StatusCode(500, errorResultViewModel);
            }
        }

        #endregion
    }
}
