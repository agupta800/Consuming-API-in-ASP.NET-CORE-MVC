using APi.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace APi.Data
{
    public class ApplicationContext:DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options)
        {
            
        }
        public DbSet<Customer> customers { get; set; }
    }
}
