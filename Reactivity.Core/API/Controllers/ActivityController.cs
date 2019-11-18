using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activity;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("dotnet/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ActivityController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> List(CancellationToken ct)
        {
            return await _mediator.Send(new ActivityList.Query(), ct);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetById(Guid id, CancellationToken ct)
        {
            return await _mediator.Send(new ActivityDetail.Query { Id = id }, ct);
        }

        [HttpPost()] 
        public async Task<ActionResult<Unit>> Create(CreateActivity.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(EditActivity.Command command, Guid id)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await _mediator.Send(new DeleteActivity.Command { Id = id });
        }
    }
}
