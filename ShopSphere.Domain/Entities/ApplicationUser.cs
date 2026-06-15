using Microsoft.AspNetCore.Identity;

namespace ShopSphere.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}