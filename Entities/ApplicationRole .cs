using Microsoft.AspNetCore.Identity;
using System;

namespace MCS.Entities
{
    public class ApplicationRole : IdentityRole<long>
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
