using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ZaliczeniowyProjekt.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        // Tabela 'Tasks'
        public DbSet<TaskItem> Tasks { get; set; }

        // Tabela na wiadomości kontaktowe
        public DbSet<ContactMessage> ContactMessages { get; set; }
    }
}
