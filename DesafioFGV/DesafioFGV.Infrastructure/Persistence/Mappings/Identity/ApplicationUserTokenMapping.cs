using DesafioFGV.Domain.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioFGV.Infrastructure.Persistence.Mappings.Identity;

public class ApplicationUserTokenMapping : IEntityTypeConfiguration<ApplicationUserToken>
{
    public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
    {
        builder.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

        builder.ToTable("AspNetUserTokens");
    }
}
