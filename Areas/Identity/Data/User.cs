using Microsoft.AspNetCore.Identity;

namespace Onyx17.Areas.Identity.Data
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public string? Description { get; set; }
    }
}
