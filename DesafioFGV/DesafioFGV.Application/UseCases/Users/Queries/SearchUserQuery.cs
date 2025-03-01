using DesafioFGV.Application.UseCases.Users.ViewModels;
using DesafioFGV.Application.ViewModels;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;

namespace DesafioFGV.Application.UseCases.Users.Queries;

public class SearchUserQuery : 
    BaseSearchNoEntity, 
    IRequest<BaseResultList<UserViewModel>>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = default;
}
