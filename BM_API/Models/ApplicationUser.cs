using Microsoft.AspNetCore.Identity;
using System.Net.NetworkInformation;

namespace BM_API.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
