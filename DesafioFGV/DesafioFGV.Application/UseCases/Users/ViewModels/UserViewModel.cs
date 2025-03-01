using DesafioFGV.Domain.Identities;

namespace DesafioFGV.Application.UseCases.Users.ViewModels;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserViewModel(Guid id, string email, string name, DateTime createdAt)
    {
        Id = id;
        Email = email;
        Name = name;
        CreatedAt = createdAt;
    }

    public UserViewModel(ApplicationUser user)
    {
        if(user == null)
        {
            return;
        }

        Id = user.Id;
        Email = user?.Email;
        Name = user?.UserName;
        CreatedAt = user.CreatedAt;
    }

    public UserViewModel()
    {
        
    }
}
