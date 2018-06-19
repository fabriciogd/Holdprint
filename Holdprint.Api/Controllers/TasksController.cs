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
    [Produces("application/json")]
    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        private readonly IService<Operation, DTOOperation> _service;
        private readonly IRepositoryQuery<Operation, DTOOperation> _query;

        public TasksController(IService<Operation, DTOOperation> service, IRepositoryQuery<Operation, DTOOperation> query)
        {
            _service = service;
            _query = query;
        }

        [HttpGet()]
        public IEnumerable<DTOOperation> Get([FromQuery] string sort = null, [FromQuery] string order = "asc")
        {
            return _query.GetAll(sort, order).ToList();
        }

        [HttpGet("{id}")]
        public DTOOperation Get(int id)
        {
            return _query.Get(id);
        }

        [HttpPost]
        public async Task<DTOOperation> Post([FromBody] DTOOperation dto, CancellationToken ct = default(CancellationToken))
        {
            dto = await _service.Add(dto, ct);

            return dto;
        }

        [HttpPut]
        public async Task<DTOOperation> Put([FromBody] DTOOperation dto, CancellationToken ct = default(CancellationToken))
        {
            dto = await _service.Update(dto, ct);

            return dto;
        }

        [HttpDelete("{id}")]
        public async void Delete(int id, CancellationToken ct = default(CancellationToken))
        {
            await _service.Delete(id, ct);
        }
    }
}