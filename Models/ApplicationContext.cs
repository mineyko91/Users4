using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _4zadanie.Models
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            Database.EnsureCreated();
        }
    }
}
