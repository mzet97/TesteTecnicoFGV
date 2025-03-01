using DesafioFGV.Domain.Shared.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DesafioFGV.Application.UseCases.Users.Commands;

public class UpdatePasswordCommand : IRequest<BaseResult>
{
    [Required(ErrorMessage = "O campo {0} é requeridod")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é requeridod")]
    [StringLength(255, ErrorMessage = "O campo  {0} deve está entre {2} e {1} caracteres", MinimumLength = 6)]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "O campo {0} é requeridod")]
    [StringLength(255, ErrorMessage = "O campo  {0} deve está entre {2} e {1} caracteres", MinimumLength = 6)]
    public string ConfirmPassword { get; set; }
}
