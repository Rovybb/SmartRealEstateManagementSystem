using Domain.Utils;
using MediatR;

namespace Application.Commands.Property
{
    public class DeletePropertyCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
}
