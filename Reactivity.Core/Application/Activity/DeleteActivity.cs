using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activity
{
    public class DeleteActivity
    {
        public class Command : IRequest
        {
            // TODO add properties
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // TODO Add Logic

                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity == null)
                {
                    throw new Exception($"Could not find Activity with Id {request.Id}");
                }

                _context.Remove(activity);

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Unit.Value;
                }
                throw new Exception("Failed to save new Activity");
            }
        }
    }
}
