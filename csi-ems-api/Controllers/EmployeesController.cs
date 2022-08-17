using AutoMapper;
using Csi.Ems.Api.Core;
using Csi.Ems.Api.Core.Domain;
using Csi.Ems.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace Csi.Ems.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeModel[]>> GetAll()
        {
            try
            {
                var results = await unitOfWork.Employees.GetAllAsync();
                return mapper.Map<EmployeeModel[]>(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Failure");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeModel>> Get(Guid id)
        {
            try
            {
                var result = await unitOfWork.Employees.GetAsync(id);
                if (result == null)
                {
                    return NotFound("Employee not found");
                }
                return mapper.Map<EmployeeModel>(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> Post(EmployeeModel model)
        {
            var emailExisting = await unitOfWork.Employees.FindAsync(e => e.Email == model.Email);
            if (emailExisting != null)
            {
                return BadRequest("Employee email already exists");
            }

            try
            {
                var entity = mapper.Map<Employee>(model);
                //entity.Id = Guid.NewGuid();
                unitOfWork.Employees.Add(entity);
                if (await unitOfWork.CompleteAsync() > 0)
                {
                    return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeModel>> Put(Guid id, EmployeeModel model)
        {
            try
            {
                var entity = await unitOfWork.Employees.GetAsync(id);
                if (entity == null)
                {
                    return NotFound($"Employee id {id} not found");
                }

                mapper.Map(model, entity);
                if (await unitOfWork.CompleteAsync() > 0)
                {
                    return mapper.Map<EmployeeModel>(entity);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var entity = await unitOfWork.Employees.GetAsync(id);
                if (entity == null)
                {
                    return NotFound($"Employee id {id} not found");
                }

                unitOfWork.Employees.Remove(entity);
                if (await unitOfWork.CompleteAsync() > 0)
                {
                    return Ok("Employee record has been deleted");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Failure");
            }

            return BadRequest();
        }
    }
}
