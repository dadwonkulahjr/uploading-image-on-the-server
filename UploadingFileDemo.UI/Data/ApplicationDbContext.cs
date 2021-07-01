using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UploadingFileDemo.UI.Models;

namespace UploadingFileDemo.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<UploadImage> UploadImage { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
