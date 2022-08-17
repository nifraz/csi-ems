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
        private readonly LinkGenerator linkGenerator;

        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
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
        public async Task<ActionResult<EmployeeModel>> Get(string id)
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
            var existing = await unitOfWork.Employees.GetAsync(model.Id);
            if (existing != null)
            {
                return BadRequest("Employee already exists");
            }

            var path = linkGenerator.GetPathByAction(nameof(Get), "Employees", new { model.Id });
            if (string.IsNullOrWhiteSpace(path))
            {
                return BadRequest("Could not use current Id");
            }

            try
            {
                var entity = mapper.Map<Employee>(model);
                unitOfWork.Employees.Add(entity);
                if (await unitOfWork.CompleteAsync() > 0)
                {
                    return Created(path, mapper.Map<EmployeeModel>(entity));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeModel>> Put(string id, EmployeeModel model)
        {
            try
            {
                var entity = await unitOfWork.Employees.GetAsync(id);
                if (entity == null)
                {
                    return NotFound($"No Employee exists for id {id}");
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
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var entity = await unitOfWork.Employees.GetAsync(id);
                if (entity == null)
                {
                    return NotFound($"No Employee exists for id {id}");
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
