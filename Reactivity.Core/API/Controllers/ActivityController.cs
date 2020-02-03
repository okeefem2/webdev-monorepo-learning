using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activity;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Authorize]
    public class ActivityController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<ActivityDto>>> List(CancellationToken ct)
        {
            return await Mediator.Send(new ActivityList.Query(), ct);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityDto>> GetById(Guid id, CancellationToken ct)
        {
            return await Mediator.Send(new ActivityDetail.Query { Id = id }, ct);
        }

        [HttpPost()]
        public async Task<ActionResult<Unit>> Create(CreateActivity.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "IsActivityHost")]
        public async Task<ActionResult<Unit>> Edit(EditActivity.Command command, Guid id)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "IsActivityHost")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteActivity.Command { Id = id });
        }

        [HttpPost("{id}/attend")]
        public async Task<ActionResult<Unit>> Attend(Guid id)
        {
            return await Mediator.Send(new Attend.Command { Id = id });
        }

        [HttpDelete("{id}/attend")]
        public async Task<ActionResult<Unit>> Leave(Guid id)
        {
            return await Mediator.Send(new Leave.Command { Id = id });
        }

    }
}
