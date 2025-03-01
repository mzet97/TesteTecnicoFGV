using System.Security.Claims;

namespace DesafioFGV.Application.UseCases.Auth.ViewModels;

public class UserTokenViewModel
{
    public string Id { get; set; }
    public string Email { get; set; }

    public IEnumerable<ClaimViewModel> Claims { get; set; }

    public UserTokenViewModel()
    {

    }

    public UserTokenViewModel(string id, string email, IList<Claim> claims)
    {
        Id = id;
        Email = email;
        Claims = claims.Select(c => new ClaimViewModel(c));
    }
}