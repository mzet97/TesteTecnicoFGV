using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.Application.UseCases.Users.Commands;

public class DeleteUserCommand : IRequest<BaseResult>
{
    public DeleteUserCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
