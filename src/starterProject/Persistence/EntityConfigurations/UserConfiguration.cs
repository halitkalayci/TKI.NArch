using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash("123456",out passwordHash,out passwordSalt);

        User user = new()
        {
            AuthenticatorType = AuthenticatorType.None,
            CreatedDate = DateTime.UtcNow,
            Email = "tki@tki.com",
            FirstName = "TKI",
            LastName = "TKI",
            Status = true,
            Id = 1,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        builder.HasData(user);
    }
}
