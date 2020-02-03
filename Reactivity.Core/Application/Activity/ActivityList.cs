using System.Collections.Generic;
using MediatR;
using Domain;
using System.Threading;
using System.Threading.Tasks;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using AutoMapper;

namespace Application.Activity
{
    public class ActivityList
    {
        public class Query : IRequest<List<ActivityDto>>
        {

        }

        public class Handler : IRequestHandler<Query, List<ActivityDto>>
        {
            private readonly ILogger _logger;
            private readonly IMapper _mapper;
            private readonly DataContext _context;
            public Handler(DataContext context,
                            ILogger<ActivityList> logger,
                            IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<ActivityDto>> Handle(
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
                var activities = await _context.Activities.Include(u => u.UserActivities)
                    .ThenInclude(ua => ua.AppUser)
                    .ToListAsync(cancellationToken);
                return _mapper.Map<List<Domain.Activity>, List<ActivityDto>>(activities);
            }
        }
    }
}
