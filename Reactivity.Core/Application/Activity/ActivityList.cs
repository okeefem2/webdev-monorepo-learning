using System.Collections.Generic;
using MediatR;
using Domain;
using System.Threading;
using System.Threading.Tasks;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Application.Activity
{
    public class ActivityList
    {
        public class Query : IRequest<List<Domain.Activity>>
        {

        }

        public class Handler : IRequestHandler<Query, List<Domain.Activity>>
        {
            private readonly ILogger _logger;
            private readonly DataContext _context;
            public Handler(DataContext context, ILogger<ActivityList> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<Domain.Activity>> Handle(
                Query request,
                CancellationToken cancellationToken
            )
            {
                // This block is just to show how to test it
                // try
                // {
                //     for (var i = 0; i < 10; i++)
                //     {
                //         cancellationToken.ThrowIfCancellationRequested();
                //         await Task.Delay(1000, cancellationToken);
                //         _logger.LogInformation($"Task {i} has completed");
                //     }
                // }
                // catch (Exception e) when (e is TaskCanceledException)
                // {
                //     _logger.LogInformation("Task was cancelled");
                // }
                var activites = await _context.Activities.ToListAsync(cancellationToken);
                return activites;
            }
        }
    }
}
