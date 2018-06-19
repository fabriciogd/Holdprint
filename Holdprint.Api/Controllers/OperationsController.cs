using Holdprint.Core.Contract;
using Holdprint.Domain.Models;
using Holdprint.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Holdprint.Api.Controllers
{
    /// <summary>
    /// Api that represents Operations
    /// </summary>
    [Produces("application/json")]
    [Route("api/Operations")]
    public class OperationsController : Controller
    {
        private readonly IService<Operation, DTOOperation> _service;
        private readonly IRepositoryQuery<Operation, DTOOperation> _query;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="service">Service</param>
        /// <param name="query">Query</param>
        public OperationsController(IService<Operation, DTOOperation> service, IRepositoryQuery<Operation, DTOOperation> query)
        {
            _service = service;
            _query = query;
        }

        /// <summary>
        /// Get all operations
        /// </summary>
        /// <param name="sort">Sort</param>
        /// <param name="order">Order</param>
        /// <returns>Returns all operations</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] string sort = null, [FromQuery] string order = "asc")
        {
            var result = _query.GetAll(sort, order).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Get an operation
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Returns an operation</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _query.Get(id);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Add and operation
        /// </summary>
        /// <param name="dto">Operation</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Returns added operation</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DTOOperation dto, CancellationToken ct = default(CancellationToken))
        {
            dto = await _service.Add(dto, ct);

            return Ok(dto);
        }

        /// <summary>
        /// Update an operation
        /// </summary>
        /// <param name="dto">Operation</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Returns updated operation</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DTOOperation dto, CancellationToken ct = default(CancellationToken))
        {
            var result = _query.Get(dto.Id);

            if (result == null)
            {
                return NotFound();
            }

            dto = await _service.Update(dto, ct);

            return Ok(dto);
        }

        /// <summary>
        /// Delete an operation
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>No response</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct = default(CancellationToken))
        {
            var result = _query.Get(id);

            if (result == null)
            {
                return NotFound();
            }

            await _service.Delete(id, ct);

            return NoContent();
        }
    }
}