using DesafioFGV.Application.UseCases.Users.ViewModels;
using DesafioFGV.Domain.Identities;
using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DesafioFGV.Application.UseCases.Users.Commands;

public class UpdateUserCommand : IRequest<BaseResult<UserViewModel>>
{
    [Required(ErrorMessage = "O campo {0} é requerido")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é requerido")]
    [EmailAddress(ErrorMessage = "O campo {0} é inválido")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é requerido")]
    [StringLength(255, ErrorMessage = "O campo  {0} deve está entre {2} e {1} caracteres", MinimumLength = 3)]
    public required string Name { get; set; }

    public ApplicationUser ToDomain()
    {
        var entity = new ApplicationUser();
        entity.Id = Id;
        entity.UserName = Name;
        entity.Email = Email;
        entity.EmailConfirmed = true;
        return entity;
    }

}
